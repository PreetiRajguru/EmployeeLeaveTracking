using System.ComponentModel.DataAnnotations;

namespace EmployeeLeaveTracking.Data.Models
{
    public class LeaveBalance
    {
        public int Id { get; set; }
       
        public int EmployeeId { get; set; }

        public int LeaveTypeId { get; set; }

        public decimal Balance { get; set; }
       
        public int YearMonth { get; set; }

        public Employee Employee { get; set; }

        public LeaveType LeaveType { get; set; }
    }
}
