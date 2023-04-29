namespace EmployeeLeaveTracking.Services.Interfaces;

public interface IRepository
{
   /* ITeacherRepository Teacher { get; }
    IStudentRepository Student { get; }*/
    IUserAuthentication UserAuthentication { get; }
    Task SaveAsync();
}
