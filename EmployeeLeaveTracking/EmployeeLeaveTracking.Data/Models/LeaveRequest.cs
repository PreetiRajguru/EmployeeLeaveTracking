using System.ComponentModel.DataAnnotations;

namespace EmployeeLeaveTracking.Data.Models
{
    public class LeaveRequest
    {
        public int Id { get; set; }

        public string RequestComments { get; set; }

        public int EmployeeId { get; set; }

        public int LeaveTypeId { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public int StatusId { get; set; }

        public StatusMaster Status { get; set; }
        public Employee Employee { get; set; }
        public LeaveType LeaveType { get; set; }
    }
}
