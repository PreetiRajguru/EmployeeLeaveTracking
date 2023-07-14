using EmployeeLeaveTracking.Data.DTOs;
using EmployeeLeaveTracking.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;

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
        [Authorize(Roles = "Manager")]
        public IActionResult CreateCompOff(CompOffDTO compOffDTO)
        {
            CompOffDTO createdCompOff = _compOffService.CreateCompOff(compOffDTO);
            if (createdCompOff != null)
                return Ok(createdCompOff);

            return BadRequest("Failed to create comp off.");
        }

        [HttpGet("{userId}")]
        [Authorize(Roles = "Employee")]
        public IActionResult GetCompOff(string userId)
        {
            CompOffDTO compOff = _compOffService.GetCompOff(userId);
            if (compOff != null)
                return Ok(compOff);

            return NotFound("Comp off not found.");
        }

        [HttpPut]
        [Authorize(Roles = "Manager,Employee")]
        public IActionResult UpdateCompOff(CompOffDTO compOffDTO)
        {
            CompOffDTO updatedCompOff = _compOffService.UpdateCompOff(compOffDTO);
            if (updatedCompOff != null)
                return Ok(updatedCompOff);

            return NotFound("Comp off not found.");
        }

        [HttpDelete("{userId}")]
        [Authorize(Roles = "Manager")]
        public IActionResult DeleteCompOff(string userId)
        {
            _compOffService.DeleteCompOff(userId);
            return NoContent();
        }
    }
}
