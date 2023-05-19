using EmployeeLeaveTracking.Data.DTOs;
using EmployeeLeaveTracking.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
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
       /* [Authorize(Roles = "Manager,Employee")]*/
        public IActionResult GetAll()
        {
            try
            {
                IEnumerable<LeaveTypeDTO> leaveTypes = _leaveTypeService.GetAll();
                return Ok(leaveTypes);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while getting leave types: {ex.Message}");
            }
        }

        [HttpGet("{id}")]
        /*[Authorize(Roles = "Manager,Employee")]*/
        public IActionResult GetById(int id)
        {
            try
            {
                LeaveTypeDTO leaveType = _leaveTypeService.GetById(id);
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

                LeaveTypeDTO addedLeaveType = _leaveTypeService.Create(leaveType);
                return CreatedAtAction(nameof(GetById), new { id = addedLeaveType.Id }, addedLeaveType);

            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while adding a new leave type: {ex.Message}");
            }
        }

        [HttpPut("{id}")]
        /*[Authorize(Roles = "Manager")]*/
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

                LeaveTypeDTO updatedLeaveType = _leaveTypeService.Update(leaveType);
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
       /* [Authorize(Roles = "Manager")]*/
        public IActionResult Delete(int id)
        {
            try
            {
                bool deleted = _leaveTypeService.Delete(id);
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
       /* [Authorize(Roles = "Manager,Employee")]*/
        public ActionResult<List<LeaveTypeWithTotalDaysDTO>> GetLeaveTypesWithTotalDays()
        {
            try
            {
                List<LeaveTypeWithTotalDaysDTO> leaveTypesWithTotalDays = _leaveTypeService.GetLeaveTypesWithTotalDaysTaken();

                return Ok(leaveTypesWithTotalDays);
            }
            catch(Exception ex) { }
            {
                return NoContent();
            }
        }
    }
}