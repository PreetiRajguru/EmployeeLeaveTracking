using EmployeeLeaveTracking.Data.DTOs;

namespace EmployeeLeaveTracking.Services.Interfaces
{
    public interface INotification
    {
        IEnumerable<NotificationDTO> GetUnviewedNotifications();
        void MarkNotificationAsViewed(int notificationId);
    }
}

