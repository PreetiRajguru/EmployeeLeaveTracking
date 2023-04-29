namespace EmployeeLeaveTracking.Data.DTOs
{
    public class LeaveBalanceDTO
    {
        public int ID { get; set; }

        public int EmployeeID { get; set; }

        public int LeaveTypeID { get; set; }

        public decimal Balance { get; set; }
        
        public int YearMonth { get; set; }

    }
}
