using EmployeeLeaveTracking.Data.Context;
using EmployeeLeaveTracking.Data.DTOs;
using EmployeeLeaveTracking.Data.Models;
using EmployeeLeaveTracking.Services.Interfaces;

namespace EmployeeLeaveTracking.Services.Services
{
    public class CompOffService : ICompOff
    {
        private readonly EmployeeLeaveDbContext _dbContext;

        public CompOffService(EmployeeLeaveDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public CompOffDTO CreateCompOff(CompOffDTO compOffDTO)
        {
            var compOff = new CompOff
            {
                UserId = compOffDTO.UserId,
                Balance = compOffDTO.Balance,
                WorkedDate = (DateTime)compOffDTO.WorkedDate,
                Reason = compOffDTO.Reason
            };

            // Calculate and set the expiry date
            compOff.ExpiryDate = ((DateTime)compOff.WorkedDate).AddDays(45);


            _dbContext.CompOffs.Add(compOff);
            _dbContext.SaveChanges();

            return compOffDTO;
        }

        public CompOffDTO GetCompOff(string userId)
        {
            var compOff = _dbContext.CompOffs.FirstOrDefault(c => c.UserId == userId);
            if (compOff == null)
                return null;

            var compOffDTO = new CompOffDTO
            {
                UserId = compOff.UserId,
                Balance = compOff.Balance,
                WorkedDate = compOff.WorkedDate,
                Reason = compOff.Reason
            };

            return compOffDTO;
        }

        /* public CompOffDTO UpdateCompOff(CompOffDTO compOffDTO)
         {
             var compOff = _dbContext.CompOffs.FirstOrDefault(c => c.UserId == compOffDTO.UserId);
             if (compOff == null)
                 return null;

             compOff.Balance = compOffDTO.Balance;
             compOff.WorkedDate = (DateTime)compOffDTO.WorkedDate;
             compOff.Reason = compOffDTO.Reason;

             // Calculate and update the expiry date
             compOff.ExpiryDate = ((DateTime)compOff.WorkedDate).AddDays(45);


             _dbContext.SaveChanges();

             return compOffDTO;
         }*/

        public CompOffDTO UpdateCompOff(CompOffDTO compOffDTO)
        {
            var compOff = _dbContext.CompOffs.FirstOrDefault(c => c.UserId == compOffDTO.UserId);
            if (compOff == null)
                return null;

            compOff.Balance = compOffDTO.Balance;
            compOff.WorkedDate = (DateTime)compOffDTO.WorkedDate;
            compOff.Reason = compOffDTO.Reason;

            // Calculate and update the expiry date
            compOff.ExpiryDate = ((DateTime)compOff.WorkedDate).AddDays(45);

            // Update LeaveBalance model
            var leaveBalance = _dbContext.LeaveBalances.FirstOrDefault(l => l.UserId == compOffDTO.UserId && l.LeaveTypeId == 5);
            if (leaveBalance != null)
            {
                leaveBalance.Balance = (double)compOffDTO.Balance;
                _dbContext.SaveChanges();
            }

            _dbContext.SaveChanges();

            return compOffDTO;
        }


        public void DeleteCompOff(string userId)
        {
            var compOff = _dbContext.CompOffs.FirstOrDefault(c => c.UserId == userId);
            if (compOff != null)
            {
                _dbContext.CompOffs.Remove(compOff);
                _dbContext.SaveChanges();
            }
        }
    }

}
