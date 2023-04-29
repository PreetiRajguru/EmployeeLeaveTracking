using AutoMapper;
using EmployeeLeaveTracking.Data.DTOs;
using EmployeeLeaveTracking.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
