using EmployeeLeaveTracking.Data.Context;
using EmployeeLeaveTracking.Scheduler;
using EmployeeLeaveTracking.Services.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace SchedulerJob
{
    public class Program
    {
        public static void Main()
        {
            // creating a service collection and configuring the dependencies
            var services = new ServiceCollection();
            services.AddScoped<EmployeeLeaveDbContext>();
            services.AddScoped<LeaveBalanceService>();
            services.AddScoped<Scheduler>();

            var configuration = new ConfigurationBuilder()
            .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .Build();

            services.AddSingleton<IConfiguration>(configuration);


            // Build the service provider
            var serviceProvider = services.BuildServiceProvider();

            // resolving the required dependencies
            var dbContext = serviceProvider.GetRequiredService<EmployeeLeaveDbContext>();
            var scheduler = serviceProvider.GetRequiredService<Scheduler>();

            string filePath = @"D:\ProjectAdditions\EmployeeLeaveTracking\EmployeeLeaveTracking\EmployeeLeaveTracking.SchedularJob\Logs\LogFile.txt";

            scheduler.LeaveAddition(filePath);
        }
    }
}