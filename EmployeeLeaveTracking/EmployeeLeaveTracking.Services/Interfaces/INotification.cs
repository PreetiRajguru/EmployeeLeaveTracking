using EmployeeLeaveTracking.Data.DTOs;

namespace EmployeeLeaveTracking.Services.Interfaces
{
    public interface INotification
    {
        IEnumerable<DetailedNotificationDTO> GetAllNotificationsForManager();

        IEnumerable<DetailedNotificationDTO> GetAllNotificationsForEmployee();

        void MarkNotificationAsViewed(int notificationId);
    }
}

