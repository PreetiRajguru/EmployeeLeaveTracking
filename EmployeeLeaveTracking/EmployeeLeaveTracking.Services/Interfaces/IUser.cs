using EmployeeLeaveTracking.Data.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeLeaveTracking.Services.Interfaces
{
    public interface IUser
    {
        IEnumerable<UserRegistrationDTO> GetUsersByManagerId(int managerId);

    }
}
