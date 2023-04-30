using System.ComponentModel.DataAnnotations;

namespace EmployeeLeaveTracking.Data.DTOs
{
    public class EmployeeDTO
    {
        public int? Id { get; set; }


        /*[Required(ErrorMessage = "Employee first name is a required field.")]
        [MaxLength(30, ErrorMessage = "Maximum length for the Name is 30 characters.")]*/
        public string? FirstName { get; set; }


        /*[Required(ErrorMessage = "Employee last name is a required field.")]
        [MaxLength(30, ErrorMessage = "Maximum length for the Name is 30 characters.")]*/
        public string? LastName { get; set; }


/*        [Required(ErrorMessage = "Email address is a required field.")]
        [EmailAddress(ErrorMessage = "Invalid email address.")]*/
        public string? Email { get; set; }


        /*[Required(ErrorMessage = "Manager Id is a required field.")]*/
        public int ManagerId { get; set; }
    }
}
