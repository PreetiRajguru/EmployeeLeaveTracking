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
        public IEnumerable<LeaveBalanceDTO> GetAll()
        {
            return _leaveBalanceService.GetAll();
        }

        [HttpGet("{id}")]
        public LeaveBalanceDTO GetLeaveBalanceById(int id)
        {
            return _leaveBalanceService.GetById(id);
        }

        [HttpPost]
        public ActionResult<LeaveBalanceDTO> Create(LeaveBalanceDTO leaveBalance)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var result = _leaveBalanceService.Create(leaveBalance);

                return CreatedAtAction(nameof(GetLeaveBalanceById), new { id = result.Id }, result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPut("{id}")]
        public ActionResult<LeaveBalanceDTO> Update(int id, LeaveBalanceDTO leaveBalance)
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

                var result = _leaveBalanceService.Update(leaveBalance);

                return Ok(result);
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
                var result = _leaveBalanceService.Delete(id);

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
