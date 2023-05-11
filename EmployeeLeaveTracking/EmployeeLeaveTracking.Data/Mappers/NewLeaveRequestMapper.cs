using EmployeeLeaveTracking.Data.DTOs;
using EmployeeLeaveTracking.Data.Models;

namespace EmployeeLeaveTracking.Data.Mappers
{
    public class NewLeaveRequestMapper
    {
        public static NewLeaveRequestDTO Map(LeaveRequest entity)
        {
            return new NewLeaveRequestDTO
            {
                Id = entity.Id,
                RequestComments = entity.RequestComments,
                StartDate = entity.StartDate,
                EndDate = entity.EndDate,
                TotalDays = entity.TotalDays,
                ManagerId = entity.Manager.FirstName,
                LeaveTypeId = entity.LeaveType.Id,
                StatusId = entity.StatusMaster.Id
            };
        }
    }
}