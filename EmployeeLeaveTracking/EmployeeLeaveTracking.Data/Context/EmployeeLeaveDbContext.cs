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
                optionsBuilder.UseSqlServer("ConnStr");
            }
        }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            this.SeedRoles(modelBuilder);

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

        private void SeedRoles(ModelBuilder builder)
        {
            builder.Entity<IdentityRole>().HasData(
                new IdentityRole() { Id = "1", Name = "Manager", ConcurrencyStamp = "1", NormalizedName = "Manager" },
                new IdentityRole() { Id = "2", Name = "Employee", ConcurrencyStamp = "2", NormalizedName = "Employee" }
                );
        }
    }
}
