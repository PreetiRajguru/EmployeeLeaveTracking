using AutoMapper;
using EmployeeLeaveTracking.Data.Context;
using EmployeeLeaveTracking.Data.DTOs;
using EmployeeLeaveTracking.Data.Models;
using EmployeeLeaveTracking.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace EmployeeLeaveTracking.Services.Services;

public sealed class UserAuthenticationService : IUserAuthentication
{
    private readonly UserManager<User> _userManager;
    private readonly IConfiguration _configuration;
    private readonly IMapper _mapper;
    private User? _user;
    private readonly RoleManager<IdentityRole> _roleManager;
    private readonly EmployeeLeaveDbContext _context;

    public UserAuthenticationService(UserManager<User> userManager, IConfiguration configuration, IMapper mapper, RoleManager<IdentityRole> roleManager,
                                   EmployeeLeaveDbContext context)
    {
        _userManager = userManager;
        _configuration = configuration;
        _mapper = mapper;
        _roleManager = roleManager;
        _context = context;
    }

    public async Task<IdentityResult> RegisterUserAsync(NewUserDTO userRegistration)
    {
        User user = _mapper.Map<User>(userRegistration);

        IdentityResult result = await _userManager.CreateAsync(user, userRegistration.Password);

        if (result.Succeeded)
        {


            if (!await _roleManager.RoleExistsAsync("Manager"))
            {
                IdentityRole managerRole = new IdentityRole("Manager");
                await _roleManager.CreateAsync(managerRole);
            }

            if (!await _roleManager.RoleExistsAsync("Employee"))
            {
                IdentityRole employeeRole = new IdentityRole("Employee");
                await _roleManager.CreateAsync(employeeRole);
            }

            if (userRegistration.ManagerId == String.Empty)
            {
                await _userManager.AddToRoleAsync(user, "Manager");
            }
            else
            {
                await _userManager.AddToRoleAsync(user, "Employee");
            }
        }


        //initial leave balances for the new user
        var leaveTypes = await _context.LeaveTypes.ToListAsync();
        var leaveBalances = new List<LeaveBalance>();
        var compOffs = new List<CompOff>();
        var onDutys = new List<OnDuty>();

        foreach (var leaveType in leaveTypes)
        {
            double balance = 0;

            switch (leaveType.LeaveTypeName)
            {
                case "Unpaid Leave":
                    balance = 30;
                    break;
                case "Paid Leave":
                    balance = 1.5;
                    break;
                case "Compensatory Off":
                    balance = 0;
                    break;
                case "Work From Home":
                    balance = 1;
                    break;
                case "Forgot Id Card":
                    balance = 0;
                    break;
                case "On Duty":
                    balance = 0;
                    break;
                default:
                    break;
            }

            var leaveBalance = new LeaveBalance
            {
                UserId = user.Id,
                LeaveTypeId = leaveType.Id,
                Balance = balance
            };

            leaveBalances.Add(leaveBalance);

        }

        //initial comp-off leaves for the new user

        var compOff = new CompOff
        {
            UserId = user.Id,
            Balance = 0,
            WorkedDate = DateTime.Now,
            Reason = " ",
        };

        compOffs.Add(compOff);

        //initial on-duty leaves for the new user

        var onDuty = new OnDuty
        {
            UserId = user.Id,
            Balance = 0,
            WorkedDate = DateTime.Now,
            Reason = " ",
        };

        onDutys.Add(onDuty);

        await _context.LeaveBalances.AddRangeAsync(leaveBalances);
        await _context.CompOffs.AddRangeAsync(compOffs);
        await _context.OnDutys.AddRangeAsync(onDutys);
        await _context.SaveChangesAsync();
        return result;

    }

    public async Task<LoginResponseDTO> ValidateUserAsync(UserLoginDTO loginDto)
    {
        _user = await _userManager.FindByNameAsync(loginDto.UserName);

        if (_user != null)
        {
            var token = await CreateTokenAsync();
            var refreshToken = GenerateRefreshToken();
            _ = int.TryParse(_configuration["JwtConfig:RefreshTokenValidityInDays"], out int refreshTokenValidityInDays);

            _user.RefreshToken = refreshToken;
            _user.RefreshTokenExpiryTime = DateTime.Now.AddDays(refreshTokenValidityInDays);

            await _userManager.UpdateAsync(_user);

            if (await _userManager.CheckPasswordAsync(_user, loginDto.Password))
            {
                return new LoginResponseDTO
                {
                    Token = token,
                    RefreshToken = refreshToken,
                    Role = GetRoles().Result?[0],
                    Id = GetUserId()
                };
            }
        }

        return null;
    }


    public async Task<string> CreateTokenAsync()
    {
        SigningCredentials signingCredentials = GetSigningCredentials();
        List<Claim> claims = await GetClaims();
        JwtSecurityToken tokenOptions = GenerateTokenOptions(signingCredentials, claims);
        return new JwtSecurityTokenHandler().WriteToken(tokenOptions);
    }

    private SigningCredentials GetSigningCredentials()
    {
        IConfigurationSection jwtConfig = _configuration.GetSection("JwtConfig");
        byte[] key = Encoding.UTF8.GetBytes(jwtConfig["secret"]);
        SymmetricSecurityKey secret = new(key);
        return new SigningCredentials(secret, SecurityAlgorithms.HmacSha256);
    }

    private async Task<List<Claim>> GetClaims()
    {
        List<Claim> claims = new List<Claim>();

        if (_user != null)
        {
            claims.Add(new Claim(ClaimTypes.Name, _user.UserName));
            claims.Add(new Claim(ClaimTypes.Sid, _user.Id));

            if (_userManager != null && _user != null)
            {
                IList<string> roles = await _userManager.GetRolesAsync(_user);
                if (roles != null)
                {
                    foreach (var role in roles)
                    {
                        claims.Add(new Claim(ClaimTypes.Role, role));
                    }
                }
            }
        }

        return claims;
    }


    public Task<IList<string>> GetRoles()
    {
        return _userManager.GetRolesAsync(_user);
    }

    public string GetUserId()
    {
        return _user.Id;
    }

    private JwtSecurityToken GenerateTokenOptions(SigningCredentials signingCredentials, List<Claim> claims)
    {
        IConfigurationSection jwtSettings = _configuration.GetSection("JwtConfig");
        JwtSecurityToken tokenOptions = new(
        issuer: jwtSettings["validIssuer"],
        audience: jwtSettings["validAudience"],
        claims: claims,
        expires: DateTime.Now.AddMinutes(Convert.ToDouble(jwtSettings["expiresIn"])),
        signingCredentials: signingCredentials
        );
        return tokenOptions;
    }

    public string GenerateRefreshToken()
    {
        var randomNumber = new byte[64];
        using var rng = RandomNumberGenerator.Create();
        rng.GetBytes(randomNumber);
        return Convert.ToBase64String(randomNumber);
    }

    public ClaimsPrincipal? GetPrincipalFromExpiredToken(string? token)
    {
        var tokenValidationParameters = new TokenValidationParameters
        {
            ValidateAudience = false,
            ValidateIssuer = false,
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JwtConfig:secret"])),
            ValidateLifetime = false
        };

        var tokenHandler = new JwtSecurityTokenHandler();
        var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out SecurityToken securityToken);
        if (securityToken is not JwtSecurityToken jwtSecurityToken || !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
            throw new SecurityTokenException("Invalid token");

        return principal;
    }

    public JwtSecurityToken CreateToken(List<Claim> authClaims)
    {
        var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JwtConfig:Secret"]));
        _ = int.TryParse(_configuration["JwtConfig:TokenValidityInMinutes"], out int tokenValidityInMinutes);

        var token = new JwtSecurityToken(
            issuer: _configuration["JwtConfig:ValidIssuer"],
            audience: _configuration["JwtConfig:ValidAudience"],
            expires: DateTime.Now.AddMinutes(tokenValidityInMinutes),
            claims: authClaims,
            signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
            );

        return token;
    }
}