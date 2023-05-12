using EmployeeLeaveTracking.Data.Context;
using EmployeeLeaveTracking.Data.DTOs;
using EmployeeLeaveTracking.Data.Models;
using EmployeeLeaveTracking.Services.Interfaces;
using System.Web.Http.ModelBinding;

namespace EmployeeLeaveTracking.Services.Services
{
    public class DesignationMasterService : IDesignationMaster
    {
        private readonly EmployeeLeaveDbContext _dbContext;

        public DesignationMasterService(EmployeeLeaveDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IEnumerable<DesignationMasterDTO> GetAll()
        {
            List<DesignationMaster> designationMasters = _dbContext.Designations.Where(s => s.IsDeleted == false).ToList();
            return designationMasters.Select(s => new DesignationMasterDTO
            {
                Id = s.Id,
                DesignationName = s.DesignationName
            });
        }

        public DesignationMasterDTO? GetById(int id)
        {
            DesignationMaster? designationMasters = _dbContext.Designations.FirstOrDefault(s => s.Id == id && s.IsDeleted == false);
            return designationMasters != null ? new DesignationMasterDTO
            {
                Id = designationMasters.Id,
                DesignationName = designationMasters.DesignationName
            } : null;
        }

        public DesignationMasterDTO Create(DesignationMasterDTO designationName)
        {
            DesignationMaster newDesignationMaster = new()
            {
                DesignationName = designationName.DesignationName
            };

            _dbContext.Designations.Add(newDesignationMaster);
            _dbContext.SaveChanges();

            designationName.Id = newDesignationMaster.Id;
            return designationName;
        }

        public DesignationMasterDTO? Update(int id, DesignationMasterDTO designationName)
        {
            DesignationMaster? existingDesignationMaster = _dbContext.Designations.FirstOrDefault(s => s.Id == id && s.IsDeleted == false);

            if (existingDesignationMaster == null)
            {
                return null;
            }

            existingDesignationMaster.DesignationName = designationName.DesignationName;

            _dbContext.SaveChanges();

            return new DesignationMasterDTO
            {
                Id = existingDesignationMaster.Id,
                DesignationName = existingDesignationMaster.DesignationName
            };
        }

        public bool Delete(int id)
        {
            DesignationMaster? existingDesignationMaster = _dbContext.Designations.FirstOrDefault(s => s.Id == id && s.IsDeleted == (false));

            if (existingDesignationMaster == null)
            {
                return false;
            }

            existingDesignationMaster.IsDeleted = true;

            _dbContext.SaveChanges();

            return true;
        }
    }
}