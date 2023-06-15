using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace EmployeeLeaveTracking.Data.Models
{
    public class ProfileImage
    {
        [Key]
        [ForeignKey("User")]
        public string? UserId { get; set; }
        [JsonIgnore]
        public User? User { get; set; }
        [NotMapped]
        public IFormFile? Image { get; set; }

        [JsonIgnore]
        public string? ImagePath { get; set; }
    }
}