using EmployeeLeaveTracking.Data.Context;
using EmployeeLeaveTracking.Data.Models;
using EmployeeLeaveTracking.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace EmployeeLeaveTracking.Services.Services
{
    public class LeaveBalanceService : ILeaveBalance
    {
        private readonly EmployeeLeaveDbContext _dbContext;

        public LeaveBalanceService()
        {
        }

        public LeaveBalanceService(EmployeeLeaveDbContext dbContext) 
        {
            _dbContext = dbContext;
        }

        public IEnumerable<LeaveBalance> GetAllLeaveBalances()
        {
            return _dbContext.LeaveBalances
                .Include(u => u.Employee)
                .Where(e => e.IsDeleted == false)
                .Select(lb => new LeaveBalance()
                {
                    Id = lb.Id,
                    UserId = lb.UserId,
                    LeaveTypeId = lb.LeaveTypeId,
                    Balance = lb.Balance,
                    ModifiedDate= lb.ModifiedDate
                })
                .ToList();
        }


        public IEnumerable<LeaveBalance> GetLeaveBalancesByEmpId(string employeeId)
        {
            return _dbContext.LeaveBalances
                .Include(u => u.Employee)
                .Where(e => e.IsDeleted == false && e.UserId == employeeId)
                .Select(lb => new LeaveBalance()
                {
                    Id = lb.Id,
                    UserId = lb.UserId,
                    LeaveTypeId = lb.LeaveTypeId,
                    Balance = lb.Balance,
                    ModifiedDate = lb.ModifiedDate
                })
                .ToList();
        }
    }
}