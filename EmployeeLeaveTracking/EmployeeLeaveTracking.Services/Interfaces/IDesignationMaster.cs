using EmployeeLeaveTracking.Data.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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