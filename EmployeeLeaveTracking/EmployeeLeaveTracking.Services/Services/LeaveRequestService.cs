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
                List<LeaveRequestDTO> leaveRequests = _dbContext.LeaveRequests
                    .Where(lr => lr.IsDeleted == (false))
                    .Select(lr => new LeaveRequestDTO
                    {
                        Id = lr.Id,
                        RequestComments = lr.RequestComments,
                        EmployeeId = lr.EmployeeId,
                        LeaveTypeId = lr.LeaveTypeId,
                        StartDate = lr.StartDate,
                        EndDate = lr.EndDate,
                        TotalDays = lr.TotalDays,
                        StatusId = lr.StatusId
                    })
                    .ToList();

                if (leaveRequests.Count == 0)
                {
                    throw new Exception("No leave requests found.");
                }

                return leaveRequests;
            }
            catch (Exception ex)
            {
                throw new Exception("Error retrieving leave requests.", ex);
            }
        }


        public LeaveRequestDTO? GetById(int id)
        {
            //var leaveRequest = _dbContext.LeaveRequests
            //    .SingleOrDefault(lr => lr.Id == id && lr.IsDeleted == (false));

            //if (leaveRequest == null)
            //{
            //    return null;
            //}

            //return new LeaveRequestDTO
            //{
            //    Id = leaveRequest.Id,
            //    RequestComments = leaveRequest.RequestComments,
            //    EmployeeId = leaveRequest.EmployeeId,
            //    LeaveTypeId = leaveRequest.LeaveTypeId,
            //    StartDate = (DateTime)leaveRequest.StartDate,
            //    EndDate = (DateTime)leaveRequest.EndDate,
            //    TotalDays = leaveRequest.TotalDays,
            //    StatusId = leaveRequest.StatusId
            //};

            return _dbContext.LeaveRequests
                .Where(lr => lr.Id == id && lr.IsDeleted == (false))
                .Select(leaveRequest => new LeaveRequestDTO
                {
                    Id = leaveRequest.Id,
                    RequestComments = leaveRequest.RequestComments,
                    EmployeeId = leaveRequest.EmployeeId,
                    LeaveTypeId = leaveRequest.LeaveTypeId,
                    StartDate = leaveRequest.StartDate,
                    EndDate = leaveRequest.EndDate,
                    TotalDays = leaveRequest.TotalDays,
                    StatusId = leaveRequest.StatusId
                }).SingleOrDefault();
        }

        public LeaveRequestDTO Create(LeaveRequestDTO leaveRequest)
        {
            LeaveRequest newLeaveRequest = new()
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

       public NewLeaveRequestDTO CreateNewLeaveRequest(NewLeaveRequestDTO leaveRequest)
        {
            LeaveRequest newLeaveRequest = new LeaveRequest
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


        //new method
        public NewLeaveRequestDTO NewCreateNewLeaveRequest(NewLeaveRequestDTO leaveRequest)
        {
            LeaveRequest newLeaveRequest = new()
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

            var balance = _dbContext.LeaveBalances
            .FirstOrDefault(b => b.UserId == leaveRequest.EmployeeId && b.LeaveTypeId == leaveRequest.LeaveTypeId);

            if (balance == null)
            {
                balance = new LeaveBalance
                {
                    UserId = leaveRequest.EmployeeId,
                    LeaveTypeId = leaveRequest.LeaveTypeId,
                    Balance = -leaveRequest.TotalDays
                };
                _dbContext.LeaveBalances.Add(balance);
            }
            else
            {
                balance.Balance = balance.Balance - leaveRequest.TotalDays;
            }

            _dbContext.SaveChanges();

            return leaveRequest;

        }


        //trial for adding logic of comp-off leave 























        

        public LeaveRequestDTO Update(LeaveRequestDTO leaveRequest)
        {
            LeaveRequest? existingLeaveRequest = _dbContext.LeaveRequests
                .SingleOrDefault(lr => lr.Id == leaveRequest.Id && lr.IsDeleted == false);

            if (existingLeaveRequest == null)
            {
                throw new ArgumentException("The leave request with the given ID does not exist or has been deleted.");
            }

            if (leaveRequest.StartDate > leaveRequest.EndDate)
            {
                throw new ArgumentException("The start date cannot be later than the end date.");
            }

            int totalDays = (int)(leaveRequest.EndDate - leaveRequest.StartDate).TotalDays + 1;
            if (totalDays != leaveRequest.TotalDays)
            {
                throw new ArgumentException("The total days must be equal to the number of days between start date and end date.");
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
            string Id = _userService.GetCurrentUserById();

            if (string.IsNullOrEmpty(Id))
            {
                throw new ArgumentNullException(nameof(Id), "The employee ID cannot be null or empty.");
            }

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
            if (statusId <= 0)
            {
                throw new ArgumentException("Invalid status ID");
            }

            List<LeaveRequest> leaveRequests = await _dbContext.LeaveRequests
                .Include(lr => lr.Employee) 
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
                EmployeeFirstName = lr.Employee.FirstName,
                EmployeeLastName = lr.Employee.LastName,
                LeaveTypeId = lr.LeaveTypeId,
                StatusId = lr.StatusId
            }).ToList();
        }


        public List<LeaveRequestDTO> GetLeaveRequestsByStatusAndManager(int statusId, string managerId)
        {
            if (string.IsNullOrEmpty(managerId))
            {
                throw new ArgumentNullException(nameof(managerId));
            }
            List<LeaveRequestDTO> leaveRequests = _dbContext.LeaveRequests
                .Where(lr => lr.StatusId == statusId && lr.ManagerId == managerId)
                .Select(lr => new LeaveRequestDTO
                {
                    Id = lr.Id,
                    RequestComments = lr.RequestComments,
                    StartDate = lr.StartDate,
                    EndDate = lr.EndDate,
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
            if (id <= 0)
            {
                throw new ArgumentException("Leave ID must be greater than zero.", nameof(id));
            }

            return await _dbContext.LeaveRequests.FirstOrDefaultAsync(l => l.Id == id);
        }

        public async Task<int> UpdateLeaveRequestStatus(int id, int statusId)
        {
            LeaveRequest leaveRequest = await GetLeaveById(id);

            if (leaveRequest == null)
            {
                return statusId;
            }

            leaveRequest.StatusId = statusId;
            _dbContext.Entry(leaveRequest).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();

            if (statusId == 3)
            {
                LeaveBalance leaveBalance = await _dbContext.LeaveBalances.FirstOrDefaultAsync(b =>
                    b.UserId == leaveRequest.EmployeeId && b.LeaveTypeId == leaveRequest.LeaveTypeId);

                if (leaveBalance != null)
                {
                    leaveBalance.Balance += leaveRequest.TotalDays;
                    leaveBalance.ModifiedDate = DateTime.Now;

                    _dbContext.Entry(leaveBalance).State = EntityState.Modified;
                    await _dbContext.SaveChangesAsync();
                }
            }

            return statusId;
        }

        public double LeaveBalance(string employeeId)
        {
            StatusMaster? approvedLeaves = _dbContext.Status.Where(s => s.StatusType.ToLower() == "approved").FirstOrDefault();
            List<LeaveRequest> leaveRequests = _dbContext.LeaveRequests
                .Where(lr => lr.EmployeeId.Equals(employeeId) && lr.StatusId == approvedLeaves.Id)
                .ToList(); 
            
            int totalDays = leaveRequests.Sum(lr => lr.TotalDays);

            DateTime dt = DateTime.Now;
            int month = dt.Month;

            double balance = (month * 1.5) - totalDays;

            return balance;
        }


        public List<UserLeaveRequestDTO> LeavesByEmployeeId(string employeeId)
        {
            IQueryable<LeaveRequest> leaveRequestByEmployeeId = _dbContext.LeaveRequests
            
            .Include(m => m.Employee)
            .Include(m => m.LeaveType)
            .Include(m => m.StatusMaster)
            .Where(c => c.EmployeeId.Equals(employeeId)); 

            return leaveRequestByEmployeeId.Select(c => new UserLeaveRequestMapper().Map(c)).ToList();
        }           
    }
}