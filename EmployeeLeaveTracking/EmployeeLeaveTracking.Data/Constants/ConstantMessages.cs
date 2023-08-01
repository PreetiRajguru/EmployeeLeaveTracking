using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeLeaveTracking.Data.Constants
{
    public static class ConstantMessages
    {
        //leave request service 
        public static string NoLeaveRequests = "No leave requests found.";
        public static string ErrorLeaveRequests = "Error retrieving leave requests.";
        public static string LeaveApproved = "Leave request already approved for the same dates.";
        public static string UserNotFound = "Employee or Manager not found.";
        public static string ManagerNotFound = "Manager not found.";
        public static string EmployeeNotFound = "Employee not found.";
        public static string LeaveRequestSubject = "Leave Request";
        public static string LeaveResponseSubject = "Leave Response";
        public static string LeaveRequestDoesNotExist = "The leave request with the given ID does not exist or has been deleted.";
        public static string DateValidation = "The start date cannot be later than the end date.";
        public static string TotalDaysValidation = "The total days must be equal to the number of days between start date and end date.";
        public static string EmpIdValidation = "The employee ID cannot be null or empty.";
        public static string StatusIdValidation = "Invalid status ID.";
        public static string LeaveIdValidation = "Leave ID must be greater than zero.";
    }
}
