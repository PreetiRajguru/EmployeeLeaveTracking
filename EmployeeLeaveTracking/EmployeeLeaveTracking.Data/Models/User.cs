using Microsoft.AspNetCore.Identity;

namespace EmployeeLeaveTracking.Data.Models
{
    public class User : IdentityUser
    {
        public string? FirstName { get; set; }

        public string? LastName { get; set; }

        public string ManagerId { get; set; }

        public int DesignationId { get; set; }



        public string? RefreshToken { get; set; }
        public DateTime RefreshTokenExpiryTime { get; set; }



        public DateTime? CreatedDate { get; set; }

        public int? CreatedBy { get; set; }

        public DateTime? ModifiedDate { get; set; }

        public int? ModifiedBy { get; set; }

        public bool? IsDeleted { get; set; } = false;


        public DesignationMaster Designation { get; set; } = null!;

        public List<LeaveRequest>? ManagerLeaveRequests { get; set; }

        public List<LeaveRequest> EmployeeLeaveRequests { get; set; }

        public List<LeaveBalance> EmployeeLeaveBalance { get; set; }

    /*    public List<LeaveBalance> ManagerLeaveBalance { get; set; }*/


        public string? ProfilePictureUrl { get; set; }
        public ProfileImage ProfileImages { get; set; }

        public User()
        {
            CreatedDate = DateTime.UtcNow;
            ModifiedDate = DateTime.UtcNow;
        }
    }
}
