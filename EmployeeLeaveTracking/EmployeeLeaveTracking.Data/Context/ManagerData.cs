using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
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
