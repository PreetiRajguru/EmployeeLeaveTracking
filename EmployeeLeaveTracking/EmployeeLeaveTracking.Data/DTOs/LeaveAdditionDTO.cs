namespace EmployeeLeaveTracking.Data.DTOs
{
    public class LeaveAdditionDTO
    {
        public string? UserId { get; set; }

        public double? Balance { get; set; }

        public DateTime? WorkedDate { get; set; }

        public string? Reason { get; set; }
    }
}
