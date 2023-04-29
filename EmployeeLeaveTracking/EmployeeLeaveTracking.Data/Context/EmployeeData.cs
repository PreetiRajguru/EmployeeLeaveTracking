using EmployeeLeaveTracking.Data.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
                    ID = 1,
                    FirstName = "Sayali",
                    LastName = "Kadam",
                    Email = "sayali@gmail.com",
                    ManagerID = 1
                });
        }
    }
}
