using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using EmployeeLeaveTracking.Data.Models;

namespace EmployeeLeaveTracking.Data.Context
{
    public class UserData : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasData(
                new User
                {
                    Id = "1",
                    FirstName = "Sarika",
                    LastName = "Bhosale",
                });
        }
    }
}
