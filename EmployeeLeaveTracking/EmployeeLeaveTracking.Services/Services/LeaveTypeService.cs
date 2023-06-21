using EmployeeLeaveTracking.Data.Context;
using EmployeeLeaveTracking.Data.DTOs;
using EmployeeLeaveTracking.Data.Models;
using EmployeeLeaveTracking.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;

namespace EmployeeLeaveTracking.Services.Services
{
    public class LeaveTypeService : ILeaveType
    {
        private readonly EmployeeLeaveDbContext _dbContext;
        private readonly UserManager<User> _userManager;
        private User? _user;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly UserService _userService;

        public LeaveTypeService(EmployeeLeaveDbContext dbContext, UserManager<User> userManager, IHttpContextAccessor httpContextAccessor, UserService userService)
        {
            _dbContext = dbContext;
            _userManager = userManager;
            _httpContextAccessor = httpContextAccessor;
            _userService = userService;
        }

        public IEnumerable<LeaveTypeDTO> GetAll()
        {
            IQueryable<LeaveTypeDTO> leaveTypes = _dbContext.LeaveTypes
                .Where(lt => lt.IsDeleted == false)
                .Select(lt => new LeaveTypeDTO
                {
                    Id = lt.Id,
                    LeaveTypeName = lt.LeaveTypeName
                });

            return leaveTypes;
        }



        public IEnumerable<LeaveTypeDTO> GetCompOff()
        {
            IQueryable<LeaveTypeDTO> leaveTypes = _dbContext.LeaveTypes
                .Where(lt => lt.IsDeleted == false && lt.LeaveTypeName == "Compensatory Off")
                .Select(lt => new LeaveTypeDTO
                {
                    Id = lt.Id,
                    LeaveTypeName = lt.LeaveTypeName
                });

            return leaveTypes;
        }

        public LeaveTypeDTO GetById(int id)
        {
            LeaveTypeDTO? leaveType = _dbContext.LeaveTypes.Where(lt => lt.IsDeleted == false && lt.Id == id).Select(lt => new LeaveTypeDTO
            {
                Id = lt.Id,
                LeaveTypeName = lt.LeaveTypeName
            }).FirstOrDefault();

            return leaveType;
        }

        public LeaveTypeDTO Create(LeaveTypeDTO leaveType)
        {
            LeaveType newLeaveType = new()
            {
                LeaveTypeName = leaveType.LeaveTypeName
            };

            _dbContext.LeaveTypes.Add(newLeaveType);
            _dbContext.SaveChanges();

            leaveType.Id = newLeaveType.Id;

            return leaveType;
        }

        public LeaveTypeDTO? Update(LeaveTypeDTO leaveType)
        {
            LeaveType existingLeaveType = _dbContext.LeaveTypes.Find(leaveType.Id);

            if (existingLeaveType == null || existingLeaveType.IsDeleted == (true))
            {
                return null;
            }

            existingLeaveType.LeaveTypeName = leaveType.LeaveTypeName;

            _dbContext.SaveChanges();

            return leaveType;
        }

        public bool Delete(int id)
        {
            LeaveType existingLeaveType = _dbContext.LeaveTypes.Find(id);

            if (existingLeaveType == null || existingLeaveType.IsDeleted == true)
            {
                return false;
            }

            existingLeaveType.IsDeleted = true;

            _dbContext.SaveChanges();

            return true;
        }

        public List<LeaveTypeWithTotalDaysDTO> GetLeaveTypesWithTotalDaysTaken()
        {
            StatusMaster approvedLeaves = _dbContext.Status.FirstOrDefault(s => s.StatusType.ToLower() == "approved");
            StatusMaster pendingLeaves = _dbContext.Status.FirstOrDefault(s => s.StatusType.ToLower() == "pending");

            string userId = _userService.GetCurrentUserById();

            List<LeaveTypeWithTotalDaysDTO> leaveTypesWithTotalDays = _dbContext.LeaveTypes
                .GroupJoin(
                    _dbContext.LeaveBalances.Where(lb => lb.UserId == userId),
                    lt => lt.Id,
                    lb => lb.LeaveTypeId,
                    (lt, lb) => new { LeaveType = lt, LeaveBalances = lb })
                .SelectMany(
                    x => x.LeaveBalances.DefaultIfEmpty(),
                    (lt, lb) => new LeaveTypeWithTotalDaysDTO
                    {
                        LeaveTypeId = lt.LeaveType.Id,
                        LeaveTypeName = lt.LeaveType.LeaveTypeName,
                        BookedDays = _dbContext.LeaveRequests
                            .Where(lr => lr.EmployeeId == userId && lr.LeaveTypeId == lt.LeaveType.Id &&
                                         (lr.StatusId == approvedLeaves.Id || lr.StatusId == pendingLeaves.Id))
                            .Sum(lr => lr.TotalDays),
                        AvailableDays = lb != null ? lb.Balance : 0
                    })
                .ToList();

            return leaveTypesWithTotalDays;
        }
    }
}