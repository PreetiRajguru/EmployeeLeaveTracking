using EmployeeLeaveTracking.Data.DTOs;

namespace EmployeeLeaveTracking.Services.Interfaces
{
    public interface ILeaveBalance
    {
        IEnumerable<LeaveBalanceDTO> GetAll();

        LeaveBalanceDTO GetById(int id);

        LeaveBalanceDTO Create(LeaveBalanceDTO leaveBalance);

        LeaveBalanceDTO Update(LeaveBalanceDTO leaveBalance);

        bool Delete(int id);
    }

}
