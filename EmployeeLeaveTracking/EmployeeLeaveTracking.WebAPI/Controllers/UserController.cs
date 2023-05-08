using EmployeeLeaveTracking.Data.DTOs;
using EmployeeLeaveTracking.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeLeaveTracking.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]

    public class UserController : ControllerBase
    {
        private readonly IUser _userService;

        public UserController(IUser userService)
        {
            _userService = userService;
        }

        /*        [HttpGet]
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
                }*/





        [HttpGet]
        [Route("employees/{managerId}")]
        public ActionResult<IEnumerable<UserRegistrationDTO>> GetUsersByManagerId(string managerId)
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


        [HttpGet("currentuser/{employeeId}")]
        public ActionResult<CurrentUserDTO> GetUserBasicInfo(string employeeId)
        {
            try
            {
                var userBasicInfo = _userService.GetCurrentUser(employeeId);
                if (userBasicInfo == null)
                {
                    return NotFound();
                }
                return Ok(userBasicInfo);
            }
            catch (Exception ex)
            {
                // log error
                return StatusCode(500, ex.Message);
            }
        }



        [HttpGet("{employeeId}/manager")]
        public async Task<ActionResult<string>> GetManagerId(string employeeId)
        {
            try
            {
                var managerId = await _userService.GetManagerIdAsync(employeeId);
                return Ok(managerId);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
    }
}