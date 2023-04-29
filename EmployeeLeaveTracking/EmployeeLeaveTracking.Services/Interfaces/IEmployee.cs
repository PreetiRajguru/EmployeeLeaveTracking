using EmployeeLeaveTracking.Data.DTOs;

namespace EmployeeLeaveTracking.Services.Interfaces
{
    public interface IEmployee
    {
        IEnumerable<EmployeeDTO> GetAllEmployees();
        EmployeeDTO GetEmployeeById(int id);
        EmployeeDTO CreateEmployee(EmployeeDTO employee);
        EmployeeDTO UpdateEmployee(EmployeeDTO employee);
        bool DeleteEmployee(int id);
    }

}
