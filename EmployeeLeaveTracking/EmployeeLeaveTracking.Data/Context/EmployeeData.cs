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
                    Id = 1,
                    FirstName = "Neha",
                    LastName = "Patole",
                    Email = "neha@gmail.com",
                    ManagerId = 1
                },
                new Employee
                {
                    Id = 2,
                    FirstName = "Sayali",
                    LastName = "Kadam",
                    Email = "sayali@gmail.com",
                    ManagerId = 1
                });
        }
    }
}
