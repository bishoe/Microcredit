using DinkToPdf;
using DinkToPdf.Contracts;
using Microcredit.ClassProject.MasterProductsWarehouseSVC.ProductsWarehouseSVC;
using Microcredit.Models;
using Microcredit.Reports.ExecuteSP;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Logging;
using System.Data.SqlClient;

namespace Microcredit.Controllers
{
    [Route("api/[controller]")]
    //[ApiController]
    public class ProductsWarehouseController : ControllerBase
    {
        private readonly ApplicationDbContext _db;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private IConverter _converter;
        private readonly IExecuteProductsWarehouse _executeProductsWarehouse;
        private IDistributedCache _cache;
        private const string ProductsWarehouseListCacheKey = "ProductsWarehouseList";
        private ILogger<ProductsWarehouseController> _logger;
        private static readonly SemaphoreSlim semaphore = new SemaphoreSlim(1, 1);


        //private readonly IMasterProductsWarehouse _masterProductsWarehouse;
        private readonly IProductsWarehouse _productsWarehouse;


        public ProductsWarehouseController(ApplicationDbContext db, IProductsWarehouse productsWarehouse, IHttpContextAccessor httpContextAccessor, IConverter converter, IExecuteProductsWarehouse executeProductsWarehouse, IDistributedCache cache, ILogger<ProductsWarehouseController> logger

            //, IMasterProductsWarehouse masterProductsWarehouse
            )
        {
            _db = db;
            //_masterProductsWarehouse = masterProductsWarehouse;
            _productsWarehouse = productsWarehouse;
            this._httpContextAccessor = httpContextAccessor;
            _executeProductsWarehouse = executeProductsWarehouse;
            _cache = cache ?? throw new ArgumentNullException(nameof(cache));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));

        }
        [HttpGet]
        public async Task<IActionResult> GetAllProductsWarehouse()
        {
 
            if (_cache.TryGetValue(ProductsWarehouseListCacheKey, out IEnumerable<ProductsWarehouseObjectT>? ProductsWarehouseObject))
            {
                _logger.Log(LogLevel.Information, "ProductsWarehouse list found in cache.");

            }
            else
            {

                try
                {
                    await semaphore.WaitAsync();
                    if (_cache.TryGetValue("ProductsWarehouselist", out ProductsWarehouseObject))
                    {
                        _logger.Log(LogLevel.Information, "ProductsWarehouse list found in cache.");
                    }
                    else
                    {
 
                        _logger.Log(LogLevel.Information, "ProductsWarehouse list not found in cache. Fetching from database.");
                        ProductsWarehouseObject = _productsWarehouse.GetAllProductsWarehouse("dbo.view_CreateReportProductsWarehouse");
                        var cacheEntryOptions = new DistributedCacheEntryOptions()
                            .SetSlidingExpiration(TimeSpan.FromSeconds(60))
                            .SetAbsoluteExpiration(TimeSpan.FromSeconds(3600));
                        await _cache.SetAsync(ProductsWarehouseListCacheKey, ProductsWarehouseObject, cacheEntryOptions);

                    }
                }
                finally
                {
                    semaphore.Release();
                }
            }
            return Ok(ProductsWarehouseObject);
        }
        //[HttpGet("{id}")]
        //public async Task<IActionResult> GetProductsWarehouseBYBillnoAsync(int Billno)

        //{
        //    if (Billno == 0) return NotFound();

        //    var GetProductsWarehouseBYBill = await _productsWarehouse.GetProductsWarehouseBYBillnoAsync(Billno);

        //    return Ok(GetProductsWarehouseBYBill);

        //}
        //[HttpGet("{ManageStoreID}")]
        //public async Task<IActionResult> GetProductsWarehouseBYIDAsync(int ManageStoreID)

        //{
        //    if (ManageStoreID == 0) return NotFound();

        //    var GetProductsWarehouseBYManageStoreID = await _productsWarehouse.GetProductsWarehouseBYIDAsync(ManageStoreID);

        //    return Ok(GetProductsWarehouseBYManageStoreID);

        //}

        [HttpPost]
        public async Task<IActionResult> CreateProductsWarehouse([FromBody] ProductsWarehouseObjectT ProductsWarehouseModel)
        {
 
            MasterProductsWarehouseT masterProductsWarehouse = new();
            ProductsWarehouseT productsWarehouseT = new();
            var AddProductsWarehouseModelresult = await _productsWarehouse.CreateProductsWarehouse(masterProductsWarehouse, productsWarehouseT, ProductsWarehouseModel); 

            if (AddProductsWarehouseModelresult.IsValid)
            {
                return Ok(new { Message = "Added successfully", masterProductsWarehouse.ManageStoreID });
             
            }
            return BadRequest("Cannot Save");

        }


        [HttpGet("GetSellingPrice/{ProdouctsID}")]
        public IActionResult GetSellingPrice(int ProdouctsID)
        {
            var checkexistsId = true;
            checkexistsId = _db.ProductsWarehouse.Any(x => x.ProdouctsID == ProdouctsID);
            if (checkexistsId is false) return BadRequest("Cannot Find Prodouct");
            var GetSellingPrice = _db.ProductsWarehouse.Where(x => x.ProdouctsID == ProdouctsID).FirstOrDefault().SellingPrice;
            GC.Collect();
            return Ok(GetSellingPrice);

        }

        public IActionResult ReportAllProductsWarehouse()
        {

            //return Ok(branches);
            var globalSettings = new GlobalSettings
            {
                ColorMode = ColorMode.Color,
                Orientation = Orientation.Portrait,
                PaperSize = PaperKind.A4,
                Margins = new MarginSettings { Top = 10 },
                DocumentTitle = "PDF Report",

                Out = @"D:\PDFCreator\AllProductsWarehouse.pdf"
            };
            var objectSettings = new ObjectSettings
            {

                PagesCount = true,
                ProduceForms = true,
                HtmlContent = _executeProductsWarehouse.GetHTMLString(),
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
        [HttpGet("ReportProductsWarehouse/{ManageStoreID}")]
        public IActionResult ReportProductsWarehouse(int ManageStoreID)
        {
            var sqlParms = new SqlParameter { ParameterName = "@ManageStoreID", Value = ManageStoreID };

            //return Ok(branches);
            var globalSettings = new GlobalSettings
            {
                ColorMode = ColorMode.Color,
                Orientation = Orientation.Portrait,
                PaperSize = PaperKind.A4,
                Margins = new MarginSettings { Top = 10 },
                DocumentTitle = "PDF Report",

                Out = @"D:\PDFCreator\ProductsWarehouse.pdf"
            };
            var objectSettings = new ObjectSettings
            {

                PagesCount = true,
                ProduceForms = true,
                HtmlContent = _executeProductsWarehouse.GetHTMLString(sqlParms),
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

