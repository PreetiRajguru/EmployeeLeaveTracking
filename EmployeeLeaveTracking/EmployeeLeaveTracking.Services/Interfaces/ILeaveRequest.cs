using EmployeeLeaveTracking.Data.DTOs;

namespace EmployeeLeaveTracking.Services.Interfaces
{
    public interface ILeaveRequest
    {
        IEnumerable<LeaveRequestDTO> GetAll();

        LeaveRequestDTO GetById(int id);

        LeaveRequestDTO Create(LeaveRequestDTO leaveRequest);

        LeaveRequestDTO Update(LeaveRequestDTO leaveRequest);

        bool Delete(int id);
    }
}
