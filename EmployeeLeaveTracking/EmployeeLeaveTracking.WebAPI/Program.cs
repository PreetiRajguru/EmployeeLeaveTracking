using AutoMapper;
using EmployeeLeaveTracking.Data.Context;
using EmployeeLeaveTracking.Data.Mappers;
using EmployeeLeaveTracking.Data.Models;
using EmployeeLeaveTracking.Services.Interfaces;
using EmployeeLeaveTracking.Services.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StudentTeacher.Service.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


builder.Services.AddIdentity<User, IdentityRole>(o =>
{
    o.Password.RequireDigit = false;
    o.Password.RequireLowercase = false;
    o.Password.RequireUppercase = false;
    o.Password.RequireNonAlphanumeric = false;
    o.User.RequireUniqueEmail = true;
})
       .AddEntityFrameworkStores<EmployeeLeaveDbContext>()
       .AddDefaultTokenProviders();

builder.Services.AddAuthentication();

builder.Services.AddDbContext<EmployeeLeaveDbContext>(options =>

options.UseSqlServer(builder.Configuration.GetConnectionString("ConnStr")));


builder.Services.Configure<ApiBehaviorOptions>(options => { options.SuppressModelStateInvalidFilter = true; });
var mapperConfig = new MapperConfiguration(map =>
{
    map.AddProfile<EmployeeProfile>();
    map.AddProfile<LeaveBalanceProfile>();
    map.AddProfile<LeaveRequestProfile>();
    map.AddProfile<LeaveTypeProfile>();
    map.AddProfile<ManagerProfile>();
    map.AddProfile<UserProfile>();
});
builder.Services.AddSingleton(mapperConfig.CreateMapper());



builder.Services.AddScoped<ILoggerManager, LoggerManager>();
builder.Services.AddScoped<IRepositoryManager, RepositoryManagerService>();
builder.Services.AddScoped<IUserAuthenticationRepository, UserAuthenticationService>();



var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();


app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
