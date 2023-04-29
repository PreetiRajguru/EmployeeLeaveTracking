using AutoMapper;
using StudentTeacher.Core.Dtos;
using StudentTeacher.Core.Models;

namespace StudentTeacher.Core.Mappings;
public class UserProfile : Profile
{
    public UserProfile()
    {
        CreateMap<UserRegistrationDto, User>();
    }
}
