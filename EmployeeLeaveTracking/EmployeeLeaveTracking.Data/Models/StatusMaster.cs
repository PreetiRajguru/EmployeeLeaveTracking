using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeLeaveTracking.Data.Models
{
    public class StatusMaster
    {

        public int ID { get; set; }
        public string StatusType { get; set; }

        public List<LeaveRequest> LeaveRequests { get; set; }
    }
}
