using System.ComponentModel.DataAnnotations;

namespace EmployeeLeaveTracking.Data.Models
{
    public class LeaveRequest
    {
        public int ID { get; set; }

        public string RequestComments { get; set; }

        public int EmployeeID { get; set; }

        public int LeaveTypeID { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public int StatusID { get; set; }

        public StatusMaster Status { get; set; }
        public Employee Employee { get; set; }
        public LeaveType LeaveType { get; set; }
    }
}
