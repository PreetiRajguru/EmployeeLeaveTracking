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
            var leaveRequests = _leaveRequestService.GetAllLeaveRequests();

            if (leaveRequests == null || leaveRequests.Count() == 0)
            {
                return NoContent();
            }

            return Ok(leaveRequests);
        }

        [HttpGet("{id}")]
        public ActionResult<LeaveRequestDTO> GetById(int id)
        {
            var leaveRequest = _leaveRequestService.GetLeaveRequestById(id);

            if (leaveRequest == null)
            {
                return NotFound();
            }

            return Ok(leaveRequest);
        }

        [HttpPost]
        public ActionResult<LeaveRequestDTO> Post(LeaveRequestDTO leaveRequest)
        {
            var newLeaveRequest = _leaveRequestService.AddLeaveRequest(leaveRequest);

            return CreatedAtAction(nameof(GetById), new { id = newLeaveRequest.Id }, newLeaveRequest);
        }

        [HttpPut("{id}")]
        public ActionResult<LeaveRequestDTO> Put(int id, LeaveRequestDTO leaveRequest)
        {
            if (id != leaveRequest.Id)
            {
                return BadRequest();
            }

            var updatedLeaveRequest = _leaveRequestService.UpdateLeaveRequest(leaveRequest);

            if (updatedLeaveRequest == null)
            {
                return NotFound();
            }

            return Ok(updatedLeaveRequest);
        }

        [HttpDelete("{id}")]
        public ActionResult<bool> Delete(int id)
        {
            var isDeleted = _leaveRequestService.DeleteLeaveRequest(id);

            if (!isDeleted)
            {
                return NotFound();
            }

            return Ok(true);
        }
    }

}
