namespace EmployeeLeaveTracking.Data.DTOs
{
    public class DetailedNotificationDTO
    {
        public int Id { get; set; }
        public string? UserId { get; set; }
        public int? LeaveRequestId { get; set; }
        public int? NotificationTypeId { get; set; }
        public bool? IsViewed { get; set; }
        public DetailedLeaveDTO Leave { get; set; }
    }
}
