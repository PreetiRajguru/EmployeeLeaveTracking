using EmployeeLeaveTracking.Data.DTOs;

namespace EmployeeLeaveTracking.Services.Interfaces
{
    public interface IManager
    {
        IEnumerable<ManagerDTO> GetAll();
        ManagerDTO GetById(int id);
        ManagerDTO Create(ManagerDTO manager);
        ManagerDTO Update(ManagerDTO manager);
        bool Delete(int id);
    }
}
