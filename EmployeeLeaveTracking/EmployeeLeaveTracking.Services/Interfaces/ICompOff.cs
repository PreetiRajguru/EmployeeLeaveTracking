using EmployeeLeaveTracking.Data.DTOs;

namespace EmployeeLeaveTracking.Services.Interfaces
{
    public interface ICompOff
    {
        CompOffDTO CreateCompOff(CompOffDTO compOffDTO);
        CompOffDTO GetCompOff(string userId);
        CompOffDTO UpdateCompOff(CompOffDTO compOffDTO);
        void DeleteCompOff(string userId);
    }
}
