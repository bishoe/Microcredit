using Microcredit.ClassProject.SuppliersSVC;

using Microcredit.Models;
using Microsoft.AspNetCore.Mvc;

namespace Microcredit.Controllers
{
    [Route("api/[controller]")]
    //[ApiController]
    public class SuppliersController : ControllerBase
    {
        private readonly ISuppliers _Suppliers;
        private readonly ApplicationDbContext _db;

        public SuppliersController(ISuppliers Suppliers, ApplicationDbContext db)
        {
            _db = db;
            _Suppliers = Suppliers;

        }
        [HttpGet]
        public async Task<IActionResult> GETALLSuppliersASYNC()
        {
            var suppliers = await _Suppliers.GETALLSuppliersASYNC();

            return Ok(suppliers);

        }

        [HttpGet("{SuppliersID}")]
        public async Task<IActionResult> GETSupplierByidASYNC(int SuppliersID)
        {
            if (SuppliersID == 0) return NotFound();
            var geTSuppliersID = await _Suppliers.GETSupplierByidASYNC(SuppliersID);



            return Ok(geTSuppliersID);
        }

        [HttpPost]
        public async Task<IActionResult> CreateSuppliers([FromBody] SuppliersT suppliers)
        {

            // Will hold all the errors related to registration

            var result = await _Suppliers.CreateSuppliers(suppliers);


            if (result.IsValid)
            {
                // Don't reveal that the user does not exist or is not confirmed
                return Ok(new { Message = "Added successfully" });
            }
            return BadRequest("Cannot Save");


        }
        [HttpPut("{SuppliersID}")]
        public async Task<IActionResult> UpdateSuppliers(int SuppliersID, [FromBody] SuppliersT suppliers)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var result = await _Suppliers.UpdateSuppliers(SuppliersID, suppliers);

            if (!result)
            {
                return BadRequest();
            }

            return NoContent();
        }

    }
}

