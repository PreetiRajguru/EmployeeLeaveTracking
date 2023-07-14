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
        public IActionResult GetAllLeaveBalances()
        {
            IEnumerable<Data.Models.LeaveBalance> leaveBalances = _leaveBalanceService.GetAllLeaveBalances();

            return Ok(leaveBalances);
        }

        [HttpGet("/employee/{employeeId}")]
        public IActionResult GetLeaveBalancesByEmpId([FromRoute] string employeeId)
        {
            IEnumerable<Data.Models.LeaveBalance> leaveBalances = _leaveBalanceService.GetLeaveBalancesByEmpId(employeeId);
            return Ok(leaveBalances);
        }
    }
}