using DinkToPdf;
using DinkToPdf.Contracts;
using Microcredit.ClassProject.PermissionToEntertheStoreProductSVC;
using Microcredit.Models;
using Microcredit.Reports.ExecuteSP;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Logging;

namespace Microcredit.Controllers
{
    [Route("api/[controller]")]
    //[ApiController]
    public class PermissionToEntertheStoreProductController : ControllerBase
    {
        private IConverter _converter;
        private readonly ApplicationDbContext _db;

        private readonly IPermissionToEntertheStoreProduct _IPermissionToEntertheStoreProduct;
        private readonly IReportExecutePermissionToEntertheStoreProduct _reportExecutePermissionToEntertheStoreProduct;
        private IDistributedCache _cache;
        private const string PermissionToEntertheStoreProductListCacheKey = "PermissionToEntertheStoreProductList";
        private ILogger<PermissionToEntertheStoreProductController> _logger;
        private static readonly SemaphoreSlim semaphore = new SemaphoreSlim(1, 1);


        //private readonly IGetAllPermissionToEntertheStoreProduct _getAllPermissionToEntertheStoreProduct;


        public PermissionToEntertheStoreProductController(



            IConverter converter,
            ApplicationDbContext db,
            IPermissionToEntertheStoreProduct IPermissionToEntertheStoreProduct,

      //IGetAllPermissionToEntertheStoreProduct getAllPermissionToEntertheStoreProduct ,

      IReportExecutePermissionToEntertheStoreProduct reportExecutePermissionToEntertheStoreProduct, IDistributedCache cache, ILogger<PermissionToEntertheStoreProductController> logger



            )
        {
            _converter = converter;
            _db = db;
            _IPermissionToEntertheStoreProduct = IPermissionToEntertheStoreProduct;
            //_getAllPermissionToEntertheStoreProduct = getAllPermissionToEntertheStoreProduct;
            _reportExecutePermissionToEntertheStoreProduct = reportExecutePermissionToEntertheStoreProduct;
            _cache = cache ?? throw new ArgumentNullException(nameof(cache));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));

        }
        [HttpGet]
        public async Task<IActionResult> GetAllPermissionToEntertheStoreProduct()
        {
            //try
            //{

            //    var ExecuteSPObject = _IPermissionToEntertheStoreProduct.GetAllPermissionToEntertheStoreProductAsync("dbo.view_PermissionToEntertheStoreProduct");

            //    return Ok(ExecuteSPObject);
            //}
            //catch (Exception ex)
            //{

            //    throw ex.InnerException;
            //}
            if (_cache.TryGetValue(PermissionToEntertheStoreProductListCacheKey, out IEnumerable<ReportPermissionToEntertheStoreProduct>? ReportPermissionToEntertheStoreProducts))
            {
                _logger.Log(LogLevel.Information, "permissionToEntertheStoreProductT list found in cache.");

            }
            else
            {

                try
                {
                    await semaphore.WaitAsync();
                    if (_cache.TryGetValue("permissionToEntertheStoreProductTlist", out ReportPermissionToEntertheStoreProducts))
                    {
                        _logger.Log(LogLevel.Information, "permissionToEntertheStoreProductTlist list found in cache.");
                    }
                    else
                    {


                        _logger.Log(LogLevel.Information, "permissionToEntertheStoreProductTlist list not found in cache. Fetching from database.");
                        ReportPermissionToEntertheStoreProducts = _IPermissionToEntertheStoreProduct.GetAllPermissionToEntertheStoreProductAsync("dbo.view_PermissionToEntertheStoreProduct");
                        var cacheEntryOptions = new DistributedCacheEntryOptions()
                            .SetSlidingExpiration(TimeSpan.FromSeconds(60))
                            .SetAbsoluteExpiration(TimeSpan.FromSeconds(3600));
                        await _cache.SetAsync(PermissionToEntertheStoreProductListCacheKey, ReportPermissionToEntertheStoreProducts, cacheEntryOptions);

                    }
                }
                finally
                {
                    semaphore.Release();
                }
            }
            return Ok(ReportPermissionToEntertheStoreProducts);


        }


        [HttpGet("{PermissionToEntertheStoreProductId}")]
        public async Task<IActionResult> GetPermissionToEntertheStoreProductByidAsync(int PermissionToEntertheStoreProductId)
        {
            if (PermissionToEntertheStoreProductId == 0) return NotFound();

            var PermissionToEntertheStoreProductIdById = await _IPermissionToEntertheStoreProduct.GetPermissionToEntertheStoreProductByidAsync(PermissionToEntertheStoreProductId);

            return Ok(PermissionToEntertheStoreProductIdById);


        }


        [HttpPost]
        public async Task<IActionResult> CreatePermissionToEntertheStoreProductAsync([FromBody] PermissionToEntertheStoreProductT PermissionToEntertheStoreProduct)
        {

            var AddPermissionToEntertheStoreProduct = await _IPermissionToEntertheStoreProduct.CreatePermissionToEntertheStoreProductAsync(PermissionToEntertheStoreProduct);

            if (AddPermissionToEntertheStoreProduct.IsValid)
            {
                return Ok(new { AddPermissionToEntertheStoreProduct.Message });
                //return Ok(new { Message = "Success" });

            }
            return BadRequest("Cannot Save");

        }

        [HttpPut("{PermissionToEntertheStoreProductId}")]
        public async Task<IActionResult> UpdatePermissionToEntertheStoreProductAsync([FromBody] int PermissionToEntertheStoreProductId, [FromBody] PermissionToEntertheStoreProductT PermissionToEntertheStoreProduct)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var result = await _IPermissionToEntertheStoreProduct.UpdatePermissionToEntertheStoreProductAsync(PermissionToEntertheStoreProductId, PermissionToEntertheStoreProduct);

            if (!result)
            {
                return BadRequest();
            }

            return NoContent();
        }
        [HttpDelete("{PermissionToEntertheStoreProductId}")]
        public async Task<IActionResult> DeletePermissionToEntertheStoreProductAsync(int PermissionToEntertheStoreProductId)
        {
            if (!ModelState.IsValid) return BadRequest();
            var GETPermissionToEntertheStoreProductId = await _IPermissionToEntertheStoreProduct.DeletePermissionToEntertheStoreProductAsync(PermissionToEntertheStoreProductId);
            if (!GETPermissionToEntertheStoreProductId) return BadRequest();

            return NoContent();
        }

        [HttpGet("ReportPermissionToEntertheStoreProduct/{PermissionToEntertheStoreProductId}")]
        public IActionResult ReportPermissionToEntertheStoreProduct(int PermissionToEntertheStoreProductId)
        {
            var sqlParms = new SqlParameter { ParameterName = "@PermissionToEntertheStoreProductId", Value = PermissionToEntertheStoreProductId };

            //return Ok(branches);
            var globalSettings = new GlobalSettings
            {
                ColorMode = ColorMode.Color,
                Orientation = Orientation.Portrait,
                PaperSize = PaperKind.A4,
                Margins = new MarginSettings { Top = 10 },
                DocumentTitle = "PDF Report",

                Out = @"D:\PDFCreator\ReportPermissionToEntertheStoreProduct.pdf"
            };
            var objectSettings = new ObjectSettings
            {

                PagesCount = true,
                ProduceForms = true,
                HtmlContent = _reportExecutePermissionToEntertheStoreProduct.GetHTMLString(sqlParms),
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
