using EmployeeLeaveTracking.Data.Context;
using EmployeeLeaveTracking.Data.DTOs;
using EmployeeLeaveTracking.Data.Models;
using EmployeeLeaveTracking.Services.Interfaces;

namespace EmployeeLeaveTracking.Services.Services
{
    public class ManagerService : IManager
    {

        private readonly EmployeeLeaveDbContext _context;


        public ManagerService(EmployeeLeaveDbContext context)
        {
            _context = context;
        }


        public IEnumerable<ManagerDTO> GetAll()
        {
            var managers = _context.Managers.Where(m => m.IsDeleted == (false)).ToList();
            return managers.Select(m => new ManagerDTO
            {
                Id = m.Id,
                FirstName = m.FirstName,
                LastName = m.LastName,
                Email = m.Email
            });
        }

        public ManagerDTO GetById(int id)
        {
            var manager = _context.Managers.FirstOrDefault(m => m.Id == id && m.IsDeleted == (false));
            if (manager == null)
            {
                return null;
            }
            return new ManagerDTO
            {
                Id = manager.Id,
                FirstName = manager.FirstName,
                LastName = manager.LastName,
                Email = manager.Email
            };
        }

        public ManagerDTO Create(ManagerDTO manager)
        {
            var newManager = new Manager
            {
                FirstName = manager.FirstName,
                LastName = manager.LastName,
                Email = manager.Email
            };
            _context.Managers.Add(newManager);
            _context.SaveChanges();
            manager.Id = newManager.Id;
            return manager;
        }

        public ManagerDTO Update(ManagerDTO manager)
        {
            var existingManager = _context.Managers.FirstOrDefault(m => m.Id == manager.Id && m.IsDeleted == (false));
            if (existingManager == null)
            {
                return null;
            }
            existingManager.FirstName = manager.FirstName;
            existingManager.LastName = manager.LastName;
            existingManager.Email = manager.Email;
            _context.SaveChanges();
            return manager;
        }

        public bool Delete(int id)
        {
            var existingManager = _context.Managers.FirstOrDefault(m => m.Id == id && m.IsDeleted == (false));
            if (existingManager == null)
            {
                return false;
            }
            existingManager.IsDeleted = true;
            _context.SaveChanges();
            return true;
        }
    }
}
