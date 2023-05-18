using EmployeeLeaveTracking.Data.Context;
using EmployeeLeaveTracking.Data.Models;
using Microsoft.EntityFrameworkCore;
using Quartz;
using Quartz.Impl;
using System.Configuration;

namespace EmployeeLeaveTrackingCron
{
    public class LeaveBalanceJob : IJob
    {


        private readonly EmployeeLeaveDbContext _context;

        public LeaveBalanceJob(EmployeeLeaveDbContext context)
        {
            _context = context;
        }


        public async Task Execute(IJobExecutionContext context)
        {
            //initial leave balances for all users
            var users = await _context.Users.ToListAsync();

            foreach (var user in users)
            {
                var leaveTypes = await _context.LeaveTypes.ToListAsync();
                var leaveBalances = new List<LeaveBalance>();

                foreach (var leaveType in leaveTypes)
                {
                    double balance = 0;

                    switch (leaveType.LeaveTypeName)
                    {
                        case "Unpaid Leave":
                            balance = 30;
                            break;
                        case "Paid Leave":
                            DateTime userCreationDate = (DateTime)user.CreatedDate;
                            int monthsSinceCreation = (DateTime.Now.Year - userCreationDate.Year) * 12 + (DateTime.Now.Month - userCreationDate.Month);
                            balance = monthsSinceCreation * 1.5;
                            break;
                        case "Compensatory Off":
                            balance = 0;
                            break;
                        case "Work From Home":
                            userCreationDate = (DateTime)user.CreatedDate;
                            monthsSinceCreation = (DateTime.Now.Year - userCreationDate.Year) * 12 + (DateTime.Now.Month - userCreationDate.Month);
                            balance = monthsSinceCreation * 1;
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
            }

            await _context.SaveChangesAsync();
        }
    }
}