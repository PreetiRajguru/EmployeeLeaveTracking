﻿using AutoMapper;
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


        public IEnumerable<DetailedNotificationDTO> GetTopNotifications(string id, string userRole)
        {
            int topCount = _configuration.GetValue<int>("NotificationsCount:TopCount");

            IQueryable<DetailedNotificationDTO> query = from notification in _dbContext.Notifications
                                                        join leaveRequest in _dbContext.LeaveRequests on notification.LeaveRequestId equals leaveRequest.Id
                                                        join leaveType in _dbContext.LeaveTypes on leaveRequest.LeaveTypeId equals leaveType.Id
                                                        join status in _dbContext.Status on leaveRequest.StatusId equals status.Id
                                                        join user in _dbContext.Users on (userRole == "Manager" ? leaveRequest.EmployeeId : leaveRequest.ManagerId) equals user.Id
                                                        where notification.UserId == id
                                                        orderby notification.Id descending
                                                        select new DetailedNotificationDTO
                                                        {
                                                            Id = notification.Id,
                                                            UserId = notification.UserId,
                                                            LeaveRequestId = (int)notification.LeaveRequestId,
                                                            NotificationTypeId = (int)notification.NotificationTypeId,
                                                            IsViewed = notification.IsViewed,
                                                            Leave = new DetailedLeaveDTO
                                                            {
                                                                LeaveId = leaveRequest.Id,
                                                                StartDate = leaveRequest.StartDate,
                                                                EndDate = leaveRequest.EndDate,
                                                                RequestComments = leaveRequest.RequestComments,
                                                                StatusId = leaveRequest.StatusId,
                                                                LeaveTypeId = leaveRequest.LeaveTypeId,
                                                                TotalDays = leaveRequest.TotalDays,
                                                                LeaveTypeName = leaveType.LeaveTypeName,
                                                                FirstName = user.FirstName,
                                                                LastName = user.LastName,
                                                                StatusName = status.StatusType
                                                            }
                                                        };

            List<DetailedNotificationDTO> notificationDTOs = query.Take(topCount).ToList();

            return notificationDTOs;
        }





        public IEnumerable<DetailedNotificationDTO> GetAllNotifications(string id, string userRole)
        {
            IQueryable<DetailedNotificationDTO> query = from notification in _dbContext.Notifications
                                                        join leaveRequest in _dbContext.LeaveRequests on notification.LeaveRequestId equals leaveRequest.Id
                                                        join leaveType in _dbContext.LeaveTypes on leaveRequest.LeaveTypeId equals leaveType.Id
                                                        join status in _dbContext.Status on leaveRequest.StatusId equals status.Id
                                                        join user in _dbContext.Users on (userRole == "Manager" ? leaveRequest.EmployeeId : leaveRequest.ManagerId) equals user.Id
                                                        where notification.UserId == id
                                                        orderby notification.Id descending
                                                        select new DetailedNotificationDTO
                                                        {
                                                            Id = notification.Id,
                                                            UserId = notification.UserId,
                                                            LeaveRequestId = (int)notification.LeaveRequestId,
                                                            NotificationTypeId = (int)notification.NotificationTypeId,
                                                            IsViewed = notification.IsViewed,
                                                            Leave = new DetailedLeaveDTO
                                                            {
                                                                LeaveId = leaveRequest.Id,
                                                                StartDate = leaveRequest.StartDate,
                                                                EndDate = leaveRequest.EndDate,
                                                                RequestComments = leaveRequest.RequestComments,
                                                                StatusId = leaveRequest.StatusId,
                                                                LeaveTypeId = leaveRequest.LeaveTypeId,
                                                                TotalDays = leaveRequest.TotalDays,
                                                                LeaveTypeName = leaveType.LeaveTypeName,
                                                                FirstName = user.FirstName,
                                                                LastName = user.LastName,
                                                                StatusName = status.StatusType
                                                            }
                                                        };

            List<DetailedNotificationDTO> notificationDTOs = query.ToList();

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