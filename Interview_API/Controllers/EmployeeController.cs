using Interview_API.DTOs;
using Interview_API.Entities;
using Interview_API.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Interview_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeService _employeeService;
        public EmployeeController(IEmployeeService employeeService)
        {
            _employeeService = employeeService;            
        }
        //post: api/employee
        [HttpPost]
        public async Task<ActionResult<ResponseEmployee>> CreateEmployee([FromBody] CreateEmployeeRequest request)
        {
            if (!ModelState.IsValid)
            { 
                return BadRequest(ModelState);
            }
          
               var response = await _employeeService.CreateEmployeeAsync(request);
                
                return CreatedAtAction(
                    nameof(GetEmployeeById),
                    new { id = response.EmployeeId },
                    response);
           
        }

        //get: api/employee/id
        [HttpGet("{id:int}")]
        public async Task<ActionResult<ResponseEmployee?>> GetEmployeeById(int id)
        {
            var response = await _employeeService.GetEmployeeByIdAsync(id);
            if (response == null)
                return NotFound();
            return Ok(response);
        }

        //put:api/employee

        [HttpPut]
        public async Task<ActionResult<ResponseEmployee?>> UpdateEmployee([FromBody] UpdateEmployee request)
        {
            var response = await _employeeService.UpdateEmployeeAsync(request);
            if (response == null)
                return NotFound();

            return Ok(response);

        }
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteEmployee(int id)
        {
            var deleted = await _employeeService.DeleteEmployeeAsync(id);
            if (!deleted)
                return NotFound();

            return NoContent();
        }

        //pagination 
        //get: api/employee?pageNumber=1&pageSize=10

        
        
        [HttpGet]
        public async Task<ActionResult<PageResponse<ResponseEmployee>>> GetEmployeePage([FromQuery]EmployeeQueryParameters parameters)
        {
            var result = await _employeeService.GetEmployeeAsync(parameters);
            return Ok(result);
        }
    }
}
