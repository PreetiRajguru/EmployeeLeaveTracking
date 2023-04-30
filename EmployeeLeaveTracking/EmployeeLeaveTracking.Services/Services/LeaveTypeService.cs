using EmployeeLeaveTracking.Data.Context;
using EmployeeLeaveTracking.Data.DTOs;
using EmployeeLeaveTracking.Data.Models;
using EmployeeLeaveTracking.Services.Interfaces;

namespace EmployeeLeaveTracking.Services.Services
{
    public class LeaveTypeService : ILeaveType
    {
        private readonly EmployeeLeaveDbContext _dbContext;

        public LeaveTypeService(EmployeeLeaveDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IEnumerable<LeaveTypeDTO> GetAll()
        {
            var leaveTypes = _dbContext.LeaveTypes.Where(lt => lt.IsDeleted == (false)).Select(lt => new LeaveTypeDTO
            {
                Id = lt.Id,
                LeaveTypeName = lt.LeaveTypeName
            });

            return leaveTypes;
        }

        public LeaveTypeDTO GetById(int id)
        {
            var leaveType = _dbContext.LeaveTypes.Where(lt => lt.IsDeleted == (false) && lt.Id == id).Select(lt => new LeaveTypeDTO
            {
                Id = lt.Id,
                LeaveTypeName = lt.LeaveTypeName
            }).FirstOrDefault();

            return leaveType;
        }

        public LeaveTypeDTO Create(LeaveTypeDTO leaveType)
        {
            var newLeaveType = new LeaveType
            {
                LeaveTypeName = leaveType.LeaveTypeName
            };

            _dbContext.LeaveTypes.Add(newLeaveType);
            _dbContext.SaveChanges();

            leaveType.Id = newLeaveType.Id;

            return leaveType;
        }

        public LeaveTypeDTO Update(LeaveTypeDTO leaveType)
        {
            var existingLeaveType = _dbContext.LeaveTypes.Find(leaveType.Id);

            if (existingLeaveType == null || existingLeaveType.IsDeleted == (true))
            {
                return null;
            }

            existingLeaveType.LeaveTypeName = leaveType.LeaveTypeName;

            _dbContext.SaveChanges();

            return leaveType;
        }

        public bool Delete(int id)
        {
            var existingLeaveType = _dbContext.LeaveTypes.Find(id);

            if (existingLeaveType == null || existingLeaveType.IsDeleted == (true))
            {
                return false;
            }

            existingLeaveType.IsDeleted = true;

            _dbContext.SaveChanges();

            return true;
        }
    }
}
