using EmployeeLeaveTracking.Data.DTOs;
using EmployeeLeaveTracking.Services.Interfaces;
using EmployeeLeaveTracking.Services.Services;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeLeaveTracking.WebAPI.Controllers
{
    [ApiController]
    [Route("api/employees")]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployee _employeeService;

        public EmployeeController(IEmployee employeeService)
        {
            _employeeService = employeeService;
        }

        [HttpGet]
        public IEnumerable<EmployeeDTO> GetAllEmployees()
        {
            return _employeeService.GetAllEmployees();
        }

        [HttpGet("{id}")]
        public EmployeeDTO GetEmployeeById(int id)
        {
            return _employeeService.GetEmployeeById(id);
        }

        [HttpPost]
        public ActionResult<EmployeeDTO> CreateEmployee(EmployeeDTO employeeDto)
        {
            var employee = _employeeService.CreateEmployee(employeeDto);

            return CreatedAtAction(nameof(GetEmployeeById), new { id = employee.Id }, employee);
        }

        [HttpPut("{id}")]
        public ActionResult<EmployeeDTO> UpdateEmployee(int id, EmployeeDTO employeeDto)
        {
            if (id != employeeDto.Id)
            {
                return BadRequest("Id mismatch between route parameter and request body.");
            }

            try
            {
                var updatedEmployee = _employeeService.UpdateEmployee(employeeDto);

                return Ok(updatedEmployee);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public ActionResult<bool> DeleteEmployee(int id)
        {
            try
            {
                var result = _employeeService.DeleteEmployee(id);

                if (result)
                {
                    return Ok(true);
                }
                else
                {
                    return StatusCode(500, "An error occurred while deleting the employee.");
                }
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}



