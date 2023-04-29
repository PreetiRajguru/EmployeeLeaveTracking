using System.ComponentModel.DataAnnotations;

namespace EmployeeLeaveTracking.Data.Models
{
    public class Manager
    {
        public int Id { get; set; }

        
        public string FirstName { get; set; }

        
        public string LastName { get; set; }

       
        public string Email { get; set; }

        public List<Employee> Employees { get; set; }
    }
}
