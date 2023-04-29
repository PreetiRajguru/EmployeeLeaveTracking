namespace EmployeeLeaveTracking.Data.DTOs
{
    public class LeaveRequestDTO
    {
        public int ID { get; set; }

        public string RequestComments { get; set; }

        public int EmployeeID { get; set; }

        public int LeaveTypeID { get; set; }

        public DateTime StartDate { get; set; }
        
        public DateTime EndDate { get; set; }

        public int StatusID { get; set; }

    }
}
