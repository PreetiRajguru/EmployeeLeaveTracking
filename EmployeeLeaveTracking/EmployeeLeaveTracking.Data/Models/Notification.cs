namespace EmployeeLeaveTracking.Data.Models
{
    public class Notification
    {
        public int Id { get; set; }
        public string? UserId { get; set; }
        public int? LeaveRequestId { get; set; }
        public int? NotificationTypeId { get; set; }
        public bool IsViewed { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        public User User { get; set; }
        public LeaveRequest LeaveRequest { get; set; }
        public NotificationType NotificationTypeName { get; set; }

        public Notification()
        {
            CreatedAt = DateTime.UtcNow;
        }
    }

}
