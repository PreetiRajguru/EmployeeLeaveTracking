using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeLeaveTracking.Data.Models
{
    public class LeaveType
    {
        public int ID { get; set; }
        public string LeaveTypeName { get; set; }

        public List<LeaveBalance> LeaveBalances { get; set; }
        public List<LeaveRequest> LeaveRequests { get; set; }
    }
}
