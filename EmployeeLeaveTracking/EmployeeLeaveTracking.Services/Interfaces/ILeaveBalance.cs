using EmployeeLeaveTracking.Data.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeLeaveTracking.Services.Interfaces
{
    public interface ILeaveBalance
    {
        IEnumerable<LeaveBalanceDTO> GetAllLeaveBalances();
        LeaveBalanceDTO GetLeaveBalanceById(int id);
        LeaveBalanceDTO AddLeaveBalance(LeaveBalanceDTO leaveBalance);
        LeaveBalanceDTO UpdateLeaveBalance(LeaveBalanceDTO leaveBalance);
        bool DeleteLeaveBalance(int id);
    }

}
