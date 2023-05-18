namespace EmployeeLeaveTracking.Data.DTOs
{
    public class LeaveBalanceDTO
    {
        public int Id { get; set; }

        public string UserId { get; set; }

        public int LeaveTypeId { get; set; }

        public double Balance { get; set; }

        public DateTime? ModifiedDate { get; set; }
    }
}
