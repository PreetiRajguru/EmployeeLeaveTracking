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
            var result = _leaveBalanceService.AddLeaveBalance(leaveBalance);

            return CreatedAtAction(nameof(GetLeaveBalanceById), new { id = result.Id }, result);
        }

        [HttpPut("{id}")]
        public ActionResult<LeaveBalanceDTO> UpdateLeaveBalance(int id, LeaveBalanceDTO leaveBalance)
        {
            if (id != leaveBalance.Id)
            {
                return BadRequest();
            }

            var result = _leaveBalanceService.UpdateLeaveBalance(leaveBalance);

            return Ok(result);
        }

        [HttpDelete("{id}")]
        public ActionResult<bool> DeleteLeaveBalance(int id)
        {
            var result = _leaveBalanceService.DeleteLeaveBalance(id);

            if (result)
            {
                return Ok(result);
            }

            return NotFound();
        }
    }

}
