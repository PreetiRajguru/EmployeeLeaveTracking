using EmployeeLeaveTracking.Data.DTOs;
using EmployeeLeaveTracking.Data.Models;

namespace EmployeeLeaveTracking.Data.Mappers
{
    public class UserDesignationMapper
    {
        public UserRegistrationDTO Map(User entity)
        {
            return new UserRegistrationDTO
            {
                Id = entity.Id,
                FirstName = entity.FirstName,
                LastName = entity.LastName,
                UserName = entity.UserName,
                Email = entity.Email,
                DesignationName = entity.Designation.DesignationName,
            };
        }
    }
}
