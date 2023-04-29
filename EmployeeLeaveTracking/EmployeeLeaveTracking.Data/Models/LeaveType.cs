using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace EmployeeLeaveTracking.Data.Models
{
    public class LeaveType
    {
        public int ID { get; set; }

        [Required(ErrorMessage = "Leave type name is a required field.")]
        [MaxLength(50, ErrorMessage = "Maximum length for the leave type name is 50 characters.")]
        public string LeaveTypeName { get; set; }

        public List<LeaveBalance> LeaveBalances { get; set; }
        public List<LeaveRequest> LeaveRequests { get; set; }
    }
}
