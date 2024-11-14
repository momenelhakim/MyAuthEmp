using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyAuthEmp.Models;
using MyAuthEmp.Services;

namespace MyAuthEmp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly EmployeeServices _employeeServices;

        public EmployeeController(EmployeeServices employeeServices)
        {
            _employeeServices = employeeServices;
        }
        [HttpGet]
    
        public async Task<ActionResult<List<EmployeeDto>>> GetAll(int pageNumber, int pageSize)
        {
            var employees = await _employeeServices.GetAllAsync(pageNumber, pageSize);
            return Ok(employees);
        }


        [HttpGet("{id}")]
        [Authorize(Policy = "RequireAdminRole")]
        public async Task<ActionResult<EmployeeDto>> Get(int id)
        {
            var employee = await _employeeServices.GetAsync(id);
            if (employee == null)
                return NotFound();
            return Ok(employee);
        }


        [HttpPost]
        public async Task<ActionResult<Employee>> Create(Employee employee   )
        {

            await _employeeServices.AddAsync(employee);


            return CreatedAtAction(nameof(Get), new { id = employee.EmployeeId }, employee);
        }


        [HttpPut("{id}")]
        public async Task<ActionResult<EmployeeDto>> Update(int id, EmployeeDto employeeDto)
        {

            var updatedEmployee = await _employeeServices.UpdateAsync(id, employeeDto);
            if (updatedEmployee == null)
                return NotFound();
            return Ok(updatedEmployee);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var deletedEmployee = await _employeeServices.DeleteAsync(id);
            if (deletedEmployee == null)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}
