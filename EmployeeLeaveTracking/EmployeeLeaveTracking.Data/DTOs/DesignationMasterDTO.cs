using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
