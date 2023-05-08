using EmployeeLeaveTracking.Data.DTOs;

namespace EmployeeLeaveTracking.Services.Interfaces
{
    public interface IUser
    {
        IEnumerable<UserRegistrationDTO> GetUsersByManagerId(string managerId);

        IEnumerable<UserRegistrationDTO> GetUserDetails(string employeeId);

        public CurrentUserDTO GetCurrentUser(string employeeId);

        Task<string> GetManagerIdAsync(string employeeId);





        Task<UserRegistrationDTO> UpdateUser(UserRegistrationDTO user);
    }
}
