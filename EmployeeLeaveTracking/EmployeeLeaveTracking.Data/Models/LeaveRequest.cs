using System.ComponentModel.DataAnnotations;

namespace EmployeeLeaveTracking.Data.Models
{
    public class LeaveRequest
    {
        public int ID { get; set; }

        [MaxLength(500, ErrorMessage = "Maximum length for the request comments is 500 characters.")]
        public string RequestComments { get; set; }

        [Required(ErrorMessage = "Employee ID is a required field.")]
        public int EmployeeID { get; set; }

        [Required(ErrorMessage = "Leave type ID is a required field.")]
        public int LeaveTypeID { get; set; }

        [Required(ErrorMessage = "Start date is a required field.")]
        public DateTime StartDate { get; set; }

        [Required(ErrorMessage = "End date is a required field.")]
        public DateTime EndDate { get; set; }

        [Required(ErrorMessage = "Status ID is a required field.")]
        public int StatusID { get; set; }

        [Required(ErrorMessage = "Status is a required field.")]
        public StatusMaster Status { get; set; }

        [Required(ErrorMessage = "Employee is a required field.")]
        public Employee Employee { get; set; }

        [Required(ErrorMessage = "Leave type is a required field.")]
        public LeaveType LeaveType { get; set; }
    }
}
