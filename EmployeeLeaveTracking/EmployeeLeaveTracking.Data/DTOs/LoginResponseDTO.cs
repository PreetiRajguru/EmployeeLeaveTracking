using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
