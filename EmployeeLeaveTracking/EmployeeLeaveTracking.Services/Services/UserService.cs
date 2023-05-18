using EmployeeLeaveTracking.Data.Context;
using EmployeeLeaveTracking.Data.DTOs;
using EmployeeLeaveTracking.Data.Models;
using EmployeeLeaveTracking.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
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

        public UserService()
        {
        }

        public IEnumerable<UserRegistrationDTO> GetUsersByManagerId(string managerId)
        {
            if (string.IsNullOrEmpty(managerId))
            {
                throw new ArgumentException("Manager Id cannot be null or empty");
            }

            IQueryable<UserRegistrationDTO> users = _dbContext.Users
                   .Where(u => u.ManagerId.Equals(managerId))
                   .Include(lr => lr.Designation)
                   .Select(u => new UserRegistrationDTO
                   {
                       Id = u.Id,
                       FirstName = u.FirstName,
                       LastName = u.LastName,
                       UserName = u.UserName,
                       Email = u.Email,
                       PhoneNumber = u.PhoneNumber,
                       ManagerId = u.ManagerId,
                       DesignationName = u.Designation.DesignationName
                   });

            return users;
        }


        public IEnumerable<UserRegistrationDTO> GetUserDetails(string employeeId)
        {
            if (string.IsNullOrWhiteSpace(employeeId))
            {
                throw new ArgumentException("Employee Id cannot be null or whitespace.", nameof(employeeId));
            }
            IQueryable<UserRegistrationDTO> user = _dbContext.Users
                .Where(u => u.Id.Equals(employeeId))
                .Select(u => new UserRegistrationDTO
                {
                    FirstName = u.FirstName,
                    LastName = u.LastName,
                    UserName = u.UserName,
                    Email = u.Email,
                    PhoneNumber = u.PhoneNumber,
                    ManagerId = u.ManagerId,
                    DesignationId = u.DesignationId
                });
            if (!user.Any())
            {
                throw new ArgumentException($"User with ID {employeeId} not found.");
            }

            return user;
        }

        public string GetCurrentUserById()
        {
            string? userId = _httpContextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.Sid);

            if (string.IsNullOrEmpty(userId))
            {
                throw new Exception("User ID not found in HttpContext");
            }

            return userId;
        }


        public CurrentUserDTO GetCurrentUser(string employeeId)
        {
            if (string.IsNullOrWhiteSpace(employeeId))
            {
                throw new ArgumentException("Employee Id cannot be null or whitespace.", nameof(employeeId));
            }

            Data.Models.User? user = _dbContext.Users
                .FirstOrDefault(u => u.Id == employeeId);

            if (user == null)
            {
                throw new ArgumentException($"User with ID {employeeId} not found.");
            }

            return new CurrentUserDTO
            {
                FirstName = user.FirstName,
                LastName = user.LastName
            };
        }



        public async Task<string> GetManagerIdAsync(string employeeId)
        {
            Data.Models.User employee = await _dbContext.Users
                .FirstOrDefaultAsync(e => e.Id == employeeId);

            if (employee == null)
            {
                throw new ArgumentException("Invalid employee ID");
            }

            return employee.ManagerId;
        }

        public async Task<UserRegistrationDTO> UpdateUser(UserRegistrationDTO user)
        {
            Data.Models.User existingUser = await _dbContext.Users.FindAsync(user.Id);

            if (existingUser == null)
            {
                throw new ArgumentException($"User with id {user.Id} does not exist");
            }

            existingUser.FirstName = user.FirstName;
            existingUser.LastName = user.LastName;
            existingUser.UserName = user.UserName;
            existingUser.Email = user.Email;
            existingUser.PhoneNumber = user.PhoneNumber;
            existingUser.ManagerId = user.ManagerId;
            existingUser.DesignationId = user.DesignationId;

            await _dbContext.SaveChangesAsync();

            return user;
        }
    }
}