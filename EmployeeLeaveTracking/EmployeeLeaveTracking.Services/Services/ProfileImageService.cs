using EmployeeLeaveTracking.Data.Context;
using EmployeeLeaveTracking.Data.DTOs;
using EmployeeLeaveTracking.Data.Models;
using EmployeeLeaveTracking.Services.Interfaces;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeLeaveTracking.Services.Services
{
    public class ProfileImageService : IProfileImage
    {
        private readonly EmployeeLeaveDbContext _context;
        private readonly IWebHostEnvironment _environment;
        public ProfileImageService(EmployeeLeaveDbContext context, IWebHostEnvironment environment)
        {
            _context = context;
            _environment = environment;
        }
        public async Task<int> UploadImage([FromForm] ProfileImageUploadDTO imageEntity)
        {
            if (imageEntity.Image.FileName == null || imageEntity.Image.FileName.Length == 0)
            {
                return 0;
            }

            string path = Path.Combine(_environment.WebRootPath,
                                        "Images", Guid.NewGuid().ToString() + imageEntity.Image.FileName);

            using (FileStream stream = new FileStream(path, FileMode.Create))
            {
                await imageEntity.Image.CopyToAsync(stream);
                stream.Close();
            }

            ProfileImage imageData = _context.ProfileImages.FirstOrDefault(e => e.UserId.Equals(imageEntity.UserId))!;
            if (imageData == null)
            {
                ProfileImage userProfileModel = new ProfileImage
                {
                    UserId = imageEntity.UserId,
                    ImagePath = path
                };
                _context.Add(userProfileModel);
            }
            else
            {
                imageData.ImagePath = path;
            }

            User user = _context.Users.FirstOrDefault(e => e.Id.Equals(imageEntity.UserId))!;
            user.ProfilePictureUrl = path;
            _context.SaveChanges();

            return 1;
        }
        public string GetImage(string userId)
        {
            string imagePath = _context.Users.FirstOrDefault(e => e.Id.Equals(userId)).ProfilePictureUrl;
            if (string.IsNullOrEmpty(imagePath))
            {
                return null;
            }
            else
                return imagePath;
        }
    }
}