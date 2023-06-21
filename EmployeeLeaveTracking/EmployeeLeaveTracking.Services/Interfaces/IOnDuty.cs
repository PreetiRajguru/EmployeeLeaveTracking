using EmployeeLeaveTracking.Data.DTOs;

namespace EmployeeLeaveTracking.Services.Interfaces
{
    public interface IOnDuty
    {
        OnDutyDTO CreateOnDuty(OnDutyDTO onDutyDTO);
        OnDutyDTO GetOnDuty(string userId);
        OnDutyDTO UpdateOnDuty(OnDutyDTO onDutyDTO);
        void DeleteOnDuty(string userId);
    }
}
