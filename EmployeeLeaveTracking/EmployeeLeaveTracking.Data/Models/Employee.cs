using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeLeaveTracking.Data.Models
{
    public class Employee
    {
        public int ID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }

        public int ManagerID { get; set; }
        public Manager Manager { get; set; }

        public List<LeaveBalance> LeaveBalances { get; set; }
        public List<LeaveRequest> LeaveRequests { get; set; }
    }
}
