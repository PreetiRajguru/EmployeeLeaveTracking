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


            modelBuilder.Entity<User>()
          .HasOne(lb => lb.Designation)
          .WithMany(lt => lt.Users)
          .HasForeignKey(lb => lb.DesignationId);


            modelBuilder.Entity<ProfileImage>()
            .HasOne(p => p.User)
            .WithOne(p => p.ProfileImages);
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
