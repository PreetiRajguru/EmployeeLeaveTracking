using AutoMapper;
using EmployeeLeaveTracking.Data.Context;
using EmployeeLeaveTracking.Data.DTOs;
using EmployeeLeaveTracking.Data.Models;
using EmployeeLeaveTracking.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;

namespace EmployeeLeaveTracking.Services.Services
{
    public class NotificationService : INotification
    {
        private readonly IMapper _mapper;
        private readonly EmployeeLeaveDbContext _dbContext;
        private readonly UserService _userService;

        public NotificationService(IMapper mapper, EmployeeLeaveDbContext dbContext, UserService userService)
        {
            _mapper = mapper;
            _dbContext = dbContext;
            _userService = userService;
        }


        //get current user logged in notifications
        public IEnumerable<NotificationDTO> GetUnviewedNotifications()
        {
            string Id = _userService.GetCurrentUserById();

            var notifications = _dbContext.Notifications
                .Where(n => (n.UserId == Id))
                .ToList();

            return _mapper.Map<List<NotificationDTO>>(notifications);
        }







        // isviewed to 1 
        public void MarkNotificationAsViewed(int notificationId)
        {
            var notification = _dbContext.Notifications.Find(notificationId);
            if (notification != null)
            {
                notification.IsViewed = true;
                _dbContext.SaveChanges();
            }
        }
    }
}





   
