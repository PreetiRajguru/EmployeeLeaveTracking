using EmployeeLeaveTracking.Data.DTOs;
using EmployeeLeaveTracking.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeLeaveTracking.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LeaveRequestController : ControllerBase
    {
        private readonly ILeaveRequest _leaveRequestService;
        protected readonly Services.Interfaces.ILogger _logger;

        public LeaveRequestController(ILeaveRequest leaveRequestService, Services.Interfaces.ILogger logger)
        {
            _leaveRequestService = leaveRequestService;
            _logger = logger;
        }

        [HttpGet]
        public ActionResult<IEnumerable<LeaveRequestDTO>> Get()
        {
            try
            {
                var leaveRequests = _leaveRequestService.GetAll();

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
                var leaveRequest = _leaveRequestService.GetById(id);

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

                var newLeaveRequest = _leaveRequestService.Create(leaveRequest);

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

                var updatedLeaveRequest = _leaveRequestService.Update(leaveRequest);

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
                var isDeleted = _leaveRequestService.Delete(id);

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


        [HttpGet("employeeId/{limit}/{offset}")]
        public IActionResult GetAllLeavesByEmployeeId(int limit , int offset)
        {
            try
            {
                var leaves = _leaveRequestService.GetAllLeavesByEmployeeId(limit, offset);

                return Ok(leaves);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }




        [HttpGet("employee/{employeeId}")]
        public IActionResult LeavesByEmployeeId(string employeeId)
        {
            try
            {
                var leaves = _leaveRequestService.LeavesByEmployeeId(employeeId);

                return Ok(leaves);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }


        [HttpGet("status/{statusId}")]
        public async Task<ActionResult<List<LeaveRequestDTO>>> GetAllLeavesByStatusId(int statusId)
        {
            try
            {
                var leaveRequests = await _leaveRequestService.GetAllLeavesByStatusIdAsync(statusId);

                return Ok(leaveRequests);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }



        [HttpGet("leaves/{statusId}/{managerId}")]
        public ActionResult<IEnumerable<LeaveRequestDTO>> GetLeavesByStatusAndManager(int statusId, string managerId)
        {
            try
            {
                var leaves = _leaveRequestService.GetLeaveRequestsByStatusAndManager(statusId, managerId);
                return Ok(leaves);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }


        [HttpPut("{id}/status/{statusId}")]
        public async Task<int> UpdateLeaveRequestStatus(int id, int statusId)
        {
            try
            {
                int result = await _leaveRequestService.UpdateLeaveRequestStatus(id, statusId);

                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError("An error occurred while updating the leave request status.");

                throw;
            }
        }


        [HttpGet("balance/{employeeId}")]
        public IActionResult LeaveBalance(string employeeId)
        {
            try
            {
                double balance = _leaveRequestService
                    .LeaveBalance(employeeId);

                return Ok(balance);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}