using EmployeeLeaveTracking.Data.DTOs;
using EmployeeLeaveTracking.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace EmployeeLeaveTracking.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OnDutyController : Controller
    {
        private readonly IOnDuty _onDutyService;

        public OnDutyController(IOnDuty onDutyService)
        {
            _onDutyService = onDutyService;
        }

        [HttpPost]
        [Authorize(Roles = "Manager")]
        public IActionResult CreateOnDuty(LeaveAdditionDTO onDutyDTO)
        {
            LeaveAdditionDTO createdOnDuty = _onDutyService.CreateOnDuty(onDutyDTO);
            if (createdOnDuty != null)
                return Ok(createdOnDuty);

            return BadRequest("Failed to create On Duty.");
        }


        [HttpGet("{userId}")]
        [Authorize(Roles = "Employee")]
        public IActionResult GetOnDuty(string userId)
        {
            LeaveAdditionDTO onDuty = _onDutyService.GetOnDuty(userId);
            if (onDuty != null)
                return Ok(onDuty);

            return NotFound("On Duty not found.");
        }


        [HttpPut]
        [Authorize(Roles = "Manager,Employee")]
        public IActionResult UpdateOnDuty(LeaveAdditionDTO onDutyDTO)
        {
            LeaveAdditionDTO updatedOnDuty = _onDutyService.UpdateOnDuty(onDutyDTO);
            if (updatedOnDuty != null)
                return Ok(updatedOnDuty);

            return NotFound("On Duty not found.");
        }


        [HttpDelete("{userId}")]
        [Authorize(Roles = "Manager")]
        public IActionResult DeleteOnDuty(string userId)
        {
            _onDutyService.DeleteOnDuty(userId);
            return NoContent();
        }
    }
}
