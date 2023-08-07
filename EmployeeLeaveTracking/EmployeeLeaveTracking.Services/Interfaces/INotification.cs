using EmployeeLeaveTracking.Data.DTOs;

namespace EmployeeLeaveTracking.Services.Interfaces
{
    public interface INotification
    {
        IEnumerable<DetailedNotificationDTO> GetTopNotifications(string id, string userRole);

        IEnumerable<DetailedNotificationDTO> GetAllNotifications(string id, string userRole);

        void MarkNotificationAsViewed(int notificationId);

        int GetNotViewedNotificationCount(string id);
    }
}

