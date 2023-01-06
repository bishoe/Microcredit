using DinkToPdf;
using DinkToPdf.Contracts;
using Microcredit.ClassProject;
using Microcredit.Models;
using Microcredit.Reports.ExecuteSP;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Logging;
using Serilog;
using System.Data.SqlClient;

namespace Microcredit.Controllers
{
    [Route("api/[controller]")]
    //[ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly ApplicationDbContext _db;
        private readonly IProducts _products;
        private IConverter _converter;
        private readonly IExecuteProducts _executeProducts;
        private IDistributedCache _cache;
        private const string ProductsListCacheKey = "ProductsList";
        private ILogger<ProductsController> _logger;
        private static readonly SemaphoreSlim semaphore = new SemaphoreSlim(1, 1);

        public ProductsController(ApplicationDbContext db, IProducts products, IConverter converter, IExecuteProducts executeProducts, IDistributedCache cache, ILogger<ProductsController> logger

)
        {
            _db = db;
            _products = products;
            _converter = converter;
            _executeProducts = executeProducts;
            _cache = cache ?? throw new ArgumentNullException(nameof(cache));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));

        }
        [HttpGet]
        public async Task<IActionResult> GetALLProducts()
        {
            //var _GetALLProducts = await _products.GetProductsAsync();
            //if (_GetALLProducts == null)
            //{
            //    return BadRequest();
            //}
            //return Ok(_GetALLProducts);



            if (_cache.TryGetValue(ProductsListCacheKey, out IEnumerable<ProductsT>? Products))
            {
                _logger.Log(LogLevel.Information, "Categories list found in cache.");

            }
            else
            {

                try
                {
                    await semaphore.WaitAsync();
                    if (_cache.TryGetValue("Categorieslist", out Products))
                    {
                        _logger.Log(LogLevel.Information, "Categories list found in cache.");
                    }
                    else
                    {


                        _logger.Log(LogLevel.Information, "Categories list not found in cache. Fetching from database.");
                        Products = _products.GetProductsAsync("dbo.view_CreateReportProducts");
                        var cacheEntryOptions = new DistributedCacheEntryOptions()
                            .SetSlidingExpiration(TimeSpan.FromSeconds(60))
                            .SetAbsoluteExpiration(TimeSpan.FromSeconds(3600));
                        await _cache.SetAsync(ProductsListCacheKey, Products, cacheEntryOptions);

                    }
                }
                finally
                {
                    semaphore.Release();
                }
            }
            return Ok(Products);



        }


        [HttpGet("{ProdouctsID}")]
        public async Task<IActionResult> GetProductsById(int ProdouctsID)
        {
            if (ProdouctsID == 0)
            {
                return NotFound();
            }
            var GETPoductsBYId = await _products.GetProductsByIdAsync(ProdouctsID);

            return Ok(GETPoductsBYId);
        }

        [HttpGet("GetProductbyBarcode/{Barcode}")]
        public async Task<IActionResult> GetProductbyBarcode(int Barcode)
        {
            var GetProductbyName = await _products.GetProductbyBarcode(Barcode);
            return Ok(GetProductbyName);
        }

        [HttpPost]
        public async Task<IActionResult> CreateProducts([FromBody] ProductsT productsT)
        {

            var result = await _products.CreateProductsAsync(productsT);
            var my = productsT.ProdouctsID;
            if (result.IsValid)
            {
                return Ok(new { Message = "Added successfully" });
            }

            return BadRequest("Cannot Save");
        }

        [HttpPut("{prodouctsID}")]
        public async Task<IActionResult> UpdateProducts(int ProdouctsID, [FromBody] ProductsT productsT)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            try
            {
                var result = await _products.UpdateProductsAsync(ProdouctsID, productsT);
                if (!result)
                {
                    return BadRequest();
                }
            }
            catch (Exception ex)
            {
                Log.Error("An error occurred while seeding the database  {Error} {StackTrace} {InnerException} {Source}",
                    ex.Message, ex.StackTrace, ex.InnerException, ex.Source);
                return BadRequest();
            }
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProducts(int productsId)
        {
            //var GETProductsId = await _db.Products.FindAsync(productsId);

            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest();
                }
                var GetproductsId = await _products.DeleteProductsAsync(productsId);
                if (!GetproductsId)
                {
                    return BadRequest();

                }

            }

            catch (Exception ex)
            {
                Log.Error("An error occurred while seeding the database  {Error} {StackTrace} {InnerException} {Source}",
                    ex.Message, ex.StackTrace, ex.InnerException, ex.Source);
                return BadRequest();
            }
            return Ok();


        }

        [HttpGet("ReportProducts")]
        public IActionResult ReportProducts()
        {

            //return Ok(branches);
            var globalSettings = new GlobalSettings
            {
                ColorMode = ColorMode.Color,
                Orientation = Orientation.Portrait,
                PaperSize = PaperKind.A4,
                Margins = new MarginSettings { Top = 10 },
                DocumentTitle = "PDF Report",

                Out = @"D:\PDFCreator\Products.pdf"
            };
            var objectSettings = new ObjectSettings
            {

                PagesCount = true,
                ProduceForms = true,
                HtmlContent = _executeProducts.GetHTMLString(),
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
        [HttpGet("ReportProduct/{ProdouctsID}")]
        public IActionResult ReportProduct(int ProdouctsID)
        {
            var sqlParms = new SqlParameter { ParameterName = "@ProdouctsID", Value = ProdouctsID };

            //return Ok(branches);
            var globalSettings = new GlobalSettings
            {
                ColorMode = ColorMode.Color,
                Orientation = Orientation.Portrait,
                PaperSize = PaperKind.A4,
                Margins = new MarginSettings { Top = 10 },
                DocumentTitle = "PDF Report",

                Out = @"D:\PDFCreator\Product.pdf"
            };
            var objectSettings = new ObjectSettings
            {

                PagesCount = true,
                ProduceForms = true,
                HtmlContent = _executeProducts.GetHTMLString(sqlParms),
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

