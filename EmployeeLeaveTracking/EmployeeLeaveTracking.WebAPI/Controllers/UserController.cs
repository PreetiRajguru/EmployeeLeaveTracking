using EmployeeLeaveTracking.Data.DTOs;
using EmployeeLeaveTracking.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeLeaveTracking.WebAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUser _userService;

        public UserController(IUser userService)
        {
            _userService = userService;
        }

        [HttpGet]
        [Route("employees/{managerId}")]
        public  ActionResult<IEnumerable<UserRegistrationDTO>> GetUsersByManagerId(string managerId)
        {
            try
            {
                var users = _userService.GetUsersByManagerId(managerId);
                return Ok(users);
            }
            catch (Exception ex) 
            {
                return BadRequest();
            }
        }


        [HttpGet]
        [Route("employee/{employeeId}")]
        public ActionResult<IEnumerable<UserRegistrationDTO>> GetUserDetails(string employeeId)
        {
            try
            {
                var user = _userService.GetUserDetails(employeeId);
                return Ok(user);
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
        }
    }
}