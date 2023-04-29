using EmployeeLeaveTracking.Data.Context;
using EmployeeLeaveTracking.Data.DTOs;
using EmployeeLeaveTracking.Data.Models;
using EmployeeLeaveTracking.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeLeaveTracking.Services.Services
{
    public class LeaveRequestService : ILeaveRequest
    {
        private readonly EmployeeLeaveDbContext _dbContext;

        public LeaveRequestService(EmployeeLeaveDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IEnumerable<LeaveRequestDTO> GetAllLeaveRequests()
        {
            return _dbContext.LeaveRequests
                .Where(lr => lr.IsDeleted == (false))
                .Select(lr => new LeaveRequestDTO
                {
                    Id = lr.Id,
                    RequestComments = lr.RequestComments,
                    EmployeeId = lr.EmployeeId,
                    LeaveTypeId = lr.LeaveTypeId,
                    StartDate = (DateTime)lr.StartDate,
                    EndDate = (DateTime)lr.EndDate,
                    StatusId = lr.StatusId
                })
                .ToList();
        }

        public LeaveRequestDTO GetLeaveRequestById(int id)
        {
            var leaveRequest = _dbContext.LeaveRequests
                .SingleOrDefault(lr => lr.Id == id && lr.IsDeleted == (false));

            if (leaveRequest == null)
            {
                return null;
            }

            return new LeaveRequestDTO
            {
                Id = leaveRequest.Id,
                RequestComments = leaveRequest.RequestComments,
                EmployeeId = leaveRequest.EmployeeId,
                LeaveTypeId = leaveRequest.LeaveTypeId,
                StartDate = (DateTime)leaveRequest.StartDate,
                EndDate = (DateTime)leaveRequest.EndDate,
                StatusId = leaveRequest.StatusId
            };
        }

        public LeaveRequestDTO AddLeaveRequest(LeaveRequestDTO leaveRequest)
        {
            var newLeaveRequest = new LeaveRequest
            {
                RequestComments = leaveRequest.RequestComments,
                EmployeeId = leaveRequest.EmployeeId,
                LeaveTypeId = leaveRequest.LeaveTypeId,
                StartDate = leaveRequest.StartDate,
                EndDate = leaveRequest.EndDate,
                StatusId = leaveRequest.StatusId
            };

            _dbContext.LeaveRequests.Add(newLeaveRequest);
            _dbContext.SaveChanges();

            leaveRequest.Id = newLeaveRequest.Id;

            return leaveRequest;
        }

        public LeaveRequestDTO UpdateLeaveRequest(LeaveRequestDTO leaveRequest)
        {
            var existingLeaveRequest = _dbContext.LeaveRequests
                .SingleOrDefault(lr => lr.Id == leaveRequest.Id && lr.IsDeleted == (false));

            if (existingLeaveRequest == null)
            {
                return null;
            }

            existingLeaveRequest.RequestComments = leaveRequest.RequestComments;
            existingLeaveRequest.EmployeeId = leaveRequest.EmployeeId;
            existingLeaveRequest.LeaveTypeId = leaveRequest.LeaveTypeId;
            existingLeaveRequest.StartDate = leaveRequest.StartDate;
            existingLeaveRequest.EndDate = leaveRequest.EndDate;
            existingLeaveRequest.StatusId = leaveRequest.StatusId;

            _dbContext.SaveChanges();

            return leaveRequest;
        }

        public bool DeleteLeaveRequest(int id)
        {
            var leaveRequest = _dbContext.LeaveRequests
                .SingleOrDefault(lr => lr.Id == id && lr.IsDeleted == (false));

            if (leaveRequest == null)
            {
                return false;
            }

            leaveRequest.IsDeleted = true;

            _dbContext.SaveChanges();

            return true;
        }
    }
}