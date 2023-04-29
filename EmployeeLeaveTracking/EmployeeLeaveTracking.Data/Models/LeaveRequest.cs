using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeLeaveTracking.Data.Models
{
    public class LeaveRequest
    {
        public int ID { get; set; }
        public string requestComments { get; set; }
        public int EmployeeID { get; set; }
        public int LeaveTypeID { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Status { get; set; }

        public Employee Employee { get; set; }
        public LeaveType LeaveType { get; set; }
    }
}
