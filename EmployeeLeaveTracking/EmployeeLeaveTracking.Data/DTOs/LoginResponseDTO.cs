namespace EmployeeLeaveTracking.Data.DTOs
{
    public class LoginResponseDTO
    {
        public string? Token { get; set; }
        public string? RefreshToken { get; set; }
        public string? Role { get; set; }
        public string? Id { get; set; }
    }
}
