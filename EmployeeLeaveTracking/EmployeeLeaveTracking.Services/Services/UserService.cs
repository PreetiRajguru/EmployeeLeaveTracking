using EmployeeLeaveTracking.Data.Context;
using EmployeeLeaveTracking.Data.DTOs;
using EmployeeLeaveTracking.Services.Interfaces;
using Microsoft.AspNetCore.Http;

using System.Security.Claims;

namespace EmployeeLeaveTracking.Services.Services
{
    public class UserService : IUser
    {
        private readonly EmployeeLeaveDbContext _dbContext;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UserService(EmployeeLeaveDbContext dbContext, IHttpContextAccessor httpContextAccessor)
        {
            _dbContext = dbContext;
            _httpContextAccessor = httpContextAccessor;
        }

        public IEnumerable<UserRegistrationDTO> GetUsersByManagerId(string managerId)
        {
            if (string.IsNullOrEmpty(managerId))
            {
                throw new ArgumentException("Manager ID cannot be null or empty");
            }

            var users = _dbContext.Users
                   .Where(u => u.ManagerId.Equals(managerId))
                   .Select(u => new UserRegistrationDTO
                   {
                       Id = u.Id,
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
            if (string.IsNullOrWhiteSpace(employeeId))
            {
                throw new ArgumentException("Employee ID cannot be null or whitespace.", nameof(employeeId));
            }
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
            if (!user.Any())
            {
                throw new ArgumentException($"User with ID {employeeId} not found.");
            }

            return user;
        }

        public string GetCurrentUserById()
        {
            var userId = _httpContextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.Sid);

            if (string.IsNullOrEmpty(userId))
            {
                throw new Exception("User ID not found in HttpContext");
            }

            return userId;
        }
    }
}
