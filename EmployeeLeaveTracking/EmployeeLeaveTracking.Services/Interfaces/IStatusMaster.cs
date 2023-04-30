using EmployeeLeaveTracking.Data.DTOs;

namespace EmployeeLeaveTracking.Services.Interfaces
{
    public interface IStatusMaster
    {
        IEnumerable<StatusMasterDTO> GetAll();

        StatusMasterDTO GetById(int id);
        
        StatusMasterDTO Create(StatusMasterDTO statusMaster);
        
        StatusMasterDTO Update(int id, StatusMasterDTO statusMaster);
        
        bool Delete(int id);
    }
}
