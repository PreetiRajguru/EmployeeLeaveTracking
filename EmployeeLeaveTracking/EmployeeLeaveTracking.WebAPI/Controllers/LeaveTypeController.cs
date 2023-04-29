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
            try
            {
                var leaveTypes = _leaveTypeService.GetAllLeaveTypes();
                return Ok(leaveTypes);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while getting leave types: {ex.Message}");
            }
        }

        [HttpGet("{id}")]
        public IActionResult GetLeaveTypeById(int id)
        {
            try
            {
                var leaveType = _leaveTypeService.GetLeaveTypeById(id);
                if (leaveType == null)
                {
                    return NotFound();
                }
                return Ok(leaveType);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while getting leave type by ID {id}: {ex.Message}");
            }
        }

        [HttpPost]
        public IActionResult AddLeaveType([FromBody] LeaveTypeDTO leaveType)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    if (leaveType == null)
                    {
                        return BadRequest();
                    }

                    var addedLeaveType = _leaveTypeService.AddLeaveType(leaveType);
                    return CreatedAtAction(nameof(GetLeaveTypeById), new { id = addedLeaveType.Id }, addedLeaveType);
                }
                catch (Exception ex)
                {
                    return StatusCode(500, $"An error occurred while adding a new leave type: {ex.Message}");
                }
            }
            return BadRequest(ModelState);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateLeaveType(int id, [FromBody] LeaveTypeDTO leaveType)
        {
            if (ModelState.IsValid)
            {
                try
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
                catch (Exception ex)
                {
                    return StatusCode(500, $"An error occurred while updating leave type with ID {id}: {ex.Message}");
                }
            }
            return BadRequest(ModelState);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteLeaveType(int id)
        {
            try
            {
                var deleted = _leaveTypeService.DeleteLeaveType(id);
                if (deleted)
                {
                    return NoContent();
                }
                return NotFound();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while deleting leave type with ID {id}: {ex.Message}");
            }
        }
    }
}
