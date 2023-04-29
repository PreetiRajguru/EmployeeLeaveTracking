using EmployeeLeaveTracking.Data.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeLeaveTracking.Services.Interfaces
{
    public interface IManager
    {
        IEnumerable<ManagerDTO> GetAllManagers();
        ManagerDTO GetManagerById(int id);
        ManagerDTO CreateManager(ManagerDTO manager);
        ManagerDTO UpdateManager(ManagerDTO manager);
        bool DeleteManager(int id);
    }
}
