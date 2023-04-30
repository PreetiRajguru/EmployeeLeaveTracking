using Microsoft.AspNetCore.Identity;

namespace EmployeeLeaveTracking.Data.Models
{
    public class User : IdentityUser
    {
        public string? FirstName { get; set; }

        public string? LastName { get; set; }

        public bool IsManager { get; set; }

        public DateTime? CreatedDate { get; set; }

        public int? CreatedBy { get; set; }

        public DateTime? ModifiedDate { get; set; }

        public int? ModifiedBy { get; set; }

        public bool? IsDeleted { get; set; } = false;

        public List<LeaveRequest>? LeaveRequests { get; set; }

        public User()
        {
            CreatedDate = DateTime.UtcNow;
            ModifiedDate = DateTime.UtcNow;
        }
    }
}
