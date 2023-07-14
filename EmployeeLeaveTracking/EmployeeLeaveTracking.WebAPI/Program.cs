using AutoMapper;
using EmployeeLeaveTracking.Data.Context;
using EmployeeLeaveTracking.Data.Mappers;
using EmployeeLeaveTracking.Data.Models;
using EmployeeLeaveTracking.Services.Interfaces;
using EmployeeLeaveTracking.Services.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen();

builder.Services.AddIdentity<User, IdentityRole>()
       .AddEntityFrameworkStores<EmployeeLeaveDbContext>()
       .AddDefaultTokenProviders().AddRoles<IdentityRole>(); ;

builder.Services.AddDbContext<EmployeeLeaveDbContext>(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString("ConnStr")));

builder.Services.Configure<ApiBehaviorOptions>(options => { options.SuppressModelStateInvalidFilter = true; });
var mapperConfig = new MapperConfiguration(map =>
{
    map.AddProfile<LeaveRequestProfile>();
    map.AddProfile<LeaveTypeProfile>();
    map.AddProfile<UserProfile>();
    map.AddProfile<NotificationProfile>();
});

builder.Services.AddSingleton(mapperConfig.CreateMapper());

builder.Services.AddControllers();
builder.Services.AddScoped<EmployeeLeaveTracking.Services.Interfaces.ILogger, LoggerService>();
builder.Services.AddScoped<IRepository, RepositoryManagerService>();
builder.Services.AddScoped<IUserAuthentication, UserAuthenticationService>();
builder.Services.AddScoped<ILeaveRequest, LeaveRequestService>();
builder.Services.AddScoped<ILeaveType, LeaveTypeService>();
builder.Services.AddScoped<IStatusMaster, StatusMasterService>();
builder.Services.AddScoped<IUser, UserService>();
builder.Services.AddScoped<UserService>();
builder.Services.AddScoped<IDesignationMaster, DesignationMasterService>();
builder.Services.AddScoped<IProfileImage, ProfileImageService>();
builder.Services.AddScoped<ILeaveBalance, LeaveBalanceService>();
builder.Services.AddScoped<ICompOff, CompOffService>();
builder.Services.AddScoped<IOnDuty, OnDutyService>();
builder.Services.AddScoped<INotification, NotificationService>();


builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", builder =>
    {
        builder.AllowAnyOrigin()
               .AllowAnyMethod()
               .AllowAnyHeader();
    });
});


IConfigurationSection jwtConfig = builder.Configuration.GetSection("JwtConfig");
string secretKey = jwtConfig["secret"];
builder.Services.AddAuthentication(opt =>
{
    opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    opt.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.SaveToken = true;
    options.RequireHttpsMetadata = false;
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = jwtConfig["validIssuer"],
        ValidAudience = jwtConfig["validAudience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey))
    };
});


builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Manager Employee API",
        Version = "v1",
        Description = "Manager Employee API Services.",
        Contact = new OpenApiContact
        {
            Name = "Ajide Habeeb."
        },
    });
    c.ResolveConflictingActions(apiDescriptions => apiDescriptions.First());
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "JWT Authorization header using the Bearer scheme."
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        }
                    },
                    Array.Empty<string>()
                }
            });
});


WebApplication app = builder.Build();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    /*app.UseSwaggerUI();*/
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "EmployeeLeaveTrackingApi V1");
    });
}
app.UseCors("AllowAll");
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.MapFallbackToFile("index.html");
app.Run();