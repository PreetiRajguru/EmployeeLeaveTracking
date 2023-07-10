using EmployeeLeaveTracking.Data.DTOs;
using EmployeeLeaveTracking.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
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
        [Authorize(Roles = "Manager")]
        public ActionResult<IEnumerable<LeaveRequestDTO>> Get()
        {
            try
            {
                IEnumerable<LeaveRequestDTO> leaveRequests = _leaveRequestService.GetAll();

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
        [Authorize(Roles = "Manager,Employee")]
        public ActionResult<LeaveRequestDTO> GetById(int id)
        {
            try
            {
                LeaveRequestDTO leaveRequest = _leaveRequestService.GetById(id);

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
        [Authorize(Roles = "Manager,Employee")]
        public ActionResult<LeaveRequestDTO> Post(LeaveRequestDTO leaveRequest)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                LeaveRequestDTO newLeaveRequest = _leaveRequestService.Create(leaveRequest);

                return CreatedAtAction(nameof(GetById), new { id = newLeaveRequest.Id }, newLeaveRequest);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost("/new")]
        [Authorize(Roles = "Manager,Employee")]
        public ActionResult<NewLeaveRequestDTO> PostNewLeave(NewLeaveRequestDTO leaveRequest)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                NewLeaveRequestDTO newLeaveRequest = _leaveRequestService.CreateNewLeaveRequest(leaveRequest);

                return CreatedAtAction(nameof(GetById), new { id = newLeaveRequest.Id }, newLeaveRequest);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Manager,Employee")]
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

                LeaveRequestDTO updatedLeaveRequest = _leaveRequestService.Update(leaveRequest);

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
        [Authorize(Roles = "Manager,Employee")]
        public ActionResult<bool> Delete(int id)
        {
            try
            {
                bool isDeleted = _leaveRequestService.Delete(id);

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
        [Authorize(Roles = "Manager,Employee")]
        public IActionResult GetAllLeavesByEmployeeId(int limit , int offset)
        {
            try
            {
                List<UserLeaveRequestDTO> leaves = _leaveRequestService.GetAllLeavesByEmployeeId(limit, offset);

                return Ok(leaves);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }


        [HttpGet("employee/{employeeId}")]
        [Authorize(Roles = "Manager,Employee")]
        public IActionResult LeavesByEmployeeId(string employeeId)
        {
            try
            {
                List<UserLeaveRequestDTO> leaves = _leaveRequestService.LeavesByEmployeeId(employeeId);

                return Ok(leaves);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }


        [HttpGet("status/{statusId}")]
        [Authorize(Roles = "Manager,Employee")]
        public async Task<ActionResult<List<LeaveRequestDTO>>> GetAllLeavesByStatusId(int statusId)
        {
            try
            {
                List<LeaveRequestDTO> leaveRequests = await _leaveRequestService.GetAllLeavesByStatusIdAsync(statusId);

                return Ok(leaveRequests);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }



        [HttpGet("leaves/{statusId}/{managerId}")]
        [Authorize(Roles = "Manager,Employee")]
        public ActionResult<IEnumerable<LeaveRequestDTO>> GetLeavesByStatusAndManager(int statusId, string managerId)
        {
            try
            {
                List<LeaveRequestDTO> leaves = _leaveRequestService.GetLeaveRequestsByStatusAndManager(statusId, managerId);
                return Ok(leaves);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }



        [HttpPut("{id}/status/{statusId}")]
        [Authorize(Roles = "Manager,Employee")]
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
        [Authorize(Roles = "Manager,Employee")]
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

        
        //new method
        [HttpPost("newleaverequest")]
        public IActionResult NewCreateNewLeaveRequest(NewLeaveRequestDTO leaveRequest)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var createdLeaveRequest = _leaveRequestService.NewCreateNewLeaveRequest(leaveRequest);

            return CreatedAtRoute(new { id = createdLeaveRequest.Id }, createdLeaveRequest);
        }
    }
}