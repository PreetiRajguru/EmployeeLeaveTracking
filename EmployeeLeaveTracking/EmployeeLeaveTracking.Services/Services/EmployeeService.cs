using EmployeeLeaveTracking.Data.Context;
using EmployeeLeaveTracking.Data.DTOs;
using EmployeeLeaveTracking.Data.Models;
using EmployeeLeaveTracking.Services.Interfaces;

namespace EmployeeLeaveTracking.Services.Services
{
    public class EmployeeService : IEmployee
    {
        private readonly EmployeeLeaveDbContext _dbContext;

        public EmployeeService(EmployeeLeaveDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IEnumerable<EmployeeDTO> GetAll()
        {
            return _dbContext.Employees.Where(e => e.IsDeleted == (false)).Select(e => new EmployeeDTO
            {
                Id = e.Id,
                FirstName = e.FirstName,
                LastName = e.LastName,
                Email = e.Email,
                ManagerId = (int)e.ManagerId
            });
        }

        public EmployeeDTO GetById(int id)
        {
            var employee = _dbContext.Employees.FirstOrDefault(e => e.Id == id && e.IsDeleted==(false));

            if (employee == null)
            {
                throw new Exception($"Employee with Id {id} not found.");
            }

            return new EmployeeDTO
            {
                Id = employee.Id,
                FirstName = employee.FirstName,
                LastName = employee.LastName,
                Email = employee.Email,
                ManagerId = (int)employee.ManagerId
            };
        }

        public EmployeeDTO Create(EmployeeDTO employeeDto)
        {
            var employee = new Employee
            {
                FirstName = employeeDto.FirstName!,
                LastName = employeeDto.LastName!,
                Email = employeeDto.Email!,
                ManagerId = employeeDto.ManagerId
            };

            _dbContext.Employees.Add(employee);
            _dbContext.SaveChanges();

            employeeDto.Id = employee.Id;
            return employeeDto;
        }

        public EmployeeDTO Update(EmployeeDTO employeeDto)
        {
            var employee = _dbContext.Employees.FirstOrDefault(e => e.Id == employeeDto.Id && e.IsDeleted == (false));

            if (employee == null)
            {
                throw new Exception($"Employee with Id {employeeDto.Id} not found.");
            }

            employee.FirstName = employeeDto.FirstName!;
            employee.LastName = employeeDto.LastName!;
            employee.Email = employeeDto.Email!;
            employee.ManagerId = employeeDto.ManagerId;

            _dbContext.SaveChanges();

            return employeeDto;
        }

        public bool Delete(int id)
        {
            var employee = _dbContext.Employees.FirstOrDefault(e => e.Id == id && e.IsDeleted == (false));

            if (employee == null)
            {
                throw new Exception($"Employee with Id {id} not found.");
            }

            employee.IsDeleted = true;
            _dbContext.SaveChanges();

            return true;
        }
    }
}