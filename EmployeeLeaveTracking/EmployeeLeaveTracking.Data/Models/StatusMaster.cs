using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace EmployeeLeaveTracking.Data.Models
{
    public class StatusMaster
    {
        public int ID { get; set; }

        [Required(ErrorMessage = "Status type is a required field.")]
        [MaxLength(30, ErrorMessage = "Maximum length for the status type is 30 characters.")]
        public string StatusType { get; set; }

        public List<LeaveRequest> LeaveRequests { get; set; }
    }
}
