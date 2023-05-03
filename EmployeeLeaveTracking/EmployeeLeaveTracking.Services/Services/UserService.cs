using EmployeeLeaveTracking.Data.Context;
using EmployeeLeaveTracking.Data.DTOs;
using EmployeeLeaveTracking.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace EmployeeLeaveTracking.Services.Services
{
    public class UserService : IUser
    {
        private readonly EmployeeLeaveDbContext _dbContext;

        public UserService(EmployeeLeaveDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IEnumerable<UserRegistrationDTO> GetUsersByManagerId(string managerId)
        {
            var users = _dbContext.Users
                .Where(u => u.ManagerId.Equals(managerId))
                .Select(u => new UserRegistrationDTO
                {
                    FirstName = u.FirstName,
                    LastName = u.LastName,
                    UserName = u.UserName,
                    Email = u.Email,
                    PhoneNumber = u.PhoneNumber,
                    ManagerId = u.ManagerId
                });

            return users;
        }

        public IEnumerable<UserRegistrationDTO> GetUserDetails(string employeeId)
        {
            var user = _dbContext.Users
                .Where(u => u.Id.Equals(employeeId))
                .Select(u => new UserRegistrationDTO
                {
                    FirstName = u.FirstName,
                    LastName = u.LastName,
                    UserName = u.UserName,
                    Email = u.Email,
                    PhoneNumber = u.PhoneNumber,
                    ManagerId = u.ManagerId
                });

            return user;
        }

    }
}
