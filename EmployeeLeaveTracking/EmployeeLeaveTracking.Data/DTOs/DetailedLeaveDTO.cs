namespace EmployeeLeaveTracking.Data.DTOs
{
    
    public class DetailedLeaveDTO
    {
        public int LeaveId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string RequestComments { get; set; }
        public int StatusId { get; set; }
        public int LeaveTypeId { get; set; }
        public double TotalDays { get; set; }
        public string LeaveTypeName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string StatusName { get; set; }
    }
}
