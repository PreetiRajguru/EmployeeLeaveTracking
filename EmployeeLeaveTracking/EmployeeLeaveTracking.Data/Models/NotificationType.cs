namespace EmployeeLeaveTracking.Data.Models
{
    public class NotificationType
    {
        public int Id { get; set; }

        public string? NotificationTypeName { get; set; }

        public DateTime? CreatedDate { get; set; }

        public int? CreatedBy { get; set; }

        public DateTime? ModifiedDate { get; set; }

        public int? ModifiedBy { get; set; }

        public bool? IsDeleted { get; set; } = false;

        public List<Notification>? Notifications { get; set; }


        public NotificationType()
        {
            CreatedDate = DateTime.UtcNow;
            ModifiedDate = DateTime.UtcNow;
        }
    }
}

