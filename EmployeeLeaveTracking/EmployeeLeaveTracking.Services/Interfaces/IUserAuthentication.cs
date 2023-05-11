using Microsoft.AspNetCore.Identity;
using EmployeeLeaveTracking.Data.DTOs;

namespace EmployeeLeaveTracking.Services.Interfaces;

public interface IUserAuthentication
{
    Task<IdentityResult> RegisterUserAsync(NewUserDTO userForRegistration);

    Task<bool> ValidateUserAsync(UserLoginDTO loginDto);

    Task<string> CreateTokenAsync();

    Task<IList<string>> GetRoles();

    public string GetUserId();
}