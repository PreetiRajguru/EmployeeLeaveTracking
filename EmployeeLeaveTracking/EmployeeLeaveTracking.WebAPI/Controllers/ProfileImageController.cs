using EmployeeLeaveTracking.Data.DTOs;
using EmployeeLeaveTracking.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeLeaveTracking.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [Authorize(Roles = "Manager, Employee")]
    [ApiController]
    public class ProfileImageController : ControllerBase
    {
        private readonly IProfileImage _profileImageService;
        public ProfileImageController(IProfileImage profileImageService)
        {
            _profileImageService = profileImageService;
        }


        [HttpPost]
        [Authorize(Roles = "Manager,Employee")]
        public async Task<IActionResult> Post([FromForm] ProfileImageUploadDTO model)
        {
            try
            {
                await _profileImageService.UploadImage(model);
                if (model == null)
                {
                    return BadRequest();
                }
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpGet("{id}")]
        [Authorize(Roles = "Manager,Employee")]
        public IActionResult Get(string id)
        {
            try
            {
                string result = _profileImageService.GetImage(id);
                if (result == null)
                {
                    return BadRequest();
                }
                
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
        [HttpDelete("{id}")]
        [Authorize(Roles = "Manager,Employee")]
        public IActionResult Delete(string id)
        {
            try
            {
                int result = _profileImageService.DeleteImage(id);
                if (result.Equals(0))
                {
                    return Ok();
                }
                return BadRequest();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
    }
}