using EmployeeLeaveTracking.Data.Context;
using EmployeeLeaveTracking.Data.DTOs;
using EmployeeLeaveTracking.Data.Models;
using EmployeeLeaveTracking.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeLeaveTracking.Services.Services
{
    public class LeaveBalanceService : ILeaveBalance
    {
        private readonly EmployeeLeaveDbContext _dbContext;

        public LeaveBalanceService(EmployeeLeaveDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IEnumerable<LeaveBalanceDTO> GetAllLeaveBalances()
        {
            return _dbContext.LeaveBalances
                .Where(lb => lb.IsDeleted == (false))
                .Select(lb => new LeaveBalanceDTO
                {
                    Id = lb.Id,
                    EmployeeId = lb.EmployeeId,
                    LeaveTypeId = lb.LeaveTypeId,
                    Balance = lb.Balance,
                    YearMonth = lb.YearMonth
                })
                .ToList();
        }

        public LeaveBalanceDTO GetLeaveBalanceById(int id)
        {
            var leaveBalance = _dbContext.LeaveBalances
                .SingleOrDefault(lb => lb.Id == id && lb.IsDeleted == (false));

            if (leaveBalance == null)
            {
                return null;
            }

            return new LeaveBalanceDTO
            {
                Id = leaveBalance.Id,
                EmployeeId = leaveBalance.EmployeeId,
                LeaveTypeId = leaveBalance.LeaveTypeId,
                Balance = leaveBalance.Balance,
                YearMonth = leaveBalance.YearMonth
            };
        }

        public LeaveBalanceDTO AddLeaveBalance(LeaveBalanceDTO leaveBalance)
        {
            var newLeaveBalance = new LeaveBalance
            {
                EmployeeId = leaveBalance.EmployeeId,
                LeaveTypeId = leaveBalance.LeaveTypeId,
                Balance = leaveBalance.Balance,
                YearMonth = leaveBalance.YearMonth
            };

            _dbContext.LeaveBalances.Add(newLeaveBalance);
            _dbContext.SaveChanges();

            leaveBalance.Id = newLeaveBalance.Id;

            return leaveBalance;
        }

        public LeaveBalanceDTO UpdateLeaveBalance(LeaveBalanceDTO leaveBalance)
        {
            var existingLeaveBalance = _dbContext.LeaveBalances
                .SingleOrDefault(lb => lb.Id == leaveBalance.Id && lb.IsDeleted == (false));

            if (existingLeaveBalance == null)
            {
                return null;
            }

            existingLeaveBalance.EmployeeId = leaveBalance.EmployeeId;
            existingLeaveBalance.LeaveTypeId = leaveBalance.LeaveTypeId;
            existingLeaveBalance.Balance = leaveBalance.Balance;
            existingLeaveBalance.YearMonth = leaveBalance.YearMonth;

            _dbContext.SaveChanges();

            return leaveBalance;
        }

        public bool DeleteLeaveBalance(int id)
        {
            var leaveBalance = _dbContext.LeaveBalances
                .SingleOrDefault(lb => lb.Id == id && lb.IsDeleted == (false));

            if (leaveBalance == null)
            {
                return false;
            }

            leaveBalance.IsDeleted = true;

            _dbContext.SaveChanges();

            return true;
        }
    }

}
