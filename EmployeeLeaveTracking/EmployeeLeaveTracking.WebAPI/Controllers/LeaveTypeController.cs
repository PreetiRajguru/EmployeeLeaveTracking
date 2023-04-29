using EmployeeLeaveTracking.Data.DTOs;
using EmployeeLeaveTracking.Services.Interfaces;
using EmployeeLeaveTracking.Services.Services;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeLeaveTracking.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LeaveTypeController : ControllerBase
    {
        private readonly ILeaveType _leaveTypeService;

        public LeaveTypeController(ILeaveType leaveTypeService)
        {
            _leaveTypeService = leaveTypeService;
        }

        [HttpGet]
        public IActionResult GetAllLeaveTypes()
        {
            var leaveTypes = _leaveTypeService.GetAllLeaveTypes();
            return Ok(leaveTypes);
        }

        [HttpGet("{id}")]
        public IActionResult GetLeaveTypeById(int id)
        {
            var leaveType = _leaveTypeService.GetLeaveTypeById(id);
            if (leaveType == null)
            {
                return NotFound();
            }
            return Ok(leaveType);
        }

        [HttpPost]
        public IActionResult AddLeaveType([FromBody] LeaveTypeDTO leaveType)
        {
            if (leaveType == null)
            {
                return BadRequest();
            }

            var addedLeaveType = _leaveTypeService.AddLeaveType(leaveType);
            return CreatedAtAction(nameof(GetLeaveTypeById), new { id = addedLeaveType.Id }, addedLeaveType);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateLeaveType(int id, [FromBody] LeaveTypeDTO leaveType)
        {
            if (leaveType == null || id != leaveType.Id)
            {
                return BadRequest();
            }

            var updatedLeaveType = _leaveTypeService.UpdateLeaveType(leaveType);
            if (updatedLeaveType == null)
            {
                return NotFound();
            }
            return Ok(updatedLeaveType);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteLeaveType(int id)
        {
            var deleted = _leaveTypeService.DeleteLeaveType(id);
            if (deleted)
            {
                return NoContent();
            }
            return NotFound();
        }
    }

}
