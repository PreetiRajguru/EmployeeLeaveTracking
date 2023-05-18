using EmployeeLeaveTracking.Data.DTOs;
using EmployeeLeaveTracking.Services.Interfaces;
using EmployeeLeaveTracking.Services.Services;
using Microsoft.AspNetCore.Authorization;
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
            var leaveBalances = _leaveBalanceService.GetAllLeaveBalances();

            return Ok(leaveBalances);
        }
    }
}