﻿namespace EmployeeLeaveTracking.Data.Models
{
    public class LeaveType
    {
        public int Id { get; set; }

        public string? LeaveTypeName { get; set; }

        public DateTime? CreatedDate { get; set; }

        public int? CreatedBy { get; set; }

        public DateTime? ModifiedDate { get; set; }

        public int? ModifiedBy { get; set; }

        public bool? IsDeleted { get; set; } = false;

        public List<LeaveBalance>? LeaveBalances { get; set; }

        public List<LeaveRequest>? LeaveRequests { get; set; }

        public LeaveType()
        {
            CreatedDate = DateTime.UtcNow;
            ModifiedDate = DateTime.UtcNow;
        }
    }
}