using EmployeeLeaveTracking.Data.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeLeaveTracking.Services.Interfaces
{
    public interface IProfileImage
    {
        Task<int> UploadImage(ProfileImageUploadDTO imageEntity);
        string GetImage(string userId);
    }
}