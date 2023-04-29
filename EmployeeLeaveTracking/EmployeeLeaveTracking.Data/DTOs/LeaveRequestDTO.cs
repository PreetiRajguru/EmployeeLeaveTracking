using EmployeeLeaveTracking.Data.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeLeaveTracking.Data.DTOs
{
    public class LeaveRequestDTO
    {
        public int ID { get; set; }

        public string RequestComments { get; set; }

        public int EmployeeID { get; set; }

        public int LeaveTypeID { get; set; }

        public DateTime StartDate { get; set; }
        
        public DateTime EndDate { get; set; }

        public int StatusID { get; set; }

    }
}
