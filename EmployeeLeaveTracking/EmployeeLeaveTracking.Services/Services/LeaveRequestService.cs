using EmployeeLeaveTracking.Data.Context;
using EmployeeLeaveTracking.Data.DTOs;
using EmployeeLeaveTracking.Data.Mappers;
using EmployeeLeaveTracking.Data.Models;
using EmployeeLeaveTracking.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace EmployeeLeaveTracking.Services.Services
{
    public class LeaveRequestService : ILeaveRequest
    {
        private readonly EmployeeLeaveDbContext _dbContext;

        public LeaveRequestService(EmployeeLeaveDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IEnumerable<LeaveRequestDTO> GetAll()
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
                    TotalDays = lr.TotalDays,
                    StatusId = lr.StatusId
                })
                .ToList();
        }

        public LeaveRequestDTO GetById(int id)
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
                TotalDays = leaveRequest.TotalDays,
                StatusId = leaveRequest.StatusId
            };
        }

        public LeaveRequestDTO Create(LeaveRequestDTO leaveRequest)
        {
            var newLeaveRequest = new LeaveRequest
            {
                RequestComments = leaveRequest.RequestComments,
                EmployeeId = leaveRequest.EmployeeId,
                ManagerId = leaveRequest.ManagerId,
                LeaveTypeId = leaveRequest.LeaveTypeId,
                StartDate = leaveRequest.StartDate,
                EndDate = leaveRequest.EndDate,
                TotalDays = leaveRequest.TotalDays,
                StatusId = leaveRequest.StatusId
            };

            _dbContext.LeaveRequests.Add(newLeaveRequest);
            _dbContext.SaveChanges();

            leaveRequest.Id = newLeaveRequest.Id;

            return leaveRequest;
        }

        public LeaveRequestDTO Update(LeaveRequestDTO leaveRequest)
        {
            var existingLeaveRequest = _dbContext.LeaveRequests
                .SingleOrDefault(lr => lr.Id == leaveRequest.Id && lr.IsDeleted == (false));

            if (existingLeaveRequest == null)
            {
                return null;
            }

            existingLeaveRequest.RequestComments = leaveRequest.RequestComments;
            existingLeaveRequest.EmployeeId = leaveRequest.EmployeeId;
            existingLeaveRequest.ManagerId = leaveRequest.ManagerId;
            existingLeaveRequest.LeaveTypeId = leaveRequest.LeaveTypeId;
            existingLeaveRequest.StartDate = leaveRequest.StartDate;
            existingLeaveRequest.EndDate = leaveRequest.EndDate;
            existingLeaveRequest.TotalDays = leaveRequest.TotalDays;
            existingLeaveRequest.StatusId = leaveRequest.StatusId;

            _dbContext.SaveChanges();

            return leaveRequest;
        }

        public bool Delete(int id)
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



        /*   public async Task<List<LeaveRequestDTO>> GetAllLeavesByEmployeeIdAsync(string employeeId)
           {
               var leaveRequests = await _dbContext.LeaveRequests
                   .Where(lr => lr.EmployeeId == employeeId)
                   .ToListAsync();

               return leaveRequests.Select(lr => new LeaveRequestDTO
               {
                   Id = lr.Id,
                   RequestComments = lr.RequestComments,
                   StartDate = (DateTime)lr.StartDate,
                   EndDate = (DateTime)lr.EndDate,
                   TotalDays = lr.TotalDays,
                   ManagerId = lr.ManagerId,
                   EmployeeId = lr.EmployeeId,
                   LeaveTypeId = lr.LeaveTypeId,
                   StatusId = lr.StatusId
               }).ToList();
           }*/





        public List<UserLeaveRequestDTO> GetAllLeavesByEmployeeId(string employeeId)
        {
            IQueryable<LeaveRequest> leaveRequestByEmployeeId = _dbContext.LeaveRequests
            .Include(m => m.Manager)
             .Include(m => m.Employee)
            .Include(m => m.LeaveType)
            .Include(m => m.StatusMaster)
            .Where(c => c.EmployeeId.Equals(employeeId));

            return leaveRequestByEmployeeId.Select(c => new UserLeaveRequestMapper().Map(c)).ToList();
        }



        public async Task<List<LeaveRequestDTO>> GetAllLeavesByStatusIdAsync(int statusId)
        {
            var leaveRequests = await _dbContext.LeaveRequests
                .Where(lr => lr.StatusId == statusId)
                .ToListAsync();

            return leaveRequests.Select(lr => new LeaveRequestDTO
            {
                Id = lr.Id,
                RequestComments = lr.RequestComments,
                StartDate = (DateTime)lr.StartDate,
                EndDate = (DateTime)lr.EndDate,
                TotalDays = lr.TotalDays,
                ManagerId = lr.ManagerId,
                EmployeeId = lr.EmployeeId,
                LeaveTypeId = lr.LeaveTypeId,
                StatusId = lr.StatusId
            }).ToList();
        }



        public List<LeaveRequestDTO> GetLeaveRequestsByStatusAndManager(int statusId, string managerId)
        {
            var leaveRequests = _dbContext.LeaveRequests
                .Where(lr => lr.StatusId == statusId && lr.ManagerId == managerId)
                .Select(lr => new LeaveRequestDTO
                {
                    Id = lr.Id,
                    RequestComments = lr.RequestComments,
                    StartDate = (DateTime)lr.StartDate,
                    EndDate = (DateTime)lr.EndDate,
                    TotalDays = lr.TotalDays,
                    ManagerId = lr.ManagerId,
                    EmployeeId = lr.EmployeeId,
                    LeaveTypeId = lr.LeaveTypeId,
                    StatusId = lr.StatusId
                })
                .ToList();

            return leaveRequests;
        }

        public async Task<LeaveRequest> GetLeaveById(int id)
        {
            return await _dbContext.LeaveRequests.FirstOrDefaultAsync(l => l.Id == id);
        }



        public async Task<int> UpdateLeaveRequestStatus(int id, int statusId)
        {
            var leaveRequest = await GetLeaveById(id);

            if (leaveRequest == null)
            {
                return statusId;
            }

            leaveRequest.StatusId = statusId;
            _dbContext.Entry(leaveRequest).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();

            return statusId;
        }

    }
}