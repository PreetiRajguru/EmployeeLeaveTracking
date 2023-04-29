using AutoMapper;
using EmployeeLeaveTracking.Data.Context;
using EmployeeLeaveTracking.Data.Models;
using EmployeeLeaveTracking.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;

namespace StudentTeacher.Service.Services;

public class RepositoryManagerService : IRepository
{
    private EmployeeLeaveDbContext _repositoryContext;

    /*private ITeacherRepository _teacherRepository;
    private IStudentRepository _studentRepository;*/


    private IUserAuthentication _userAuthenticationRepository;
    private UserManager<User> _userManager;
    private IMapper _mapper;
    private IConfiguration _configuration;

    public RepositoryManagerService(EmployeeLeaveDbContext repositoryContext, UserManager<User> userManager, IMapper mapper, IConfiguration configuration) 
    {
        _repositoryContext = repositoryContext;
        _userManager = userManager;    
        _mapper = mapper;   
        _configuration = configuration; 
    }

 
    public IUserAuthentication UserAuthentication
    {
        get
        {
            if (_userAuthenticationRepository is null)
                _userAuthenticationRepository = new UserAuthenticationService(_userManager, _configuration, _mapper);
            return _userAuthenticationRepository;
        }
    }
    public Task SaveAsync() => _repositoryContext.SaveChangesAsync();




    /* public ITeacherRepository Teacher
   {
       get
       {
           if (_teacherRepository is null)
               _teacherRepository = new TeacherRepository( _repositoryContext);
           return _teacherRepository;
       }
   }
   public IStudentRepository Student
   {
       get
       {
           if (_studentRepository is null)
               _studentRepository = new StudentRepository(_repositoryContext);
           return _studentRepository;
       }
   }*/

}
