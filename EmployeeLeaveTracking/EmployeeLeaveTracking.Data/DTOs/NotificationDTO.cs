namespace EmployeeLeaveTracking.Data.DTOs
{
    public class NotificationDTO
    {
        public string? UserId { get; set; }
        public int? LeaveRequestId { get; set; }
        public int? NotificationTypeId { get; set; }
        public bool? IsViewed { get; set; }
    }
}
