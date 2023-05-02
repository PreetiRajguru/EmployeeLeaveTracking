using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using EmployeeLeaveTracking.Data.Models;
using Microsoft.AspNetCore.Identity;

namespace EmployeeLeaveTracking.Data.Context
{
    public class EmployeeLeaveDbContext : IdentityDbContext<User>
    {
        public DbSet<LeaveType> LeaveTypes { get; set; }

        public DbSet<LeaveRequest> LeaveRequests { get; set; }

        public DbSet<StatusMaster> Status { get; set; }

        public EmployeeLeaveDbContext()
        {

        }

        public EmployeeLeaveDbContext(DbContextOptions<EmployeeLeaveDbContext> options) : base(options)
        {

        }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server = localhost; Database = EmployeeLeave; Trusted_Connection = True; TrustServerCertificate = True; MultipleActiveResultSets = True;");
            }
        }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            this.SeedUsers(modelBuilder);
            this.SeedRoles(modelBuilder);
            this.SeedUserRoles(modelBuilder);
            /* modelBuilder.ApplyConfiguration(new UserData());*/


            modelBuilder.Entity<LeaveRequest>()
           .HasOne(lb => lb.Manager)
           .WithMany(lt => lt.ManagerLeaveRequests)
           .HasForeignKey(lb => lb.ManagerId)
           .OnDelete(DeleteBehavior.NoAction);


            modelBuilder.Entity<LeaveRequest>()
           .HasOne(lb => lb.Employee)
           .WithMany(lt => lt.EmployeeLeaveRequests)
           .HasForeignKey(lb => lb.EmployeeId)
           .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<LeaveRequest>()
           .HasOne(lb => lb.LeaveType)
           .WithMany(lt => lt.LeaveRequests)
           .HasForeignKey(lb => lb.LeaveTypeId);


            modelBuilder.Entity<LeaveRequest>()
          .HasOne(lb => lb.StatusMaster)
          .WithMany(lt => lt.LeaveRequests)
          .HasForeignKey(lb => lb.StatusId);
        }


        private void SeedUsers(ModelBuilder builder)
        {
            PasswordHasher<User> passwordHasher = new();

            User user = new()
            {
                Id = "1",
                FirstName = "Sarika",
                LastName = "Bhosale",
                UserName = "sarika", 
                Email = "Sarika@gmail.com", 
                LockoutEnabled = false, 
                PhoneNumber = "1234567890"
            };

            user.PasswordHash = passwordHasher.HashPassword(user, "Sarika@123");
            

            builder.Entity<User>().HasData(user);
        }

        private void SeedRoles(ModelBuilder builder)
        {
            builder.Entity<IdentityRole>().HasData(
                new IdentityRole() { Id = "1", Name = "Manager", ConcurrencyStamp = "1", NormalizedName = "Manager" },
                new IdentityRole() { Id = "2", Name = "Employee", ConcurrencyStamp = "2", NormalizedName = "Employee" }
                );
        }

        private void SeedUserRoles(ModelBuilder builder)
        {
            builder.Entity<IdentityUserRole<string>>().HasData(
                new IdentityUserRole<string>() { RoleId = "1", UserId = "1" }
                );
        }
    }
}
