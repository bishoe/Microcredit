using DinkToPdf;
using DinkToPdf.Contracts;
using Microcredit.ClassProject;
using Microcredit.Models;
using Microcredit.Reports.ExecuteSP;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using System.Data.SqlClient;

namespace Microcredit.Controllers
{
    [Route("api/[controller]")]
    //[ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategories _categories;
        private readonly ApplicationDbContext _db;
        IConverter _converter; IExecuteCategories _executeCategories;
        private IDistributedCache _cache;
        private const string CategoriesListCacheKey = "CategoriesList";
        private ILogger<CategoriesController> _logger;
        private static readonly SemaphoreSlim semaphore = new SemaphoreSlim(1, 1);


        public CategoriesController(ICategories categories, ApplicationDbContext db, IConverter converter, IExecuteCategories executeCategories, IDistributedCache cache, ILogger<CategoriesController> logger
)
        {
            _db = db;
            _categories = categories;
            _converter = converter;
            _executeCategories = executeCategories;
            _cache = cache ?? throw new ArgumentNullException(nameof(cache));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));

        }
        // GET All Categories
        [HttpGet]
        public async Task<IActionResult> GetCategories()
        {
            //var categories =   _categories.GeTCategoriesAsync();

            //return Ok(categories);

            if (_cache.TryGetValue(CategoriesListCacheKey, out IEnumerable<CategoriesT>? Categories))
            {
                _logger.Log(LogLevel.Information, "Categories list found in cache.");

            }
            else
            {

                try
                {
                    await semaphore.WaitAsync();
                    if (_cache.TryGetValue("Categorieslist", out Categories))
                    {
                        _logger.Log(LogLevel.Information, "Categories list found in cache.");
                    }
                    else
                    {


                        _logger.Log(LogLevel.Information, "Categories list not found in cache. Fetching from database.");
                        Categories = _categories.GeTCategoriesAsync("dbo.view_CreateReportCategories");
                        var cacheEntryOptions = new DistributedCacheEntryOptions()
                            .SetSlidingExpiration(TimeSpan.FromSeconds(60))
                            .SetAbsoluteExpiration(TimeSpan.FromSeconds(3600));
                        await _cache.SetAsync(CategoriesListCacheKey, Categories, cacheEntryOptions);

                    }
                }
                finally
                {
                    semaphore.Release();
                }
            }
            return Ok(Categories);
        }
        //[Route("[action]")]
        // GETById api/Categories/1
        [HttpGet("{CategoryProductId}")]
        public async Task<IActionResult> GeTCategoriesById(int CategoryProductId)
        {

            if (CategoryProductId == 0) return NotFound();
            var geTcategoriesbyid = await _categories.GeTCategoriesByIdAsync(CategoryProductId);



            return Ok(geTcategoriesbyid);

        }
        [HttpPost]
        public async Task<IActionResult> CreateCategories([FromBody] CategoriesT categoriesViewModel)
        {

            // Will hold all the errors related to registration

            var result = await _categories.CreateCategoriesAsync(categoriesViewModel);


            if (result.IsValid)
            {
                // Don't reveal that the user does not exist or is not confirmed
                return Ok(new { Message = "Added successfully" });
            }
            return BadRequest("Cannot Save");


        }
        [HttpPut("{CategoryProductId}")]
        public async Task<IActionResult> UpdateCategories(int CategoryProductId, [FromBody] CategoriesT categories)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var result = await _categories.UpdateCategoriesAsync(CategoryProductId, categories);

            if (result.IsValid)
            {

                return Ok(new { Message = "Added successfully" });
            }

            return NoContent();
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCategories(int categoryProductId)
        {



            if (!ModelState.IsValid) return BadRequest();

            var GETCategoryProductId = await _categories.DeleteCategoriesAsync(categoryProductId);
            if (!GETCategoryProductId)
            {
                return BadRequest();

            }

            return NoContent();
        }
        [HttpGet("ReportCategories")]
        public IActionResult ReportCategories()
        {

            //return Ok(branches);
            var globalSettings = new GlobalSettings
            {
                ColorMode = ColorMode.Color,
                Orientation = Orientation.Portrait,
                PaperSize = PaperKind.A4,
                Margins = new MarginSettings { Top = 10 },
                DocumentTitle = "PDF Report",

                Out = @"D:\PDFCreator\Categories.pdf"
            };
            var objectSettings = new ObjectSettings
            {

                PagesCount = true,
                ProduceForms = true,
                HtmlContent = _executeCategories.GetHTMLString(),
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
        [HttpGet("ReportReportCategorie-{CategoryProductId}")]
        public IActionResult ReportProduct(int CategoryProductId)
        {
            var sqlParms = new SqlParameter { ParameterName = "@CategoryProductId", Value = CategoryProductId };

            //return Ok(branches);
            var globalSettings = new GlobalSettings
            {
                ColorMode = ColorMode.Color,
                Orientation = Orientation.Portrait,
                PaperSize = PaperKind.A4,
                Margins = new MarginSettings { Top = 10 },
                DocumentTitle = "PDF Report",

                Out = @"D:\PDFCreator\Category.pdf"
            };
            var objectSettings = new ObjectSettings
            {

                PagesCount = true,
                ProduceForms = true,
                HtmlContent = _executeCategories.GetHTMLString(sqlParms),
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
