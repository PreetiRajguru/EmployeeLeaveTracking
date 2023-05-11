using EmployeeLeaveTracking.Data.DTOs;
using EmployeeLeaveTracking.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeLeaveTracking.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ProfileImageController : ControllerBase
    {
        private readonly IWebHostEnvironment _environment;
        private readonly IProfileImage _profileImageService;
        public ProfileImageController(IWebHostEnvironment environment, IProfileImage profileImageService)
        {
            _environment = environment;
            _profileImageService = profileImageService;
        }
        [HttpPost]
        public async Task<IActionResult> Post([FromForm] ProfileImageUploadDTO model)
        {
            try
            {
                await _profileImageService.UploadImage(model);

                if (model == null)
                {
                    return BadRequest("Null");
                }

                return Ok("Upload Successfull ");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred : {ex.Message}");
            }

        }
        [HttpGet]
        public IActionResult Get(string id)
        {
            try
            {
                string result = _profileImageService.GetImage(id);
                if (result == null)
                {
                    return BadRequest("Null");
                }

                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred : {ex.Message}");
            }
        }
    }
}