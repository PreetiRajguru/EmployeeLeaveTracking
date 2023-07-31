using EmployeeLeaveTracking.Data.DTOs;

namespace EmployeeLeaveTracking.Services.Interfaces
{
    public interface IOnDuty
    {
        LeaveAdditionDTO CreateOnDuty(LeaveAdditionDTO onDutyDTO);

        LeaveAdditionDTO GetOnDuty(string userId);

        LeaveAdditionDTO UpdateOnDuty(LeaveAdditionDTO onDutyDTO);

        void DeleteOnDuty(string userId);
    }
}
