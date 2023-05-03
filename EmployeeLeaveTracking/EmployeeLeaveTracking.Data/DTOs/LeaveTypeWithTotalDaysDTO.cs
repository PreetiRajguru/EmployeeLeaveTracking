using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeLeaveTracking.Data.DTOs
{
    public class LeaveTypeWithTotalDaysDTO
    {
        public int LeaveTypeId { get; set; }
        public string LeaveTypeName { get; set; }
        public int TotalDaysTaken { get; set; }
    }

}
