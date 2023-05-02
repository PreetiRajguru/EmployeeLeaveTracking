namespace EmployeeLeaveTracking.Services.Interfaces;

public interface IRepository
{
    IUserAuthentication UserAuthentication { get; }
    Task SaveAsync();
}
