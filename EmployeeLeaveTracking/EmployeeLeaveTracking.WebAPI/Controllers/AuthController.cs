using AutoMapper;
using EmployeeLeaveTracking.Data.DTOs;
using EmployeeLeaveTracking.Data.Models;
using EmployeeLeaveTracking.Services.Interfaces;
using EmployeeLeaveTracking.WebAPI.Controllers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.IdentityModel.Tokens.Jwt;

namespace EmployeeLeaveTracking.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : BaseApiController
    {
        private readonly UserManager<User> _userManager;
        private readonly IConfiguration _configuration;

        public AuthController(UserManager<User> userManager, IConfiguration configuration, IRepository repository, EmployeeLeaveTracking.Services.Interfaces.ILogger logger, IMapper mapper) 
            : base(repository, logger, mapper)
        {
             _userManager = userManager;
            _configuration = configuration;
        }

        [HttpPost("register")]
        public async Task<IActionResult> RegisterUser([FromBody] NewUserDTO userRegistration)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                IdentityResult userResult = await _repository.UserAuthentication.RegisterUserAsync(userRegistration);
                return !userResult.Succeeded ? new BadRequestObjectResult(userResult) : StatusCode(201);

            }
            catch (Exception ex)
            {
                _logger.LogError($"Error registering user: {ex.Message}");
                return StatusCode(500, "An error occurred while registering user");
            }
        }


        [HttpPost("login")]
        public async Task<IActionResult> Authenticate([FromBody] UserLoginDTO user)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                LoginResponseDTO validate =await _repository.UserAuthentication.ValidateUserAsync(user);
                return validate == null
                    ? Unauthorized()
                    : Ok(validate);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error authenticating user: {ex.Message}");
                return StatusCode(500, "An error occurred while authenticating user");
            }
        }


        [HttpPost]
       
        [Route("refresh-token")]
        public async Task<IActionResult> RefreshToken(TokenModel tokenModel)
            {
            if (tokenModel is null)
            {
                return BadRequest("Invalid client request");
            }

            string? accessToken = tokenModel.AccessToken;
            string? refreshToken = tokenModel.RefreshToken;

            var principal = _repository.UserAuthentication.GetPrincipalFromExpiredToken(accessToken);
            if (principal == null)
            {
                return BadRequest("Invalid access token or refresh token");
            }

            #pragma warning disable CS8600 // Converting null literal or possible null value to non-nullable type.
            #pragma warning disable CS8602 // Dereference of a possibly null reference.
            string username = principal.Identity.Name;
            #pragma warning restore CS8602 // Dereference of a possibly null reference.
            #pragma warning restore CS8600 // Converting null literal or possible null value to non-nullable type.

            var user = await _userManager.FindByNameAsync(username);

            if (user == null || user.RefreshToken != refreshToken || user.RefreshTokenExpiryTime <= DateTime.Now)
            {
                return BadRequest("Invalid access token or refresh token");
            }

            var newAccessToken = _repository.UserAuthentication.CreateToken(principal.Claims.ToList());
            var newRefreshToken = _repository.UserAuthentication.GenerateRefreshToken();

            user.RefreshToken = newRefreshToken;
            await _userManager.UpdateAsync(user);

            return new ObjectResult(new
            {
                accessToken = new JwtSecurityTokenHandler().WriteToken(newAccessToken),
                refreshToken = newRefreshToken
            });
        }

        [HttpPost]
        [Route("revoke/{username}")]
        public async Task<IActionResult> Revoke(string username)
        {
            var user = await _userManager.FindByNameAsync(username);
            if (user == null) return BadRequest("Invalid user name");

            user.RefreshToken = null;
            await _userManager.UpdateAsync(user);

            return NoContent();
        }

        [HttpPost]
        [Route("revoke-all")]
        public async Task<IActionResult> RevokeAll()
        {
            var users = _userManager.Users.ToList();
            foreach (var user in users)
            {
                user.RefreshToken = null;
                await _userManager.UpdateAsync(user);
            }

            return NoContent();
        }


        /*
                [HttpPost]
                public async Task<IActionResult> ChangePasswordHash(string userId, string newPasswordHash)
                {
                    var user = await _userManager.FindByIdAsync(userId);
                    if (user == null)
                    {
                        return NotFound();
                    }

                    *//*var newPassword = _userManager.PasswordHasher.HashPassword(user,newPasswordHash);*//*

                    user.PasswordHash = newPasswordHash;
                    var result = await _userManager.UpdateAsync(user);
                    if (!result.Succeeded)
                    {
                        foreach (var error in result.Errors)
                        {
                            ModelState.AddModelError("", error.Description);
                        }

                        return BadRequest(ModelState);
                    }

                    return Ok();
                }*/







        /*  [HttpPost]
          public async Task<IActionResult> ChangePasswordHash(string userId, string newPassword)
          {
              var user = await _userManager.FindByIdAsync(userId);
              if (user == null)
              {
                  return NotFound();
              }

              var newPasswordHash = _userManager.PasswordHasher.HashPassword(user, newPassword);

              user.PasswordHash = newPassword;
              var result = await _userManager.UpdateAsync(user);
              if (!result.Succeeded)
              {
                  foreach (var error in result.Errors)
                  {
                      ModelState.AddModelError("", error.Description);
                  }

                  return BadRequest(ModelState);
              }

              return Ok();
          }
  */



        [HttpPost]
        [Route("{userid}/{currentpassword}/{newpassword}")]
        public async Task<IActionResult> ChangePassword(string userId, string currentPassword, string newPassword)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return NotFound();
            }

            var passwordValid = await _userManager.CheckPasswordAsync(user, currentPassword);
            if (!passwordValid)
            {
                ModelState.AddModelError("", "Invalid current password.");
                return BadRequest(ModelState);
            }

            var newPasswordHash = _userManager.PasswordHasher.HashPassword(user, newPassword);
            user.PasswordHash = newPasswordHash;

            var result = await _userManager.UpdateAsync(user);
            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }

                return BadRequest(ModelState);
            }

            return Ok();
        }






















        /* [HttpPost]
         public async Task<IActionResult> ChangePassword(User usermodel)
         {
             var user = await _userManager.FindByIdAsync(usermodel.Id);
             if (user == null)
             {
                 return NotFound();
             }
             user.PasswordHash = _userManager.PasswordHasher.HashPassword(usermodel,usermodel.PasswordHash);
             var result = await _userManager.UpdateAsync(user);
             if (!result.Succeeded)
             {
                 //throw exception......
             }
             return Ok();
         }*/


        /*


                [HttpPost]
                public async Task<IActionResult> changePassword(UsercredentialsModel usermodel)
                {
                    ApplicationUser user = await AppUserManager.FindByIdAsync(usermodel.Id);
                    if (user == null)
                    {
                        return NotFound();
                    }
                    user.PasswordHash = AppUserManager.PasswordHasher.HashPassword(usermodel.Password);
                    var result = await AppUserManager.UpdateAsync(user);
                    if (!result.Succeeded)
                    {
                        //throw exception......
                    }
                    return Ok();
                }*/


    }
}
