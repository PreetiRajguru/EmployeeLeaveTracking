using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeLeaveTracking.Data.DTOs
{
    public class LeaveTypeDTO
    {
        public int ID { get; set; }

        public string LeaveTypeName { get; set; }
    }
}
