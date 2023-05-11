namespace EmployeeLeaveTracking.Data.Models
{
    public class StatusMaster
    {
        public int Id { get; set; }

        public string? StatusType { get; set; }


        public DateTime? CreatedDate { get; set; }

        public int? CreatedBy { get; set; }

        public DateTime? ModifiedDate { get; set; }

        public int? ModifiedBy { get; set; }

        public bool? IsDeleted { get; set; } = false;


        public List<LeaveRequest>? LeaveRequests { get; set; }

        public StatusMaster()
        {
            CreatedDate = DateTime.UtcNow;
            ModifiedDate = DateTime.UtcNow;
        }
    }
}