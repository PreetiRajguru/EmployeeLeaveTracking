using EmployeeLeaveTracking.Data.DTOs;
using EmployeeLeaveTracking.Services.Interfaces;
using EmployeeLeaveTracking.Services.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeLeaveTracking.WebAPI.Controllers
{
    [ApiController]
    [Authorize]
    [Route("api/[controller]")]
    public class LeaveTypeController : ControllerBase
    {
        private readonly ILeaveType _leaveTypeService;

        public LeaveTypeController(ILeaveType leaveTypeService)
        {
            _leaveTypeService = leaveTypeService;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            try
            {
                var leaveTypes = _leaveTypeService.GetAll();
                return Ok(leaveTypes);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while getting leave types: {ex.Message}");
            }
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            try
            {
                var leaveType = _leaveTypeService.GetById(id);
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
        public IActionResult Create([FromBody] LeaveTypeDTO leaveType)
        {

            try
            {
                if (leaveType == null)
                {
                    return BadRequest();
                }
               
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var addedLeaveType = _leaveTypeService.Create(leaveType);
                return CreatedAtAction(nameof(GetById), new { id = addedLeaveType.Id }, addedLeaveType);

            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while adding a new leave type: {ex.Message}");
            }
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] LeaveTypeDTO leaveType)
        {
                try
                {
                    if (leaveType == null || id != leaveType.Id)
                    {
                        return BadRequest();
                    }

                    if (!ModelState.IsValid)
                    {
                        return BadRequest(ModelState);
                    }

                    var updatedLeaveType = _leaveTypeService.Update(leaveType);
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

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                var deleted = _leaveTypeService.Delete(id);
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


        [HttpGet("employee/leavetypes")]
        public ActionResult<List<LeaveTypeWithTotalDaysDTO>> GetLeaveTypesWithTotalDays()
        {
            try
            {
                var leaveTypesWithTotalDays = _leaveTypeService.GetLeaveTypesWithTotalDaysTaken();

                return Ok(leaveTypesWithTotalDays);
            }
            catch(Exception ex) { }
            {
                return NoContent();
            }
        }

    }
}
