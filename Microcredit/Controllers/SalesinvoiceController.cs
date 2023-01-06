using DinkToPdf;
using DinkToPdf.Contracts;
using Microcredit.ClassProject.SalesinvoiceSVC;
using Microcredit.Models;
using Microcredit.Reports.ReportSalesInvoice;
using Microsoft.AspNetCore.Mvc;
//using System.Data.SqlClient;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Logging;

namespace Microcredit.Controllers
{
    [Route("api/[controller]")]
    //[ApiController]
    public class SalesinvoiceController : ControllerBase
    {
        private readonly ISalesinvoice _salesinvoice;
        private readonly ApplicationDbContext _db;
        //private readonly IReportSalesInvoice _reportSalesInvoice;
        private IConverter _converter;
        private readonly IReportS _ReportSalesInvoice;
        private IDistributedCache _cache;
        private const string SalesinvoiceListCacheKey = "SalesinvoiceList";
        private ILogger<SalesinvoiceController> _logger;
        private static readonly SemaphoreSlim semaphore = new SemaphoreSlim(1, 1);

        public SalesinvoiceController(ISalesinvoice salesinvoice, ApplicationDbContext db, IConverter converter, IReportS reportSalesInvoice
, IReportS ReportSalesInvoice, IDistributedCache cache, ILogger<SalesinvoiceController> logger

)
        {
            _db = db;
            _salesinvoice = salesinvoice;
            _converter = converter;
            _ReportSalesInvoice = ReportSalesInvoice;
            _cache = cache ?? throw new ArgumentNullException(nameof(cache));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));

        }
        [HttpGet("{SellingMasterID}")]
        public IActionResult GetCurrentSalesInvoice(int SellingMasterID)
        {

            if (SellingMasterID <= 0) return BadRequest("invalid id ");
            var GETSellingMasterID = _db.SalesInvoicesMaster.Where(b => b.SellingMasterID == SellingMasterID).ToList();

            if (GETSellingMasterID.Count != 0)
            {

                var globalSettings = new GlobalSettings
                {
                    ColorMode = ColorMode.Color,
                    Orientation = Orientation.Portrait,
                    PaperSize = PaperKind.A4,
                    Margins = new MarginSettings { Top = 10 },
                    DocumentTitle = "PDF Report",

                    Out = @"D:\PDFCreator\SalesInvoice_Report.pdf"
                };
                var objectSettings = new ObjectSettings
                {

                    PagesCount = true,
                    ProduceForms = true,
                    HtmlContent = _ReportSalesInvoice.GetHTMLString(SellingMasterID),
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
            return BadRequest("Cannot create Report");
        }


        //return Ok(_salesinvoice.CreateReportSalesInvoice(SellingMasterID));







        //        public async Task< IActionResult> GetCurrentSalesInvoice(int SellingMasterID)
        //        {
        //         //   string StoredProc = "exec _db.SalesInvoicesMaster" +

        //var _SellingMasterID = new SqlParameter("@SellingMasterID", SellingMasterID);

        //            var sell = _db.SalesInvoicesMaster.FromSqlRaw("SalesInvoicesMaster", _SellingMasterID).ToList();
        //            //return (await _db.SalesInvoicesMaster.FromSqlRaw("exec SP_CreateReportSalesInvoiceById @SellingMasterID", parameters: new[] { _SellingMasterID }).ToListAsync()).FirstOrDefault();


        //            return Ok(sell);
        //        }


        //public     IActionResult  GetCurrentSalesInvoice()
        //{

        //    return  Ok(_salesinvoice.GetInvoiceReportByID(2));
        //}
        // public  async Task<IEnumerable<IActionResult>> GetCurrentSalesInvoice() {
        //    //var query1 = _db.ClassForreports().Where(c =>c.SellingMasterID ==2).ToList();

        //    //_context.NameAndTotalSpentByCustomer().Where(c => c.TotalSpent > 100).ToList();
        //    //var query1 =  await _db.ClassForreports().
        //    return Ok(query1);


        //}
        [HttpPost]
        public async Task<IActionResult> CreateSalesinvoiceAsync([FromBody] SalesinvoiceObject salesinvoice)
        {
            // Will hold all the errors related to 

            var result = await _salesinvoice.CreateSalesinvoiceAsync(salesinvoice);


            if (salesinvoice.SellingMasterID <= 0) return BadRequest("invalid id ");
            var GETSellingMasterID = _db.SalesInvoicesMaster.Where(b => b.SellingMasterID == salesinvoice.SellingMasterID).ToList();
            //Report
            if (GETSellingMasterID.Count != 0)
            {

                var globalSettings = new GlobalSettings
                {
                    ColorMode = ColorMode.Color,
                    Orientation = Orientation.Portrait,
                    PaperSize = PaperKind.A4,
                    Margins = new MarginSettings { Top = 10 },
                    DocumentTitle = "PDF Report",

                    Out = @"D:\PDFCreator\Selling_Report.pdf"
                };
                var objectSettings = new ObjectSettings
                {

                    PagesCount = true,
                    ProduceForms = true,
                    HtmlContent = _ReportSalesInvoice.GetHTMLString(salesinvoice.SellingMasterID),
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
            return BadRequest("Cannot create Report");



        }
        [HttpGet("ReportSalesinvoice/{SellingMasterID}")]
        public IActionResult ReportSalesinvoice(int SellingMasterID)
        {
            var sqlParms = new SqlParameter { ParameterName = "@SellingMasterID", Value = SellingMasterID };

            //return Ok(branches);
            var globalSettings = new GlobalSettings
            {
                ColorMode = ColorMode.Color,
                Orientation = Orientation.Portrait,
                PaperSize = PaperKind.A4,
                Margins = new MarginSettings { Top = 10 },
                DocumentTitle = "PDF Report",

                Out = @"D:\PDFCreator\Selling.pdf"
            };
            var objectSettings = new ObjectSettings
            {

                PagesCount = true,
                ProduceForms = true,
                HtmlContent = _ReportSalesInvoice.GetHTMLString(SellingMasterID),
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

        [HttpGet]
        public async Task<IActionResult> GetAllsalesinvoice()
        {


            if (_cache.TryGetValue(SalesinvoiceListCacheKey, out IEnumerable<SalesinvoiceObjectReport>? Salesinvoice))
            {
                _logger.Log(LogLevel.Information, "salesinvoice list found in cache.");

            }
            else
            {

                try
                {
                    await semaphore.WaitAsync();
                    if (_cache.TryGetValue("salesinvoicelist", out Salesinvoice))
                    {
                        _logger.Log(LogLevel.Information, "salesinvoice list found in cache.");
                    }
                    else
                    {


                        _logger.Log(LogLevel.Information, "salesinvoice list not found in cache. Fetching from database.");
                        Salesinvoice = _salesinvoice.GetAllsalesinvoice("dbo.view_CreateReportSalesInvoices");
                        var cacheEntryOptions = new DistributedCacheEntryOptions()
                            .SetSlidingExpiration(TimeSpan.FromSeconds(60))
                            .SetAbsoluteExpiration(TimeSpan.FromSeconds(3600));
                        await _cache.SetAsync(SalesinvoiceListCacheKey, Salesinvoice, cacheEntryOptions);

                    }
                }
                finally
                {
                    semaphore.Release();
                }
            }
            return Ok(Salesinvoice);

        }


    }
}
