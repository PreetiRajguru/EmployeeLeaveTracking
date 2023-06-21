using EmployeeLeaveTracking.Data.DTOs;
using EmployeeLeaveTracking.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

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
        public IActionResult CreateOnDuty(OnDutyDTO onDutyDTO)
        {
            var createdOnDuty = _onDutyService.CreateOnDuty(onDutyDTO);
            if (createdOnDuty != null)
                return Ok(createdOnDuty);

            return BadRequest("Failed to create On Duty.");
        }


        [HttpGet("{userId}")]
        public IActionResult GetOnDuty(string userId)
        {
            var onDuty = _onDutyService.GetOnDuty(userId);
            if (onDuty != null)
                return Ok(onDuty);

            return NotFound("On Duty not found.");
        }


        [HttpPut]
        public IActionResult UpdateOnDuty(OnDutyDTO onDutyDTO)
        {
            var updatedOnDuty = _onDutyService.UpdateOnDuty(onDutyDTO);
            if (updatedOnDuty != null)
                return Ok(updatedOnDuty);

            return NotFound("On Duty not found.");
        }


        [HttpDelete("{userId}")]
        public IActionResult DeleteOnDuty(string userId)
        {
            _onDutyService.DeleteOnDuty(userId);
            return NoContent();
        }
    }
}
