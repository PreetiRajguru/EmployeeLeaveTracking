using EmployeeLeaveTracking.Data.DTOs;

namespace EmployeeLeaveTracking.Services.Interfaces
{
    public interface ILeaveType
    {
        IEnumerable<LeaveTypeDTO> GetAll();

        IEnumerable<LeaveTypeDTO> GetCompOff();

        LeaveTypeDTO GetById(int id);

        LeaveTypeDTO Create(LeaveTypeDTO leaveType);

        LeaveTypeDTO Update(LeaveTypeDTO leaveType);

        bool Delete(int id);

        List<LeaveTypeWithTotalDaysDTO> GetLeaveTypesWithTotalDaysTaken();
    }
}