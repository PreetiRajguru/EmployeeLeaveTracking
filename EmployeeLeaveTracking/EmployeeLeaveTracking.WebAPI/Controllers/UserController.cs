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

        [HttpGet]
        [Route("employees/{managerId}")]
        public ActionResult<IEnumerable<UserRegistrationDTO>> GetUsersByManagerId(string managerId)
        {
            try
            {
                IEnumerable<UserRegistrationDTO> users = _userService.GetUsersByManagerId(managerId);
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
                IEnumerable<UserRegistrationDTO> user = _userService.GetUserDetails(employeeId);
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
                CurrentUserDTO userBasicInfo = _userService.GetCurrentUser(employeeId);
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
                string managerId = await _userService.GetManagerIdAsync(employeeId);
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


        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUser(string id, [FromBody] UserRegistrationDTO user)
        {
            if (user == null)
            {
                return BadRequest("User object is null");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest("Invalid model object");
            }

            if (string.IsNullOrEmpty(id))
            {
                return BadRequest("User id is null or empty");
            }

            user.Id = id;

            try
            {
                UserRegistrationDTO updatedUser = await _userService.UpdateUser(user);

                return Ok(updatedUser);
            }
            catch (ArgumentException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}