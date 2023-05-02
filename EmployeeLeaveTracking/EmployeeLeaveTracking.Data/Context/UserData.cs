using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using EmployeeLeaveTracking.Data.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

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
                    Email = "srk@gmail.com"
                });

        } 
        
    }

    public class RoleData : IEntityTypeConfiguration<IdentityRole>
    {
        public void Configure(EntityTypeBuilder<IdentityRole> builder)
        {
            builder.HasData(
                new IdentityRole
                {
                    Id="1",
                    Name = "Manager"
                }, new IdentityRole
                {
                    Id = "2",
                    Name = "Employee"
                });
        } 

    }

    public class UserRoleData : IEntityTypeConfiguration<IdentityRole>
    {
        public void Configure(EntityTypeBuilder<IdentityRole> builder)
        {
            builder.HasData(
                new IdentityRole
                {
                    Id = "1",
                    Name = "Manager"
                }, new IdentityRole
                {
                    Id = "2",
                    Name = "Employee"
                });
        }

    }

}
