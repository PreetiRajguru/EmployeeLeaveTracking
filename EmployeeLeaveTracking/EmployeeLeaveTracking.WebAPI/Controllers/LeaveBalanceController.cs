using EmployeeLeaveTracking.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using EmailService;
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
        [Authorize(Roles = "Manager,Employee")]
        public IActionResult GetAllLeaveBalances()
        {
            IEnumerable<Data.Models.LeaveBalance> leaveBalances = _leaveBalanceService.GetAllLeaveBalances();

            return Ok(leaveBalances);
        }

        [HttpGet("/employee/{employeeId}")]
        [Authorize(Roles = "Manager,Employee")]
        public IActionResult GetLeaveBalancesByEmpId([FromRoute] string employeeId)
        {
            IEnumerable<Data.Models.LeaveBalance> leaveBalances = _leaveBalanceService.GetLeaveBalancesByEmpId(employeeId);
            return Ok(leaveBalances);
        }
    }
}