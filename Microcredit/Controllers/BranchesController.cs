
using DinkToPdf;
using DinkToPdf.Contracts;
using Microcredit.ClassProject.BranchesSVC;
using Microcredit.Models;
using Microcredit.Reports.ExecuteSP;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;


namespace Microcredit.Controllers
{
    [Route("api/[controller]")]
    //[ApiController]
    public class BranchesController : ControllerBase
    {
        private readonly IBranches _Branches;
        private IConverter _converter;
        private readonly IExecuteBranches _executeBranches;
        private IDistributedCache _cache;
        private const string BRANCHESListCacheKey = "BRANCHESList";
        private ILogger<BranchesController> _logger;
        private static readonly SemaphoreSlim semaphore = new SemaphoreSlim(1, 1);

        public BranchesController(IBranches branches, IConverter converter, IExecuteBranches executeBranches, IDistributedCache cache, ILogger<BranchesController> logger)
        {

            _Branches = branches;
            _converter = converter;
            _executeBranches = executeBranches;
            _cache = cache ?? throw new ArgumentNullException(nameof(cache));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));

        }
        [HttpGet]
        //[Authorize]

        public async Task<IActionResult> GETALLBRANCHESASYNC()
        {


            if (_cache.TryGetValue(BRANCHESListCacheKey, out IEnumerable<BranchesReportT>? Branches))
            {
                _logger.Log(LogLevel.Information, "Branches list found in cache.");

            }
            else
            {

                try
                {
                    await semaphore.WaitAsync();
                    if (_cache.TryGetValue("Brancheslist", out Branches))
                    {
                        _logger.Log(LogLevel.Information, "Branches list found in cache.");
                    }
                    else
                    {


                        _logger.Log(LogLevel.Information, "Branches list not found in cache. Fetching from database.");
                        Branches = _Branches.GETALLBRANCHESASYNC("dbo.view_CreateReportBranches");
                        var cacheEntryOptions = new DistributedCacheEntryOptions()
                            .SetSlidingExpiration(TimeSpan.FromSeconds(60))
                            .SetAbsoluteExpiration(TimeSpan.FromSeconds(3600));
                        await _cache.SetAsync(BRANCHESListCacheKey, Branches, cacheEntryOptions);

                    }
                }
                finally
                {
                    semaphore.Release();
                }
            }
            return Ok(Branches);


        }


        [HttpGet("ReportBranches")]
        public IActionResult ReportBranches()
        {

            //return Ok(branches);
            var globalSettings = new GlobalSettings
            {
                ColorMode = ColorMode.Color,
                Orientation = Orientation.Portrait,
                PaperSize = PaperKind.A4,
                Margins = new MarginSettings { Top = 10 },
                DocumentTitle = "PDF Report",

                Out = @"D:\PDFCreator\Branches.pdf"
            };
            var objectSettings = new ObjectSettings
            {

                PagesCount = true,
                ProduceForms = true,
                HtmlContent = _executeBranches.GetHTMLStringWithoutParam(),
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
        [HttpGet("ReportBRANCHEBranchCode/{BranchCode}")]
        public IActionResult ReportBRANCHEBranchCode(int BranchCode)
        {
            var sqlParms = new SqlParameter { ParameterName = "@BranchCode", Value = BranchCode };

            //return Ok(branches);
            var globalSettings = new GlobalSettings
            {
                ColorMode = ColorMode.Color,
                Orientation = Orientation.Portrait,
                PaperSize = PaperKind.A4,
                Margins = new MarginSettings { Top = 10 },
                DocumentTitle = "PDF Report",

                Out = @"D:\PDFCreator\Branch.pdf"
            };
            var objectSettings = new ObjectSettings
            {

                PagesCount = true,
                ProduceForms = true,
                HtmlContent = _executeBranches.GetHTMLString(sqlParms),
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


        [HttpGet("{BranchCode}")]
        public async Task<IActionResult> GETBRANCHByidASYNC(int BranchCode)
        {
            if (BranchCode == 0) return NotFound();
            var geTBranchCode = await _Branches.GETBRANCHByidASYNC(BranchCode);



            return Ok(geTBranchCode);
        }

        [HttpPost]
        public async Task<IActionResult> CreateBranches([FromBody] BranchesT branches)
        {

            // Will hold all the errors related to registration
            if (branches is null)
            {
                return BadRequest("branches is null");

            }
            var result = await _Branches.CreateBranches(branches);


            if (result.IsValid)
            {
                // Don't reveal that the user does not exist or is not confirmed
                return Ok(new { Message = "Added successfully" });
            }
            _cache.Remove(BRANCHESListCacheKey);
            return BadRequest("Cannot Save");


        }
        [HttpPut("{BranchCode}")]
        public async Task<IActionResult> UpdateBranches(int BranchCode, [FromBody] BranchesT branches)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var result = await _Branches.UpdateBranches(BranchCode, branches);

            if (!result)
            {
                return BadRequest();
            }

            return NoContent();
        }

    }
}

