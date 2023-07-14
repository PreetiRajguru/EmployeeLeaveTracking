using AutoMapper;
using EmployeeLeaveTracking.Data.Context;
using EmployeeLeaveTracking.Data.DTOs;
using EmployeeLeaveTracking.Data.Models;
using EmployeeLeaveTracking.Services.Interfaces;
using Microsoft.Extensions.Configuration;

namespace EmployeeLeaveTracking.Services.Services
{
    public class NotificationService : INotification
    {
        private readonly IMapper _mapper;
        private readonly EmployeeLeaveDbContext _dbContext;
        private readonly IConfiguration _configuration;

        public NotificationService(IMapper mapper, EmployeeLeaveDbContext dbContext, IConfiguration configuration)
        {
            _mapper = mapper;
            _dbContext = dbContext;
            _configuration = configuration;
        }



        






     




        public IEnumerable<DetailedNotificationDTO> GetAllNotifications(string id, string userRole)
        {
            int topCount = _configuration.GetValue<int>("NotificationsCount:TopCount");

            List<Notification> notifications = _dbContext.Notifications
                .Where(n => n.UserId == id)
                .OrderByDescending(n => n.Id)
                .Take(topCount)
                .ToList();

            List<DetailedNotificationDTO> notificationDTOs = _mapper.Map<List<DetailedNotificationDTO>>(notifications);

            return notificationDTOs;
        }








        // updating isviewed value to 1 
        public void MarkNotificationAsViewed(int notificationId)
        {
            try
            {
                Notification notification = _dbContext.Notifications.Find(notificationId);

                if (notification != null)
                {
                    notification.IsViewed = true;
                    _dbContext.SaveChanges();
                }
            }
            catch (ArgumentException ex)
            {
                throw new Exception("Null Value Identified", ex);
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while marking the notification as viewed.", ex);
            }
        }


        //new notifications
        public int GetNotViewedNotificationCount(string id)
        {
            try
            {
                if (string.IsNullOrEmpty(id))
                {
                    throw new ArgumentNullException(nameof(id), "User ID is null or empty.");
                }

                int count = _dbContext.Notifications
                    .Where(n => n.UserId == id && !n.IsViewed)
                    .Count();
                return count;
            }
            catch (ArgumentNullException ex)
            {
                throw new Exception("Null Value Identified", ex);
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while retrieving the notification count.", ex);
            }
        }

    }
}