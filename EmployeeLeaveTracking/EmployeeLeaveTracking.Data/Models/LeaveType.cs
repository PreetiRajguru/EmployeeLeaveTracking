using System.ComponentModel.DataAnnotations;

namespace EmployeeLeaveTracking.Data.Models
{
    public class LeaveType
    {
        public int Id { get; set; }
        public string? LeaveTypeName { get; set; }

        public DateTime? CreatedDate { get; set; }

        public string? CreatedBy { get; set; }

        public DateTime? ModifiedDate { get; set; }

        public string? ModifiedBy { get; set; }

        public bool? IsDeleted { get; set; } = false;


        public List<LeaveBalance>? LeaveBalances { get; set; }

        public List<LeaveRequest>? LeaveRequests { get; set; }
    }
}
