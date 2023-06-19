using System.ComponentModel.DataAnnotations;

namespace EmployeeLeaveTracking.Data.DTOs
{
    public class LeaveTypeDTO
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Leave type name is a required field.")]
        [MaxLength(50, ErrorMessage = "Maximum length for the leave type name is 50 characters.")]
        public string? LeaveTypeName { get; set; }
    }
}