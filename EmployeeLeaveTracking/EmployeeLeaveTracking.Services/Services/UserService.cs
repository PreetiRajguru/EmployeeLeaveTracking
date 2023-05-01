﻿using EmployeeLeaveTracking.Data.Context;
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

        public  IEnumerable<UserRegistrationDTO> GetUsersByManagerId(int managerId)
        {
            var users =  _dbContext.Users
                .Where(u => u.ManagerId == managerId)
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



     
    }
}
