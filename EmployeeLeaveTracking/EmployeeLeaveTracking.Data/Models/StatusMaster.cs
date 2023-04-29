using System.ComponentModel.DataAnnotations;

namespace EmployeeLeaveTracking.Data.Models
{
    public class StatusMaster
    {
        public int ID { get; set; }

        public string StatusType { get; set; }

        public List<LeaveRequest> LeaveRequests { get; set; }
    }
}
