using EmployeeLeaveTracking.Data.DTOs;
using EmployeeLeaveTracking.Services.Interfaces;
using EmployeeLeaveTracking.Services.Services;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeLeaveTracking.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CompOffController : Controller
    {
        private readonly ICompOff _compOffService;

        public CompOffController(ICompOff compOffService)
        {
            _compOffService = compOffService;
        }

        [HttpPost]
        public IActionResult CreateCompOff(CompOffDTO compOffDTO)
        {
            var createdCompOff = _compOffService.CreateCompOff(compOffDTO);
            if (createdCompOff != null)
                return Ok(createdCompOff);

            return BadRequest("Failed to create comp off.");
        }

        [HttpGet("{userId}")]
        public IActionResult GetCompOff(string userId)
        {
            var compOff = _compOffService.GetCompOff(userId);
            if (compOff != null)
                return Ok(compOff);

            return NotFound("Comp off not found.");
        }

        [HttpPut]
        public IActionResult UpdateCompOff(CompOffDTO compOffDTO)
        {
            var updatedCompOff = _compOffService.UpdateCompOff(compOffDTO);
            if (updatedCompOff != null)
                return Ok(updatedCompOff);

            return NotFound("Comp off not found.");
        }

        [HttpDelete("{userId}")]
        public IActionResult DeleteCompOff(string userId)
        {
            _compOffService.DeleteCompOff(userId);
            return NoContent();
        }
    }
}
