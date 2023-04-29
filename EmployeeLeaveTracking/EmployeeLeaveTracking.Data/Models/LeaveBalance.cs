namespace EmployeeLeaveTracking.Data.Models
{
    public class LeaveBalance
    {
        public int Id { get; set; }
       
        public int EmployeeId { get; set; }

        public int LeaveTypeId { get; set; }

        public decimal Balance { get; set; }
       
        public int YearMonth { get; set; }



        public DateTime? CreatedDate { get; set; }

        public string? CreatedBy { get; set; }

        public DateTime? ModifiedDate { get; set; }

        public string? ModifiedBy { get; set; }

        public bool? IsDeleted { get; set; } = false;




        public Employee? Employee { get; set; }

        public LeaveType? LeaveType { get; set; }
    }
}
