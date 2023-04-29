using System.ComponentModel.DataAnnotations;

namespace EmployeeLeaveTracking.Data.DTOs
{
    public class LeaveBalanceDTO
    {
        public int ID { get; set; }


        [Required(ErrorMessage = "Employee ID is a required field.")]
        public int EmployeeID { get; set; }

        [Required(ErrorMessage = "Leave type ID is a required field.")]
        public int LeaveTypeID { get; set; }


        [Required(ErrorMessage = "Balance is a required field.")]
        [Range(0, double.MaxValue, ErrorMessage = "Balance must be a positive number.")]
        public decimal Balance { get; set; }


        [Required(ErrorMessage = "Year month is a required field.")]
        [Range(190001, 999912, ErrorMessage = "Year month must be in the format of YYYYMM.")]
        public int YearMonth { get; set; }

    }
}
