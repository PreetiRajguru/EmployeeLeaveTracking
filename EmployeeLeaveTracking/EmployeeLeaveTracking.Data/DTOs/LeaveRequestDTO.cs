using System.ComponentModel.DataAnnotations;

namespace EmployeeLeaveTracking.Data.DTOs
{
    public class LeaveRequestDTO
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

    }
}
