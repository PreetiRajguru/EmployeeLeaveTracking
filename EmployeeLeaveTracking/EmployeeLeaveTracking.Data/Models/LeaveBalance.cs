using System.ComponentModel.DataAnnotations;

namespace EmployeeLeaveTracking.Data.Models
{
    public class LeaveBalance
    {
        public int ID { get; set; }
       
        public int EmployeeID { get; set; }

        public int LeaveTypeID { get; set; }

        public decimal Balance { get; set; }
       
        public int YearMonth { get; set; }

        public Employee Employee { get; set; }

        public LeaveType LeaveType { get; set; }
    }
}
