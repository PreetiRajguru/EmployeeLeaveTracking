using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace EmployeeLeaveTracking.Data.Models
{
    public class OnDuty
    {
        [Key]
        [ForeignKey("User")]
        public string? UserId { get; set; }

        [JsonIgnore]
        public User? User { get; set; }

        public double? Balance { get; set; }

        public DateTime? WorkedDate { get; set; }

        [NotMapped]
        public DateTime? ExpiryDate { get; set; }

        public string? Reason { get; set; }

    }
}
