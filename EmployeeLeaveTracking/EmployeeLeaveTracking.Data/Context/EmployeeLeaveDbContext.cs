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

        public DbSet<DesignationMaster> Designations { get; set; }

        public DbSet<ProfileImage> ProfileImages { get; set; }

        public DbSet<LeaveBalance> LeaveBalances { get; set; }

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
                /*optionsBuilder.UseSqlServer("ConnStr");*/
                optionsBuilder.UseSqlServer("Server=localhost;Database=EmployeeLeaveAdditions;Trusted_Connection=True;TrustServerCertificate=True;" +
                    "MultipleActiveResultSets=True;");
            }
        }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            SeedRoles(modelBuilder);

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


            modelBuilder.Entity<User>()
          .HasOne(lb => lb.Designation)
          .WithMany(lt => lt.Users)
          .HasForeignKey(lb => lb.DesignationId);

            modelBuilder.Entity<LeaveBalance>()
          .HasOne(lb => lb.Employee)
          .WithMany(lt => lt.EmployeeLeaveBalance)
          .HasForeignKey(lb => lb.UserId);


            modelBuilder.Entity<LeaveBalance>()
          .HasOne(lb => lb.LeaveType)
          .WithMany(lt => lt.LeaveBalances)
          .HasForeignKey(lb => lb.LeaveTypeId);


            modelBuilder.Entity<ProfileImage>()
            .HasOne(p => p.User)
            .WithOne(p => p.ProfileImages);


            modelBuilder.Entity<LeaveType>().HasData(
            new LeaveType { Id = 1, LeaveTypeName = "Paid Leave" },
            new LeaveType { Id = 2, LeaveTypeName = "Unpaid Leave" },
            new LeaveType { Id = 3, LeaveTypeName = "Forgot ID Card" },
            new LeaveType { Id = 4, LeaveTypeName = "Work From Home" },
            new LeaveType { Id = 5, LeaveTypeName = "Compensatory Off" },
            new LeaveType { Id = 6, LeaveTypeName = "On Duty" });


            modelBuilder.Entity<DesignationMaster>().HasData(
            new DesignationMaster { Id = 1, DesignationName = "Intern" },
            new DesignationMaster { Id = 2, DesignationName = "Software Engineer" },
            new DesignationMaster { Id = 3, DesignationName = "Senior Software Engineer" },
            new DesignationMaster { Id = 4, DesignationName = "Tech Lead" });


            modelBuilder.Entity<StatusMaster>().HasData(
            new StatusMaster { Id = 1, StatusType = "Pending" },
            new StatusMaster { Id = 2, StatusType = "Approved" },
            new StatusMaster { Id = 3, StatusType = "Rejected" });

        }

        private static void SeedRoles(ModelBuilder builder)
        {
            builder.Entity<IdentityRole>().HasData(
                new IdentityRole() { Id = "1", Name = "Manager", ConcurrencyStamp = "1", NormalizedName = "Manager" },
                new IdentityRole() { Id = "2", Name = "Employee", ConcurrencyStamp = "2", NormalizedName = "Employee" }
                );
        }
    }
}









