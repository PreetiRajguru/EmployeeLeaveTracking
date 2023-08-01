using EmployeeLeaveTracking.Data.DTOs;

namespace EmployeeLeaveTracking.Services.Interfaces
{
    public interface IUser
    {
        IEnumerable<UserRegistrationDTO> GetUsersByManagerId(string managerId);

        IEnumerable<UserRegistrationDTO> GetUserDetails(string employeeId);

        IEnumerable<UserRegistrationDTO> GetManagerDetails(string employeeId);

        public CurrentUserDTO GetCurrentUser(string employeeId);

        public UpdateProfileDTO GetCurrentUserDetails(string employeeId);

        Task<string> GetManagerIdAsync(string employeeId);

        Task<UpdateProfileDTO> UpdateUserProfile(UpdateProfileDTO user);
    }
}