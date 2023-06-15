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
            if (imageEntity.Image?.FileName == null || imageEntity.Image.FileName.Length == 0)
            {
                return 0;
            }

            string randomFileName = Guid.NewGuid().ToString();
            string rootPath = Path.Combine(_environment.WebRootPath, "Images", randomFileName + imageEntity.Image.FileName);
            string databaseImagePath = "\\Images\\" + randomFileName + imageEntity.Image.FileName;

            using (FileStream stream = new(rootPath, FileMode.Create))
            {
                await imageEntity.Image.CopyToAsync(stream);
                stream.Close();
            }

            ProfileImage imageData = _context.ProfileImages.FirstOrDefault(e => e.UserId.Equals(imageEntity.UserId))!;
            if (imageData == null)
            {
                ProfileImage userProfileModel = new()
                {
                    UserId = imageEntity.UserId,
                    ImagePath = databaseImagePath
                };
                _context.Add(userProfileModel);
            }
            else
            {
                File.Delete(_environment.WebRootPath + imageData.ImagePath);
                imageData.ImagePath = databaseImagePath;
            }
            _context.SaveChanges();
            return 1;
        }

        public string GetImage(string userId)
        {
            var profileImage = _context.ProfileImages.FirstOrDefault(e => e.UserId.Equals(userId));
            if (profileImage != null && !string.IsNullOrEmpty(profileImage.ImagePath))
            {
                return profileImage.ImagePath;
            }
            else
            {
                return null;
            }
        }

        public int DeleteImage(string userId)
        {
            ProfileImage imageData = _context.ProfileImages.FirstOrDefault(e => e.UserId.Equals(userId))!;
            if (imageData != null)
            {
                File.Delete(_environment.WebRootPath + imageData.ImagePath);
                _context.Remove(imageData);
                _context.SaveChanges();
            }
            return 0;
        }
    }
}