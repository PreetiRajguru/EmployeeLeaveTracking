using System.ComponentModel.DataAnnotations;

namespace EmployeeLeaveTracking.Data.Models
{
    public class Employee
    {
        public int? Id { get; set; }

        public string? FirstName { get; set; }

        public string? LastName { get; set; }

        public string? Email { get; set; }

        public int? ManagerId { get; set; }

        /*public int? UserId { get; set; }*/


        public DateTime? CreatedDate { get; set; }

        public string? CreatedBy { get; set; }

        public DateTime? ModifiedDate { get; set; }

        public string? ModifiedBy { get; set; }

        public bool? IsDeleted { get; set; } = false;



        /*  public User? User { get; set; }*/

        public Manager? Manager { get; set; }

        public List<LeaveBalance>? LeaveBalances { get; set; }

        public List<LeaveRequest>? LeaveRequests { get; set; }
    }

}
