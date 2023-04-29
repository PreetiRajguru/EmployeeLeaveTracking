using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Text;
using System.Threading.Tasks;
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
                .HasForeignKey(e => e.ManagerID);

            modelBuilder.Entity<LeaveBalance>()
                .HasKey(lb => new { lb.EmployeeID, lb.LeaveTypeID, lb.YearMonth });

            modelBuilder.Entity<LeaveBalance>()
                .HasOne(lb => lb.Employee)
                .WithMany(e => e.LeaveBalances)
                .HasForeignKey(lb => lb.EmployeeID);

            modelBuilder.Entity<LeaveBalance>()
                .HasOne(lb => lb.LeaveType)
                .WithMany(lt => lt.LeaveBalances)
                .HasForeignKey(lb => lb.LeaveTypeID);

            modelBuilder.Entity<LeaveRequest>()
                .HasOne(lr => lr.Employee)
                .WithMany(e => e.LeaveRequests)
                .HasForeignKey(lr => lr.EmployeeID);

            modelBuilder.Entity<LeaveRequest>()
                .HasOne(lr => lr.LeaveType)
                .WithMany(lt => lt.LeaveRequests)
                .HasForeignKey(lr => lr.LeaveTypeID);
        }
    }
}
