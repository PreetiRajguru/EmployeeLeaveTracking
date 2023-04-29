using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using EmployeeLeaveTracking.Data.Models;

namespace EmployeeLeaveTracking.Data.Context
{
    public class EmployeeLeaveDbContext : IdentityDbContext<User>
    {
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Manager> Managers { get; set; }
        public DbSet<LeaveType> LeaveTypes { get; set; }
        public DbSet<LeaveBalance> LeaveBalances { get; set; }
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


            modelBuilder.ApplyConfiguration(new ManagerData());
            modelBuilder.ApplyConfiguration(new EmployeeData());


            modelBuilder.Entity<Employee>()
                .HasOne(e => e.Manager)
                .WithMany(m => m.Employees)
                .HasForeignKey(e => e.ManagerId);

            modelBuilder.Entity<LeaveBalance>()
                .HasKey(lb => new { lb.EmployeeId, lb.LeaveTypeId, lb.YearMonth });

            modelBuilder.Entity<LeaveBalance>()
                .HasOne(lb => lb.Employee)
                .WithMany(e => e.LeaveBalances)
                .HasForeignKey(lb => lb.EmployeeId);

            modelBuilder.Entity<LeaveBalance>()
                .HasOne(lb => lb.LeaveType)
                .WithMany(lt => lt.LeaveBalances)
                .HasForeignKey(lb => lb.LeaveTypeId);

            modelBuilder.Entity<LeaveRequest>()
                .HasOne(lr => lr.Employee)
                .WithMany(e => e.LeaveRequests)
                .HasForeignKey(lr => lr.EmployeeId);

            modelBuilder.Entity<LeaveRequest>()
                .HasOne(lr => lr.LeaveType)
                .WithMany(lt => lt.LeaveRequests)
                .HasForeignKey(lr => lr.LeaveTypeId);

            modelBuilder.Entity<LeaveRequest>()
              .HasOne(lr => lr.Status)
              .WithMany(lt => lt.LeaveRequests)
              .HasForeignKey(lr => lr.StatusId);

            /*modelBuilder.Entity<Manager>()
                        .HasOne(lr => lr.User)
                        .WithMany(lt => lt.Managers)
                        .HasForeignKey(lr => lr.UserId);

            modelBuilder.Entity<Employee>()
                       .HasOne(lr => lr.User)
                       .WithMany(lt => lt.Employees)
                       .HasForeignKey(lr => lr.UserId);*/
        }
    }
}
