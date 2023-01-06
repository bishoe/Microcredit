
using DinkToPdf;
using DinkToPdf.Contracts;
using Microcredit.ClassProject.MasterOFSToresSVC;
using Microcredit.Models;
using Microcredit.Reports.ExecuteSP;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Logging;

namespace Microcredit.Controllers
{
    [Route("api/[controller]")]

    public class ManageStoreController : ControllerBase
    {
        private readonly ApplicationDbContext _db;
        private readonly IManageStore _manageStore;
        private readonly IExecuteManageStore _executeManageStore;
        private IConverter _converter;
        private IDistributedCache _cache;
        private const string ManageStoreListCacheKey = "ManageStoreList";
        private ILogger<ManageStoreController> _logger;
        private static readonly SemaphoreSlim semaphore = new SemaphoreSlim(1, 1);

        public ManageStoreController(ApplicationDbContext db, IManageStore manageStore, IExecuteManageStore executeManageStore, IConverter converter, IDistributedCache cache, ILogger<ManageStoreController> logger
)
        {
            _db = db;
            _manageStore = manageStore;
            _executeManageStore = executeManageStore;
            _converter = converter;
            _cache = cache ?? throw new ArgumentNullException(nameof(cache));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));

        }
        [HttpGet]
        public async Task<IActionResult> GETALLStore()
        {
            //var GetAllStore = await _manageStore.GetAllManageStoreAsync();

            //return Ok(GetAllStore);


            if (_cache.TryGetValue(ManageStoreListCacheKey, out IEnumerable<ManageStoreT>? ManageStore))
            {
                _logger.Log(LogLevel.Information, "ManageStore list found in cache.");

            }
            else
            {

                try
                {
                    await semaphore.WaitAsync();
                    if (_cache.TryGetValue("ManageStore list", out ManageStore))
                    {
                        _logger.Log(LogLevel.Information, "ManageStore list found in cache.");
                    }
                    else
                    {


                        _logger.Log(LogLevel.Information, "ManageStore list not found in cache. Fetching from database.");
                        ManageStore = _manageStore.GetAllManageStoreAsync("dbo.view_CreateReportManageStore");
                        var cacheEntryOptions = new DistributedCacheEntryOptions()
                            .SetSlidingExpiration(TimeSpan.FromSeconds(60))
                            .SetAbsoluteExpiration(TimeSpan.FromSeconds(3600));
                        await _cache.SetAsync(ManageStoreListCacheKey, ManageStore, cacheEntryOptions);

                    }
                }
                finally
                {
                    semaphore.Release();
                }
            }
            return Ok(ManageStore);

        }
        [HttpGet("{ManageStoreID}")]
        public async Task<IActionResult> GETStoreById(int ManageStoreID)
        {
            if (ManageStoreID == 0) return NotFound();
            var GetStoreByid = await _manageStore.GetManageStoreByidAsync(ManageStoreID);
            return Ok(GetStoreByid);

        }

        [HttpPost]
        public async Task<IActionResult> CreateManageStoreAsync([FromBody] ManageStoreT manageStore)
        {
            var result = await _manageStore.CreateManageStoreAsync(manageStore);
            if (result.IsValid)
            {

                return Ok(new { Message = "Added successfully" });
            }
            return BadRequest("Cannot Save");


        }

        [HttpPut("{ManageStoreID}")]
        public async Task<IActionResult> UpdateManageStore([FromBody] ManageStoreT manageStore, int ManageStoreID)
        {
            if (!ModelState.IsValid) return BadRequest();

            var result = await _manageStore.UpdateManageStoreAsync(ManageStoreID, manageStore);
            if (!result)
                return BadRequest();
            return NoContent();

        }
        [HttpGet("ReportManageStore/{ManageStoreId}")]
        public IActionResult ReportManageStore(int ManageStoreId)
        {
            var sqlParms = new SqlParameter { ParameterName = "@ManageStoreId", Value = ManageStoreId };

            //return Ok(branches);
            var globalSettings = new GlobalSettings
            {
                ColorMode = ColorMode.Color,
                Orientation = Orientation.Portrait,
                PaperSize = PaperKind.A4,
                Margins = new MarginSettings { Top = 10 },
                DocumentTitle = "PDF Report",

                Out = @"D:\PDFCreator\ReportManageStore.pdf"
            };
            var objectSettings = new ObjectSettings
            {

                PagesCount = true,
                ProduceForms = true,
                HtmlContent = _executeManageStore.GetHTMLString(sqlParms),
                WebSettings = { DefaultEncoding = "utf-8", UserStyleSheet = Path.Combine(Directory.GetCurrentDirectory(), "assets", "styles.css") },
                HeaderSettings = { FontName = "Arial", FontSize = 9, Right = "Page [page] of [toPage]", Line = true },
                FooterSettings = { FontName = "Arial", FontSize = 9, Line = true, Center = "Report Footer" }
            };
            var pdf = new HtmlToPdfDocument()
            {

                GlobalSettings = globalSettings,
                Objects = { objectSettings }
            };
            _converter.Convert(pdf);
            return Ok("Successfully created PDF document.");

        }


    }
}
