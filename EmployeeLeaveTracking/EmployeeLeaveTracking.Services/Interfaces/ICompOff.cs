using EmployeeLeaveTracking.Data.DTOs;

namespace EmployeeLeaveTracking.Services.Interfaces
{
    public interface ICompOff
    {
        LeaveAdditionDTO CreateCompOff(LeaveAdditionDTO compOffDTO);
        LeaveAdditionDTO GetCompOff(string userId);
        LeaveAdditionDTO UpdateCompOff(LeaveAdditionDTO compOffDTO);
        void DeleteCompOff(string userId);
    }
}
