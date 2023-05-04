using System.ComponentModel.DataAnnotations;

namespace EmployeeLeaveTracking.Data.Models
{
    public class LeaveRequest
    {
        public int Id { get; set; }

        public string? RequestComments { get; set; }

        public DateTime? StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        public int TotalDays { get; set; }


        public DateTime? CreatedDate { get; set; }

        public int? CreatedBy { get; set; }

        public DateTime? ModifiedDate { get; set; }

        public int? ModifiedBy { get; set; }

        public bool? IsDeleted { get; set; } = false;



        public string? ManagerId { get; set; }

        public string? EmployeeId { get; set; }

        public int LeaveTypeId { get; set; }

        public int StatusId { get; set; }



        public User? Employee { get; set; }

        public User? Manager { get; set; }

        public StatusMaster? StatusMaster { get; set; }

        public LeaveType? LeaveType { get; set; }

        public LeaveRequest()
        {
            CreatedDate = DateTime.UtcNow;
            ModifiedDate = DateTime.UtcNow;
        }
    }
}
