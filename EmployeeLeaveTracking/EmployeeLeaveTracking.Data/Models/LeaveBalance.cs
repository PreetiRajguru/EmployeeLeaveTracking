namespace EmployeeLeaveTracking.Data.Models
{
    public class LeaveBalance
    {
        public int Id { get; set; }

        public string UserId { get; set; }

        public int LeaveTypeId { get; set; }

        public double Balance { get; set; }

        public User? Employee { get; set; }

        public LeaveType LeaveType { get; set; } = null!;


        public DateTime? CreatedDate { get; set; }

        public int? CreatedBy { get; set; }

        public DateTime? ModifiedDate { get; set; }

        public int? ModifiedBy { get; set; }

        public bool? IsDeleted { get; set; } = false;

        public LeaveBalance()
        {
            CreatedDate = DateTime.UtcNow;
            ModifiedDate = DateTime.UtcNow;
        }
    }
}