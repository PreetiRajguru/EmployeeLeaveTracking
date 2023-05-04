using EmployeeLeaveTracking.Data.Context;
using EmployeeLeaveTracking.Data.DTOs;
using EmployeeLeaveTracking.Data.Models;
using EmployeeLeaveTracking.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace EmployeeLeaveTracking.Services.Services
{
    public class LeaveTypeService : ILeaveType
    {
        private readonly EmployeeLeaveDbContext _dbContext;
        private readonly UserManager<User> _userManager;
        private User? _user;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public LeaveTypeService(EmployeeLeaveDbContext dbContext, UserManager<User> userManager, IHttpContextAccessor httpContextAccessor)
        {
            _dbContext = dbContext;
            _userManager = userManager;
            _httpContextAccessor = httpContextAccessor;
        }

        public IEnumerable<LeaveTypeDTO> GetAll()
        {
            var leaveTypes = _dbContext.LeaveTypes.Where(lt => lt.IsDeleted == (false)).Select(lt => new LeaveTypeDTO
            {
                Id = lt.Id,
                LeaveTypeName = lt.LeaveTypeName
            });

            return leaveTypes;
        }

        public LeaveTypeDTO GetById(int id)
        {
            var leaveType = _dbContext.LeaveTypes.Where(lt => lt.IsDeleted == (false) && lt.Id == id).Select(lt => new LeaveTypeDTO
            {
                Id = lt.Id,
                LeaveTypeName = lt.LeaveTypeName
            }).FirstOrDefault();

            return leaveType;
        }

        public LeaveTypeDTO Create(LeaveTypeDTO leaveType)
        {
            var newLeaveType = new LeaveType
            {
                LeaveTypeName = leaveType.LeaveTypeName
            };

            _dbContext.LeaveTypes.Add(newLeaveType);
            _dbContext.SaveChanges();

            leaveType.Id = newLeaveType.Id;

            return leaveType;
        }

        public LeaveTypeDTO Update(LeaveTypeDTO leaveType)
        {
            var existingLeaveType = _dbContext.LeaveTypes.Find(leaveType.Id);

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
            var existingLeaveType = _dbContext.LeaveTypes.Find(id);

            if (existingLeaveType == null || existingLeaveType.IsDeleted == (true))
            {
                return false;
            }

            existingLeaveType.IsDeleted = true;

            _dbContext.SaveChanges();

            return true;
        }

  

        public List<LeaveTypeWithTotalDaysDTO> GetLeaveTypesWithTotalDaysTaken()
        {
            StatusMaster? approvedLeaves = _dbContext.Status.Where(s => s.StatusType.ToLower() == "approved").FirstOrDefault();

            var id = _httpContextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.Sid);

            var leaveTypesWithTotalDays = _dbContext.LeaveTypes
                .Select(lt => new LeaveTypeWithTotalDaysDTO
                {
                    LeaveTypeId = lt.Id,
                    LeaveTypeName = lt.LeaveTypeName,
                    TotalDaysTaken = _dbContext.LeaveRequests
                        .Where(lr => lr.EmployeeId == id && lr.LeaveTypeId == lt.Id && lr.StatusId == approvedLeaves.Id)
                        .Sum(lr => lr.TotalDays)
                })
                .ToList();

            return leaveTypesWithTotalDays;
        }
    }
}
