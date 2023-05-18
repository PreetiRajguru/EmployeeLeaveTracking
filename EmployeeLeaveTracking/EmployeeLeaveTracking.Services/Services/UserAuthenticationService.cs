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
using System.Text;

namespace StudentTeacher.Service.Services;

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

        foreach (var leaveType in leaveTypes)
        {
            double balance = 0;
           /* DateTime dt = DateTime.Now;
            int month = dt.Month;*/

            switch (leaveType.LeaveTypeName)
            {
                case "Unpaid Leave":
                    balance = 30;
                    break;
                case "Paid Leave":
                    /*balance = month * 1.5;*/
                    balance = 1.5;
                    break;
                case "Compensatory Off":
                    balance = 0;
                    break;
                case "Work From Home":
                    /*balance = month * 1;*/
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

        await _context.LeaveBalances.AddRangeAsync(leaveBalances);
        await _context.SaveChangesAsync();
        return result;

    }

    public async Task<bool> ValidateUserAsync(UserLoginDTO loginDto)
    {
        _user = await _userManager.FindByNameAsync(loginDto.UserName);
        bool result = _user != null && await _userManager.CheckPasswordAsync(_user, loginDto.Password);
        return result;
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
        SymmetricSecurityKey secret = new SymmetricSecurityKey(key);
        return new SigningCredentials(secret, SecurityAlgorithms.HmacSha256);
    }

    private async Task<List<Claim>> GetClaims()
    {
        List<Claim> claims = new List<Claim>
        {
            new Claim(ClaimTypes.Name, _user.UserName),
            new Claim(ClaimTypes.Sid, _user.Id)
        };
        IList<string> roles = await _userManager.GetRolesAsync(_user);
        foreach (var role in roles)
        {
            claims.Add(new Claim(ClaimTypes.Role, role));
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
        JwtSecurityToken tokenOptions = new JwtSecurityToken
        (
        issuer: jwtSettings["validIssuer"],
        audience: jwtSettings["validAudience"],
        claims: claims,
        expires: DateTime.Now.AddMinutes(Convert.ToDouble(jwtSettings["expiresIn"])),
        signingCredentials: signingCredentials
        );
        return tokenOptions;
    }
}