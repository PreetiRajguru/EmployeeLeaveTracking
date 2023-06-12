using Microsoft.AspNetCore.Identity;
using EmployeeLeaveTracking.Data.DTOs;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;

namespace EmployeeLeaveTracking.Services.Interfaces;

public interface IUserAuthentication
{
    Task<IdentityResult> RegisterUserAsync(NewUserDTO userForRegistration);

    Task<LoginResponseDTO> ValidateUserAsync(UserLoginDTO loginDto);

    Task<string> CreateTokenAsync();

    string GenerateRefreshToken();

    ClaimsPrincipal? GetPrincipalFromExpiredToken(string? token);

    public JwtSecurityToken CreateToken(List<Claim> authClaims);

    Task<IList<string>> GetRoles();

    public string GetUserId();
}