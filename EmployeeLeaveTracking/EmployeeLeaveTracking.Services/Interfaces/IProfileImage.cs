using EmployeeLeaveTracking.Data.DTOs;

namespace EmployeeLeaveTracking.Services.Interfaces
{
    public interface IProfileImage
    {
        Task<int> UploadImage(ProfileImageUploadDTO imageEntity);
        string GetImage(string userId);
        int DeleteImage(string userId);
    }
}