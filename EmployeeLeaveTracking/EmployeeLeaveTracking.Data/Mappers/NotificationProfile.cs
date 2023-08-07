using AutoMapper;
using EmployeeLeaveTracking.Data.DTOs;
using EmployeeLeaveTracking.Data.Models;

namespace EmployeeLeaveTracking.Data.Mappers
{
    public class NotificationProfile : Profile
    {
        public NotificationProfile()
        {
            CreateMap<NotificationDTO, Notification>();
            CreateMap<Notification, DetailedNotificationDTO>();
            CreateMap<Notification, NotificationDTO>();
            CreateMap<LeaveRequest, NotificationDTO>();
            CreateMap<Notification, DetailedNotificationDTO>()
            .ForMember(d => d.Leave, opt => opt.Ignore()); //ignore mapping for nested property

            CreateMap<LeaveRequest, DetailedLeaveDTO>();
        }
    }
}
