using AutoMapper;
using EmployeeLeaveTracking.Data.DTOs;
using EmployeeLeaveTracking.Data.Models;

namespace EmployeeLeaveTracking.Data.Mappers
{
    public class StatusMasterProfile : Profile
    {
        public StatusMasterProfile()
        {
            CreateMap<StatusMaster, StatusMasterDTO>();
            CreateMap<StatusMasterDTO, StatusMaster>();
        }
    }
}
