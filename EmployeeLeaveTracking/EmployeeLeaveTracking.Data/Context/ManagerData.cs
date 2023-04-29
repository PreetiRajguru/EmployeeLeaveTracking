using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EmployeeLeaveTracking.Data.Models;

namespace EmployeeLeaveTracking.Data.Context
{
    public class ManagerData : IEntityTypeConfiguration<Manager>
    {
        public void Configure(EntityTypeBuilder<Manager> builder)
        {
            builder.HasData(
                new Manager
                {
                    ID = 1,
                    FirstName = "Sarika",
                    LastName = "Bhosale",
                    Email = "sarika@gmail.com"
                });
        }
    }
}
