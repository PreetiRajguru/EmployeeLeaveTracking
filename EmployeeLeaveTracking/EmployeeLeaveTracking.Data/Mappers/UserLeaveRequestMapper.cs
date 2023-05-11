using EmployeeLeaveTracking.Data.DTOs;
using EmployeeLeaveTracking.Data.Models;

namespace EmployeeLeaveTracking.Data.Mappers
{
    public class UserLeaveRequestMapper
    {
        public UserLeaveRequestDTO Map(LeaveRequest entity)
        {
            return new UserLeaveRequestDTO
            {
                Id = entity.Id,
                UserId = entity.EmployeeId,
                RequestComments = entity.RequestComments,
                StartDate = entity.StartDate,
                EndDate = entity.EndDate,
                TotalDays = entity.TotalDays,
                Comments = entity.RequestComments,
                EmployeeName = entity.Employee.FirstName,
                LeaveTypeName = entity.LeaveType.LeaveTypeName,
                StatusName = entity.StatusMaster.StatusType
            };
        }
    }
}