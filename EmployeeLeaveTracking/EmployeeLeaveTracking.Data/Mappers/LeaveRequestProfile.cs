using AutoMapper;
using EmployeeLeaveTracking.Data.DTOs;
using EmployeeLeaveTracking.Data.Models;

namespace EmployeeLeaveTracking.Data.Mappers
{
    public class LeaveRequestProfile : Profile
    {
        public LeaveRequestProfile()
        {
            CreateMap<LeaveRequest, LeaveRequestDTO>();
            CreateMap<LeaveRequestDTO, LeaveRequest>();
        }
    }
}