using EmployeeLeaveTracking.Data.Context;
using EmployeeLeaveTracking.Data.DTOs;
using EmployeeLeaveTracking.Data.Models;
using EmployeeLeaveTracking.Services.Interfaces;

namespace EmployeeLeaveTracking.Services.Services
{
    public class OnDutyService : IOnDuty
    {
        private readonly EmployeeLeaveDbContext _dbContext;

        public OnDutyService(EmployeeLeaveDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public OnDutyDTO CreateOnDuty(OnDutyDTO onDutyDTO)
        {
            var onDuty = new OnDuty
            {
                UserId = onDutyDTO.UserId,
                Balance = onDutyDTO.Balance,
                WorkedDate = (DateTime)onDutyDTO.WorkedDate,
                Reason = onDutyDTO.Reason
            };

            // calculating and setting the expiry date
            onDuty.ExpiryDate = ((DateTime)onDuty.WorkedDate).AddDays(45);


            _dbContext.OnDutys.Add(onDuty);
            _dbContext.SaveChanges();

            return onDutyDTO;
        }

        public OnDutyDTO GetOnDuty(string userId)
        {
            var onDuty = _dbContext.OnDutys.FirstOrDefault(c => c.UserId == userId);
            if (onDuty == null)
                return null;

            var onDutyDTO = new OnDutyDTO
            {
                UserId = onDuty.UserId,
                Balance = onDuty.Balance,
                WorkedDate = onDuty.WorkedDate,
                Reason = onDuty.Reason
            };

            return onDutyDTO;
        }


        public OnDutyDTO UpdateOnDuty(OnDutyDTO onDutyDTO)
        {
            var onDuty = _dbContext.OnDutys.FirstOrDefault(c => c.UserId == onDutyDTO.UserId);
            if (onDuty == null)
                return null;

            onDuty.Balance = onDutyDTO.Balance;
            onDuty.WorkedDate = (DateTime)onDutyDTO.WorkedDate;
            onDuty.Reason = onDutyDTO.Reason;

            // Calculate and update the expiry date
            onDuty.ExpiryDate = ((DateTime)onDuty.WorkedDate).AddDays(45);

            // Update LeaveBalance model
            var leaveBalance = _dbContext.LeaveBalances.FirstOrDefault(l => l.UserId == onDutyDTO.UserId && l.LeaveTypeId == 6);
            if (leaveBalance != null)
            {
                leaveBalance.Balance = (double)onDutyDTO.Balance;
                _dbContext.SaveChanges();
            }

            _dbContext.SaveChanges();

            return onDutyDTO;
        }


        public void DeleteOnDuty(string userId)
        {
            var onDuty = _dbContext.OnDutys.FirstOrDefault(c => c.UserId == userId);
            if (onDuty != null)
            {
                _dbContext.OnDutys.Remove(onDuty);
                _dbContext.SaveChanges();
            }
        }
    }
}