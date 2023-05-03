using EmployeeLeaveTracking.Data.DTOs;

namespace EmployeeLeaveTracking.Services.Interfaces
{
    public interface IUser
    {
        IEnumerable<UserRegistrationDTO> GetUsersByManagerId(string managerId);

        IEnumerable<UserRegistrationDTO> GetUserDetails(string employeeId);
    }
}
