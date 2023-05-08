using System.ComponentModel.DataAnnotations;

namespace EmployeeLeaveTracking.Data.DTOs
{
    public class DesignationMasterDTO
    {
        public int Id { get; set; }


        [Required(ErrorMessage = "Designation type name is a required field.")]
        [MaxLength(50, ErrorMessage = "Maximum length for the designation type name is 50 characters.")]
        public string? DesignationName { get; set; }
    }
}
