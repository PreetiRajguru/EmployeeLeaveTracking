using System.ComponentModel.DataAnnotations;

namespace EmployeeLeaveTracking.Data.DTOs
{
    public class LeaveRequestDTO
    {
        public int Id { get; set; }



        [MaxLength(500, ErrorMessage = "Maximum length for the request comments is 500 characters.")]
        public string? RequestComments { get; set; }



        [Required(ErrorMessage = "Employee Id is a required field.")]
        public int EmployeeId { get; set; }



        [Required(ErrorMessage = "Leave type Id is a required field.")]
        public int LeaveTypeId { get; set; }



        [Required(ErrorMessage = "Start date is a required field.")]
        public DateTime StartDate { get; set; }



        [Required(ErrorMessage = "End date is a required field.")]
        public DateTime EndDate { get; set; }



        [Required(ErrorMessage = "Status ID is a required field.")]
        public int StatusId { get; set; }

    }
}
