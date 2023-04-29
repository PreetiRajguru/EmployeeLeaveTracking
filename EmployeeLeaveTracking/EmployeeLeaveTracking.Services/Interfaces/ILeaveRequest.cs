using EmployeeLeaveTracking.Data.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeLeaveTracking.Services.Interfaces
{
    public interface ILeaveRequest
    {
        IEnumerable<LeaveRequestDTO> GetAllLeaveRequests();
        LeaveRequestDTO GetLeaveRequestById(int id);
        LeaveRequestDTO AddLeaveRequest(LeaveRequestDTO leaveRequest);
        LeaveRequestDTO UpdateLeaveRequest(LeaveRequestDTO leaveRequest);
        bool DeleteLeaveRequest(int id);
    }
}
