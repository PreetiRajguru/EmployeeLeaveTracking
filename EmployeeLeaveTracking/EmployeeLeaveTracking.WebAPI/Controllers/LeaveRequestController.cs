using EmployeeLeaveTracking.Data.DTOs;
using EmployeeLeaveTracking.Services.Interfaces;
using EmployeeLeaveTracking.Services.Services;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeLeaveTracking.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LeaveRequestController : ControllerBase
    {
        private readonly ILeaveRequest _leaveRequestService;

        public LeaveRequestController(ILeaveRequest leaveRequestService)
        {
            _leaveRequestService = leaveRequestService;
        }

        [HttpGet]
        public ActionResult<IEnumerable<LeaveRequestDTO>> Get()
        {
            try
            {
                var leaveRequests = _leaveRequestService.GetAllLeaveRequests();

                if (leaveRequests == null || leaveRequests.Count() == 0)
                {
                    return NoContent();
                }

                return Ok(leaveRequests);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("{id}")]
        public ActionResult<LeaveRequestDTO> GetById(int id)
        {
            try
            {
                var leaveRequest = _leaveRequestService.GetLeaveRequestById(id);

                if (leaveRequest == null)
                {
                    return NotFound();
                }

                return Ok(leaveRequest);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost]
        public ActionResult<LeaveRequestDTO> Post(LeaveRequestDTO leaveRequest)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var newLeaveRequest = _leaveRequestService.AddLeaveRequest(leaveRequest);

                return CreatedAtAction(nameof(GetById), new { id = newLeaveRequest.Id }, newLeaveRequest);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPut("{id}")]
        public ActionResult<LeaveRequestDTO> Put(int id, LeaveRequestDTO leaveRequest)
        {
            try
            {
                if (id != leaveRequest.Id)
                {
                    return BadRequest();
                }

                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var updatedLeaveRequest = _leaveRequestService.UpdateLeaveRequest(leaveRequest);

                if (updatedLeaveRequest == null)
                {
                    return NotFound();
                }

                return Ok(updatedLeaveRequest);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public ActionResult<bool> Delete(int id)
        {
            try
            {
                var isDeleted = _leaveRequestService.DeleteLeaveRequest(id);

                if (!isDeleted)
                {
                    return NotFound();
                }

                return Ok(true);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
