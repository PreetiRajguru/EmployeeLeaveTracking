using EmployeeLeaveTracking.Data.DTOs;
using EmployeeLeaveTracking.Data.Models;

namespace EmployeeLeaveTracking.Services.Interfaces
{
    public interface ILeaveRequest
    {
        IEnumerable<LeaveRequestDTO> GetAll();

        LeaveRequestDTO GetById(int id);

        LeaveRequestDTO Create(LeaveRequestDTO leaveRequest);

        public NewLeaveRequestDTO CreateNewLeaveRequest(NewLeaveRequestDTO leaveRequest);


        LeaveRequestDTO Update(LeaveRequestDTO leaveRequest);

        bool Delete(int id);


        List<UserLeaveRequestDTO> GetAllLeavesByEmployeeId(int limit, int offset);

        Task<List<LeaveRequestDTO>> GetAllLeavesByStatusIdAsync(int statusId);

        List<LeaveRequestDTO> GetLeaveRequestsByStatusAndManager(int statusId, string managerId);


        Task<LeaveRequest> GetLeaveById(int id);

        Task<int> UpdateLeaveRequestStatus(int id, int statusId);

        double LeaveBalance(string employeeId);

        public List<UserLeaveRequestDTO> LeavesByEmployeeId(string employeeId);

        
        //new method
        public NewLeaveRequestDTO NewCreateNewLeaveRequest(NewLeaveRequestDTO leaveRequest);
        
    }
}