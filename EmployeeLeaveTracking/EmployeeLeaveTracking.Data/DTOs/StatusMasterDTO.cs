using System.ComponentModel.DataAnnotations;

namespace EmployeeLeaveTracking.Data.DTOs
{
    public class StatusMasterDTO
    {
        public int Id { get; set; }


        [Required(ErrorMessage = "Status type is a required field.")]
        [MaxLength(30, ErrorMessage = "Maximum length for the status type is 30 characters.")]
        public string StatusType { get; set; }
    }
}
