using EmployeeLeaveTracking.Data.DTOs;

namespace EmployeeLeaveTracking.Services.Interfaces
{
    public interface IEmployee
    {
        IEnumerable<EmployeeDTO> GetAll();

        EmployeeDTO GetById(int id);

        EmployeeDTO Create(EmployeeDTO employee);

        EmployeeDTO Update(EmployeeDTO employee);

        bool Delete(int id);
    }
}