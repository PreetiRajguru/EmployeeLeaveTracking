using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeLeaveTracking.Data.DTOs
{
    public class DetailedNotificationDTO
    {
        public string? UserId { get; set; }
        public int? LeaveRequestId { get; set; }
        public int? NotificationTypeId { get; set; }
        public bool? IsViewed { get; set; }
        public DetailedLeaveDTO Leave { get; set; }
    }
}
