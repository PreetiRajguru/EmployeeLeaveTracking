using EmployeeLeaveTracking.Data.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace EmployeeLeaveTracking.Data.Context
{
    public class EmployeeData : IEntityTypeConfiguration<Employee>
    {
        public void Configure(EntityTypeBuilder<Employee> builder)
        {
            builder.HasData(
                new Employee
                {
                    ID = 1,
                    FirstName = "Neha",
                    LastName = "Patole",
                    Email = "neha@gmail.com",
                    ManagerID = 1
                },
                new Employee
                {
                    ID = 2,
                    FirstName = "Sayali",
                    LastName = "Kadam",
                    Email = "sayali@gmail.com",
                    ManagerID = 1
                });
        }
    }
}
