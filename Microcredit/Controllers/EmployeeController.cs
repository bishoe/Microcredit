using Microcredit.ClassProject;

using Microcredit.Models;
using Microsoft.AspNetCore.Mvc;

namespace Microcredit.Controllers
{
    [Route("api/[controller]")]
    //[ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly ApplicationDbContext _db;
        private readonly IEmployee _employee;
        public EmployeeController(ApplicationDbContext db, IEmployee employee)
        {
            _db = db;
            _employee = employee;
        }

        [HttpGet]
        public async Task<IActionResult> c()
        {
            var GETEmployees = await _employee.GETEmployeesAsync();
            return Ok(GETEmployees);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetEmployeesByIdAsync(int EmployeeId)
        {
            if (EmployeeId == 0) return NotFound();

            var GetEmployeeId = await _employee.GetEmployeesByIdAsync(EmployeeId);

            return Ok(GetEmployeeId);

        }
        [HttpPost]
        public async Task<IActionResult> CreateEmployeesAsync([FromBody] EmployeesT employees)
        {
            var result = await _employee.CreateEmployeesAsync(employees);
            if (result.IsValid)
            {

                return Ok(new { Message = "Added successfully" });
            }
            return BadRequest("Cannot Save");

        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateEmployeesAsync([FromBody] EmployeesT employees, int EmployeeId)
        {

            if (!ModelState.IsValid) return BadRequest();

            var result = await _employee.UpdateEmployeesAsync(EmployeeId, employees);
            if (!result)
                return BadRequest();
            return NoContent();
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEmployeesAsync(int EmployeeId)
        {


            if (!ModelState.IsValid) return BadRequest();

            var GETEmployeeId = await _employee.DeleteEmployeesAsync(EmployeeId);
            if (!GETEmployeeId) return BadRequest();



            return Ok();
        }
    }



}
