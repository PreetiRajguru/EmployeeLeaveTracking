﻿using EmployeeLeaveTracking.Data.Models;

namespace EmployeeLeaveTracking.Services.Interfaces
{
    public interface ILeaveBalance
    {
        IEnumerable<LeaveBalance> GetAllLeaveBalances();
    }
}
