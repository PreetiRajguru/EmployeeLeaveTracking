using EmployeeLeaveTracking.Data.DTOs;
using EmployeeLeaveTracking.Data.Models;

namespace EmployeeLeaveTracking.Data.Mappers
{
    public class NewLeaveRequestMapper
    {
        public NewLeaveRequestDTO Map(LeaveRequest entity)
        {
            return new NewLeaveRequestDTO
            {
                Id = entity.Id,
                RequestComments = entity.RequestComments,
                StartDate = (DateTime)entity.StartDate,
                EndDate = (DateTime)entity.EndDate,
                TotalDays = entity.TotalDays,
                ManagerId = entity.Manager.FirstName,
                LeaveTypeId = entity.LeaveType.Id,
                StatusId = entity.StatusMaster.Id
            };
        }
    }
}
