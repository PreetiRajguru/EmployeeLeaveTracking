using EmployeeLeaveTracking.Data.Context;
using EmployeeLeaveTracking.Data.Models;
using EmployeeLeaveTracking.Services.Services;
using Microsoft.Extensions.Configuration;

namespace EmployeeLeaveTracking.Scheduler
{
    public class Scheduler
    {
        public readonly EmployeeLeaveDbContext _dbContext;
        private readonly LeaveBalanceService _leaveBalanceService;
        private readonly IConfiguration _configuration;

        public Scheduler(EmployeeLeaveDbContext dbContext, LeaveBalanceService leaveBalanceService, IConfiguration configuration)
        {
            _dbContext = dbContext;
            _leaveBalanceService = leaveBalanceService;
            _configuration = configuration;
        }

        public void LeaveAddition(string filePath)
        {
            double leaveTypeId1Balance = _configuration.GetValue<double>("LeaveBalanceSettings:LeaveTypeId1Balance");
            double leaveTypeId4Balance = _configuration.GetValue<double>("LeaveBalanceSettings:LeaveTypeId4Balance");

            IEnumerable<LeaveBalance> balances = _leaveBalanceService.GetAllLeaveBalances();

            Console.WriteLine("Fetched All Leave Balances");

            using StreamWriter writer = new(filePath);

            foreach (LeaveBalance balance in balances)
            {
                try
                {
                    writer.WriteLine("User Id: " + balance.UserId);
                    writer.WriteLine("Previous Balance: " + balance.Balance);

                    if (balance.UserId == "cd7d9e27-52da-4626-b7bd-8c97d0d0d7c6")
                    {
                        throw new Exception();
                    }

                    if (balance.LeaveTypeId == 1)
                    {
                        balance.Balance += leaveTypeId1Balance;
                    }
                    else if (balance.LeaveTypeId == 4)
                    {
                        balance.Balance += leaveTypeId4Balance;
                    }

                    writer.WriteLine("Updated Balance: " + balance.Balance);
                }
                catch (Exception ex)
                {
                    writer.WriteLine($"Error occurred for User ID: {balance.UserId}");
                    writer.WriteLine($"Exception Message: {ex.Message}");
                    writer.WriteLine($"Exception Stack Trace: {ex.StackTrace}");
                    writer.WriteLine($"Exception Occurred At: {DateTime.UtcNow}");
                    Console.WriteLine($"Error occurred for User ID: {balance.UserId}");
                    Console.WriteLine($"Exception Message: {ex.Message}");
                    Console.WriteLine($"Exception Stack Trace: {ex.StackTrace}");
                    Console.WriteLine($"Exception Occurred At: {DateTime.UtcNow}");
                    continue;
                }
            }

            _dbContext.LeaveBalances.UpdateRange(balances);
            Console.WriteLine("Updated All Leave Balances");

            _dbContext.SaveChanges();
            Console.WriteLine("Saved All Leave Balances to the Database");
        }
    }
}