using AutoMapper;
using EmployeeLeaveTracking.Data.DTOs;
using EmployeeLeaveTracking.Data.Models;

namespace EmployeeLeaveTracking.Data.Mappers
{

    public class LeaveBalanceProfile : Profile
    {
        public LeaveBalanceProfile()
        {
            CreateMap<LeaveBalance, LeaveBalanceDTO>();
            CreateMap<LeaveBalanceDTO, LeaveBalance>();
        }
    }
}
