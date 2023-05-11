using Microsoft.AspNetCore.Http;

namespace EmployeeLeaveTracking.Data.DTOs
{
    public class ProfileImageUploadDTO
    {
        public string? UserId { get; set; }

        public IFormFile? Image { get; set; }
    }
}