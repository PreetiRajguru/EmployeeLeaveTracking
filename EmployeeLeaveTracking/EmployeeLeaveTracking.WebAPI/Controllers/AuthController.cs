﻿using AutoMapper;
using EmployeeLeaveTracking.Data.DTOs;
using EmployeeLeaveTracking.Data.Models;
using EmployeeLeaveTracking.Services.Interfaces;
using EmployeeLeaveTracking.WebAPI.Controllers;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace StudentTeacher.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : BaseApiController
    {
        public AuthController(IRepository repository, EmployeeLeaveTracking.Services.Interfaces.ILogger logger, IMapper mapper) 
            : base(repository, logger, mapper)
        {
        }

        [HttpPost("register")]
        public async Task<IActionResult> RegisterUser([FromBody] UserRegistrationDTO userRegistration)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var userResult = await _repository.UserAuthentication.RegisterUserAsync(userRegistration);
                return !userResult.Succeeded ? new BadRequestObjectResult(userResult) : StatusCode(201);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error registering user: {ex.Message}");
                return StatusCode(500, "An error occurred while registering user");
            }
        }


       /* [Authorize]*/
        [HttpPost("login")]
        public async Task<IActionResult> Authenticate([FromBody] UserLoginDTO user)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }


/*                await _userManager.GetUserId*/



                var ddd = await _repository.UserAuthentication.ValidateUserAsync(user);
                return !ddd
                    ? Unauthorized()
                    : Ok(new { Token = await _repository.UserAuthentication.CreateTokenAsync() , Role = _repository.UserAuthentication.GetRoles().Result , Id = _repository.UserAuthentication.GetUserId() });
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error authenticating user: {ex.Message}");
                return StatusCode(500, "An error occurred while authenticating user");
            }
        }
    }
}
