using EmployeeLeaveTracking.Data.DTOs;
using EmployeeLeaveTracking.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeLeaveTracking.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LeaveBalanceController : ControllerBase
    {
        private readonly ILeaveBalance _leaveBalanceService;

        public LeaveBalanceController(ILeaveBalance leaveBalanceService)
        {
            _leaveBalanceService = leaveBalanceService;
        }

        [HttpGet]
        public IEnumerable<LeaveBalanceDTO> GetAllLeaveBalances()
        {
            return _leaveBalanceService.GetAllLeaveBalances();
        }

        [HttpGet("{id}")]
        public LeaveBalanceDTO GetLeaveBalanceById(int id)
        {
            return _leaveBalanceService.GetLeaveBalanceById(id);
        }

        [HttpPost]
        public ActionResult<LeaveBalanceDTO> AddLeaveBalance(LeaveBalanceDTO leaveBalance)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var result = _leaveBalanceService.AddLeaveBalance(leaveBalance);

                return CreatedAtAction(nameof(GetLeaveBalanceById), new { id = result.Id }, result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPut("{id}")]
        public ActionResult<LeaveBalanceDTO> UpdateLeaveBalance(int id, LeaveBalanceDTO leaveBalance)
        {
            try
            {
                if (id != leaveBalance.Id)
                {
                    return BadRequest();
                }

                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var result = _leaveBalanceService.UpdateLeaveBalance(leaveBalance);

                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public ActionResult<bool> DeleteLeaveBalance(int id)
        {
            try
            {
                var result = _leaveBalanceService.DeleteLeaveBalance(id);

                if (result)
                {
                    return Ok(result);
                }

                return NotFound();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
