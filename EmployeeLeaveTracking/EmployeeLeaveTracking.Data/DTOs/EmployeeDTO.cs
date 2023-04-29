namespace EmployeeLeaveTracking.Data.DTOs
{
    public class EmployeeDTO
    {
        public int ID { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public int ManagerID { get; set; }
    }
}
