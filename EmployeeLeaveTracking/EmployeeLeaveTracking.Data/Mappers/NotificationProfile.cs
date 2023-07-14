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

            /*CreateMap<Notification, DetailedNotificationDTO>();
            CreateMap<LeaveRequest, DetailedLeaveDTO>();*/


            CreateMap<Notification, DetailedNotificationDTO>()
            .ForMember(dest => dest.Leave, opt => opt.Ignore()); // Ignore mapping for nested property

            CreateMap<LeaveRequest, DetailedLeaveDTO>();
        }
    }
}
