using EmployeeLeaveTracking.Data.DTOs;

namespace EmployeeLeaveTracking.Services.Interfaces
{
    public interface IDesignationMaster
    {
        IEnumerable<DesignationMasterDTO> GetAll();

        DesignationMasterDTO GetById(int id);

        DesignationMasterDTO Create(DesignationMasterDTO designationName);

        DesignationMasterDTO Update(int id,DesignationMasterDTO designationName);

        bool Delete(int id);
    }
}