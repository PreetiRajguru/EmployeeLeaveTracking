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

        Task<List<LeaveRequestDTO>> GetAllLeavesByEmployeeIdAsync(string employeeId);

        Task<List<LeaveRequestDTO>> GetAllLeavesByStatusIdAsync(int statusId);

        List<LeaveRequestDTO> GetLeaveRequestsByStatusAndManager(int statusId, string managerId);
    }
}
