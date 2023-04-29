using EmployeeLeaveTracking.Data.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeLeaveTracking.Services.Interfaces
{
    public interface ILeaveType
    {
        IEnumerable<LeaveTypeDTO> GetAllLeaveTypes();
        LeaveTypeDTO GetLeaveTypeById(int id);
        LeaveTypeDTO AddLeaveType(LeaveTypeDTO leaveType);
        LeaveTypeDTO UpdateLeaveType(LeaveTypeDTO leaveType);
        bool DeleteLeaveType(int id);
    }
}
