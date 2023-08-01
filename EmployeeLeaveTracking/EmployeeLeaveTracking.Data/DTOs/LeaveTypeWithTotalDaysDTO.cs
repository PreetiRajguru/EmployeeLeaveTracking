namespace EmployeeLeaveTracking.Data.DTOs
{
    public class LeaveTypeWithTotalDaysDTO
    {
        public int LeaveTypeId { get; set; }
        public string? LeaveTypeName { get; set; }
        public int BookedDays { get; set; }
        public double AvailableDays { get; set; }
    }
}