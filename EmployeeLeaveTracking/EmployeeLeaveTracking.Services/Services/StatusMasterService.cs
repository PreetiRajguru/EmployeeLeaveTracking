using EmployeeLeaveTracking.Data.Context;
using EmployeeLeaveTracking.Data.DTOs;
using EmployeeLeaveTracking.Data.Models;
using EmployeeLeaveTracking.Services.Interfaces;

namespace EmployeeLeaveTracking.Services.Services
{
    public class StatusMasterService : IStatusMaster
    {
        private readonly EmployeeLeaveDbContext _dbContext;

        public StatusMasterService(EmployeeLeaveDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IEnumerable<StatusMasterDTO> GetAll()
        {
            var statusMasters = _dbContext.Status.Where(s => s.IsDeleted == (false)).ToList();
            return statusMasters.Select(s => new StatusMasterDTO
            {
                Id = s.Id,
                StatusType = s.StatusType
            });
        }

        public StatusMasterDTO? GetById(int id)
        {
            var statusMaster = _dbContext.Status.FirstOrDefault(s => s.Id == id && s.IsDeleted == (false));
            return statusMaster != null ? new StatusMasterDTO
            {
                Id = statusMaster.Id,
                StatusType = statusMaster.StatusType
            } : null;
        }

        public StatusMasterDTO Create(StatusMasterDTO statusMaster)
        {
            var newStatusMaster = new StatusMaster
            {
                StatusType = statusMaster.StatusType
            };

            _dbContext.Status.Add(newStatusMaster);
            _dbContext.SaveChanges();

            statusMaster.Id = newStatusMaster.Id;
            return statusMaster;
        }

        public StatusMasterDTO Update(int id, StatusMasterDTO statusMaster)
        {
            var existingStatusMaster = _dbContext.Status.FirstOrDefault(s => s.Id == id && s.IsDeleted == (false));

            if (existingStatusMaster == null)
            {
                return null;
            }

            existingStatusMaster.StatusType = statusMaster.StatusType;

            _dbContext.SaveChanges();

            return new StatusMasterDTO
            {
                Id = existingStatusMaster.Id,
                StatusType = existingStatusMaster.StatusType
            };
        }

        public bool Delete(int id)
        {
            var existingStatusMaster = _dbContext.Status.FirstOrDefault(s => s.Id == id && s.IsDeleted == (false));

            if (existingStatusMaster == null)
            {
                return false;
            }

            existingStatusMaster.IsDeleted = true;

            _dbContext.SaveChanges();

            return true;
        }
    }
}
