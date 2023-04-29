using EmployeeLeaveTracking.Data.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeLeaveTracking.Services.Interfaces
{
    public interface IStatusMaster
    {
        IEnumerable<StatusMasterDTO> GetAllStatusMasters();
        StatusMasterDTO GetStatusMasterById(int id);
        StatusMasterDTO AddStatusMaster(StatusMasterDTO statusMaster);
        StatusMasterDTO UpdateStatusMaster(int id, StatusMasterDTO statusMaster);
        bool DeleteStatusMaster(int id);
    }
}
