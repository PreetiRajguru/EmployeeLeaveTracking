namespace EmployeeLeaveTracking.Data.DTOs
{
    public class LeaveTypeWithTotalDaysDTO
    {
        public int LeaveTypeId { get; set; }
        public string LeaveTypeName { get; set; }
        public int TotalDaysTaken { get; set; }
    }
}