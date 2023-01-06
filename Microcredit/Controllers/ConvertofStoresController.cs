using DinkToPdf;
using DinkToPdf.Contracts;
using Microcredit.ClassProject.ConvertofStoresSVC;
using Microcredit.Models;
using Microcredit.Reports.ExecuteSP;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Serilog;

namespace Microcredit.Controllers
{
    [Route("api/[controller]")]
    //[ApiController]
    public class ConvertofStoresController : ControllerBase
    {
        private readonly ApplicationDbContext _db;

        private readonly IConvertofStores _convertofStores;
        private readonly IExecuteConvertofStores _ExecuteConvertofStores;
        private IConverter _converter;


        public ConvertofStoresController(ApplicationDbContext db, IConvertofStores convertofStores,

            IExecuteConvertofStores executeConvertofStores, IConverter converter
            )
        {
            _db = db;
            _convertofStores = convertofStores;
            _ExecuteConvertofStores = executeConvertofStores;
            _converter = converter;

        }
        [HttpGet]
        public IActionResult GetAllConvertofStoresAsync()
        {



            try
            {
                var ExecuteSPObject = _convertofStores.GetAllConvertofStoresAsync("dbo.View_GetAllConvertOfStores");

                if (ExecuteSPObject == null)
                {
                    return BadRequest();
                }


                return Ok(ExecuteSPObject);
            }
            catch (Exception ex)
            {

                Log.Error("An error occurred while seeding the database  {Error} {StackTrace} {InnerException} {Source}",
                ex.Message, ex.StackTrace, ex.InnerException, ex.Source);
            }
            return Ok();
        }
        [HttpGet]


        [HttpGet("{IdConvertofStores}")]
        public async Task<IActionResult> GetConvertofStoresByidAsync(int IdConvertofStores)
        {
            var GetConvertofStores = (ConvertofStoresT)null;

            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            if (IdConvertofStores == 0) return BadRequest();
            GetConvertofStores = await _convertofStores.GetConvertofStoresByidAsync(IdConvertofStores);

            return Ok(GetConvertofStores);
        }


        [HttpPost]
        public async Task<IActionResult> CreateConvertofStoresAsync([FromBody] ConvertofStoresT convertofStores)
        {
            var result = await _convertofStores.CreateConvertofStoresAsync(convertofStores);

            try
            {
                if (result.IsValid) return Ok(new { Message = "Added successfully" });

            }
            catch (Exception ex)
            {

                Log.Error("An error occurred while seeding the database  {Error} {StackTrace} {InnerException} {Source}",
                ex.Message, ex.StackTrace, ex.InnerException, ex.Source);
            }
            return BadRequest("Cannot Save");

        }


        [HttpPut("{IdConvertofStores}")]
        public async Task<IActionResult> UpdateConvertofStoresAsync(int IdConvertofStores, [FromBody] ConvertofStoresT convertofStores)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            var result = await _convertofStores.UpdateConvertofStoresAsync(IdConvertofStores, convertofStores);
            if (!result)
            {
                return BadRequest();

            }


            return Ok();

        }


        [HttpDelete("{IdConvertofStores}")]
        public async Task<IActionResult> DeleteConvertofStoresAsync(int ConvertofStoresId)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            var GetConvertofStoresId = await _convertofStores.DeleteConvertofStoresAsync(ConvertofStoresId);
            if (!GetConvertofStoresId)
            {
                return BadRequest();

            }




            return Ok();
        }
        [HttpGet("ReportConvertofStore/{ProdouctsID}")]
        public IActionResult ReportConvertofStore(int ProdouctsID)
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

                Out = @"D:\PDFCreator\ReportConvertofStore.pdf"
            };
            var objectSettings = new ObjectSettings
            {

                PagesCount = true,
                ProduceForms = true,
                HtmlContent = _ExecuteConvertofStores.GetHTMLString(sqlParms),
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

