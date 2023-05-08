using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System.Runtime.CompilerServices;
using System.Text;

namespace EmployeeLeaveTracking.Data.DTOs
{
    public class UserLeaveRequestDTO
    {
        public int Id { get; set; }

        public string RequestComments { get; set; } = null!;

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public int TotalDays { get; set; }

        /*public string? ManagerName { get; set; }*/

        public string? EmployeeName { get; set; }

        public string? LeaveTypeName { get; set; }

        public string? StatusName { get; set; }

        public string? UserId { get; set; }

        public string? Comments { get; set; }
    }
}
