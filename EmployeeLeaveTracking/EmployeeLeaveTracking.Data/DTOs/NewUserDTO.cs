using System.ComponentModel.DataAnnotations;

namespace EmployeeLeaveTracking.Data.DTOs
{
    public class NewUserDTO
    {
        public string? FirstName { get; init; }

        public string? LastName { get; init; }

        [Required(ErrorMessage = "Username is required")]
        public string? UserName { get; init; }

        public string? Password { get; init; }

        public string? Email { get; init; }

        public string? PhoneNumber { get; init; }

        public string? ManagerId { get; init; }

        public string? DesignationId { get; init; }
    }
}