using Microcredit.ClassProject.CustomersSVC;
using Microcredit.Models;
using Microsoft.AspNetCore.Mvc;

namespace Microcredit.Controllers
{
    [Route("api/[controller]")]
    //[ApiController]
    public class CustomersController : ControllerBase
    {
        private readonly ApplicationDbContext _db;
        private readonly ICustomers _customers;
        public CustomersController(ApplicationDbContext db, ICustomers customers)
        {
            _db = db;
            _customers = customers;
        }
        [HttpGet]
        public async Task<IActionResult> GETCustomersAsync()
        {
            var GETCustomers = await _customers.GETCustomersAsync();
            return Ok(GETCustomers);
        }
        [HttpGet("{CustomerId}")]
        public async Task<IActionResult> GETCustomersBYIdAsync(int CustomerId)
        {
            if (CustomerId == 0) return NotFound();

            var GetCustomesById = await _customers.GETCustomersBYIdAsync(CustomerId);

            return Ok(GetCustomesById);

        }
        //[HttpGet]
        //[Authorize]
        //public IEnumerable<string> Get()
        //{
        //    return new string[] { "John Doe", "Jane Doe" };
        //}

        [HttpPost]
        public async Task<IActionResult> CreateCustomers([FromBody] CustomersT customersT)
        {
            var result = await _customers.CreateCustomersAsync(customersT);
            if (result.IsValid || result.Message == "Added successfully")
            {

                return Ok(new { Message = "Added successfully" });

            }
            return BadRequest("Cannot Save");

        }

        [HttpPut("{CustomerId}")]
        public async Task<IActionResult> UpdateCustomers([FromBody] CustomersT customersT, int CustomerId)
        {

            if (!ModelState.IsValid) return BadRequest();

            var result = await _customers.UpdateCustomersAsync(CustomerId, customersT);
            if (!result)
            {
                return BadRequest();
            }


            return NoContent();


        }


        [HttpDelete("{CustomerId}")]
        public async Task<IActionResult> DeleteCustomers(int CustomerId)
        {


            if (!ModelState.IsValid) return BadRequest();

            var GETCustomerBYId = await _customers.DeleteCustomersAsync(CustomerId);
            if (!GETCustomerBYId) return BadRequest();



            return Ok();
        }
    }



}
