using EmployeeLeaveTracking.Data.DTOs;

namespace EmployeeLeaveTracking.Services.Interfaces
{
    public interface INotification
    {
        /*IEnumerable<DetailedNotificationDTO> GetAllNotificationsForManager(string id);

        IEnumerable<DetailedNotificationDTO> GetAllNotificationsForEmployee(string id);*/



        IEnumerable<DetailedNotificationDTO> GetAllNotifications(string id, string userRole);

        void MarkNotificationAsViewed(int notificationId);

        int GetNotViewedNotificationCount(string id);
    }
}

