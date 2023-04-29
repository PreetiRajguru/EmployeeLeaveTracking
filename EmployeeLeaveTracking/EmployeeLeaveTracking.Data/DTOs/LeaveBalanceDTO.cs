using System.ComponentModel.DataAnnotations;

namespace EmployeeLeaveTracking.Data.DTOs
{
    public class LeaveBalanceDTO
    {
        public int Id { get; set; }


        [Required(ErrorMessage = "Employee ID is a required field.")]
        public int EmployeeId { get; set; }

        [Required(ErrorMessage = "Leave type ID is a required field.")]
        public int LeaveTypeId { get; set; }


        [Required(ErrorMessage = "Balance is a required field.")]
        [Range(0, double.MaxValue, ErrorMessage = "Balance must be a positive number.")]
        public decimal Balance { get; set; }


        [Required(ErrorMessage = "Year month is a required field.")]
        [Range(190001, 999912, ErrorMessage = "Year month must be in the format of YYYYMM.")]
        public int YearMonth { get; set; }

    }
}
