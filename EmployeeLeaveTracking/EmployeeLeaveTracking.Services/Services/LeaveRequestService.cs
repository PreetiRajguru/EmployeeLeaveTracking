using EmployeeLeaveTracking.Data.Context;
using EmployeeLeaveTracking.Data.DTOs;
using EmployeeLeaveTracking.Data.Mappers;
using EmployeeLeaveTracking.Data.Models;
using EmployeeLeaveTracking.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace EmployeeLeaveTracking.Services.Services
{
    public class LeaveRequestService : ILeaveRequest
    {
        private readonly EmployeeLeaveDbContext _dbContext;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly UserService _userService;
        private readonly UserManager<User> _userManager;
        private User? _user;
        public LeaveRequestService(EmployeeLeaveDbContext dbContext, UserManager<User> userManager, IHttpContextAccessor httpContextAccessor, UserService userService)
        {
            _dbContext = dbContext;
            _userManager = userManager;
            _httpContextAccessor = httpContextAccessor;
            _userService = userService;
        }

        public IEnumerable<LeaveRequestDTO> GetAll()
        {
            try
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
            }catch(Exception ex)
            {
                throw new Exception();
            }
            
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


        public List<UserLeaveRequestDTO> GetAllLeavesByEmployeeId(int limit, int offset)
        {
            var Id = _userService.GetCurrentUserById();

            IQueryable<LeaveRequest> leaveRequestByEmployeeId = _dbContext.LeaveRequests
                .Include(m => m.Manager)
                .Include(m => m.Employee)
                .Include(m => m.LeaveType)
                .Include(m => m.StatusMaster)
                .Where(c => c.EmployeeId.Equals(Id))
                .Skip(offset)
                .Take(limit);

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


        public double LeaveBalance(string employeeId)
        {
            StatusMaster? approvedLeaves = _dbContext.Status.Where(s => s.StatusType.ToLower() == "approved").FirstOrDefault();
            List<LeaveRequest> leaveRequests = _dbContext.LeaveRequests
                .Where(lr => lr.EmployeeId.Equals(employeeId) && lr.StatusId == approvedLeaves.Id)
                .ToList(); int totalDays = leaveRequests.Sum(lr => lr.TotalDays);

            DateTime dt = DateTime.Now;
            int month = dt.Month;

            double balance = (month * 1.5) - totalDays;

            return balance;
        }
    }
}