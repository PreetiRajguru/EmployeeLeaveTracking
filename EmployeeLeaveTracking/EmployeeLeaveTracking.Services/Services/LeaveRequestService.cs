using AutoMapper;
using EmailService;
using EmployeeLeaveTracking.Data.Context;
using EmployeeLeaveTracking.Data.DTOs;
using EmployeeLeaveTracking.Data.Mappers;
using EmployeeLeaveTracking.Data.Models;
using EmployeeLeaveTracking.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Resources;

namespace EmployeeLeaveTracking.Services.Services
{
    public class LeaveRequestService : ILeaveRequest
    {
        private readonly EmployeeLeaveDbContext _dbContext;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly UserService _userService;
        private readonly UserManager<User> _userManager;
        private User? _user;
        private readonly IMapper _mapper;

        public LeaveRequestService(EmployeeLeaveDbContext dbContext, UserManager<User> userManager, IHttpContextAccessor httpContextAccessor, UserService userService, IMapper mapper)
        {
            _dbContext = dbContext;
            _userManager = userManager;
            _httpContextAccessor = httpContextAccessor;
            _userService = userService;
            _mapper = mapper;
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

        public NewLeaveRequestDTO NewCreateNewLeaveRequest(NewLeaveRequestDTO leaveRequest)
        {
            //checking if there are any existing approved leave requests for the same user and overlapping dates
            bool isLeaveAlreadyExists = _dbContext.LeaveRequests
                .Any(lr => lr.EmployeeId == leaveRequest.EmployeeId &&
                           lr.StartDate <= leaveRequest.EndDate &&
                           lr.EndDate >= leaveRequest.StartDate &&
                           lr.StatusId == 2);


            if (isLeaveAlreadyExists)
            {
                throw new Exception("Leave request already approved for the same dates.");
            }

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

            LeaveBalance? balance = _dbContext.LeaveBalances
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
                balance.Balance -= leaveRequest.TotalDays;
            }

            Notification notification = _mapper.Map<NotificationDTO, Notification>(new NotificationDTO
            {
                UserId = leaveRequest.ManagerId,
                LeaveRequestId = newLeaveRequest.Id,
                NotificationTypeId = 1,
                IsViewed = false
            });

            _dbContext.Notifications.Add(notification);
            _dbContext.SaveChanges();

            // Retrieve employee and manager details
            User employee = _dbContext.Users.FirstOrDefault(u => u.Id == leaveRequest.EmployeeId);
            User manager = _dbContext.Users.FirstOrDefault(u => u.Id == leaveRequest.ManagerId);

            if (employee == null || manager == null)
            {
                throw new Exception("Employee or manager not found.");
            }

            User managerEmail = _dbContext.Users.FirstOrDefault(u => u.Id == leaveRequest.ManagerId);

            if (managerEmail == null)
            {
                throw new Exception("Manager not found.");
            }

            //accessing the resource file
            ResourceManager resourceManager = new ResourceManager("EmailService.EmailTemplates", typeof(EmailHelper).Assembly);

            //retrieving the LeaveRequestTemplate from the resource file
            string leaveRequestTemplate = resourceManager.GetString("LeaveRequestTemplate");

            //replacing placeholders with appropriate values
            string emailBody = string.Format(leaveRequestTemplate,
                employee.FirstName + " " + employee.LastName,
                manager.FirstName + " " + manager.LastName,
                leaveRequest.StartDate.ToShortDateString(),
                leaveRequest.EndDate.ToShortDateString(),
                leaveRequest.TotalDays,
                leaveRequest.RequestComments);

            EmailHelper emailService = new EmailHelper();
            string emailToAddress = manager.Email; //manager's email address
            string subject = "Leave Request";
            emailService.SendEmail(emailToAddress, subject, emailBody);

            return leaveRequest;
        }


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

            if (statusId == 2 || statusId == 3)
            {
                NotificationDTO notification = new NotificationDTO
                {
                    UserId = leaveRequest.EmployeeId,
                    LeaveRequestId = leaveRequest.Id,
                    NotificationTypeId = 2,
                    IsViewed = false
                };

                var notificationEntity = _mapper.Map<Notification>(notification);
                _dbContext.Notifications.Add(notificationEntity);
                await _dbContext.SaveChangesAsync();
            }

            User employeeEmail = await _dbContext.Users.FindAsync(leaveRequest.EmployeeId);

            if (employeeEmail == null)
            {
                throw new Exception("Employee not found.");
            }

            // using the appropriate email template based on the statusId
            string emailTemplateKey = (statusId == 2) ? "LeaveApprovedTemplate" : "LeaveRejectedTemplate";

            //accessing the resource file
            ResourceManager resourceManager = new ResourceManager("EmailService.EmailTemplates", typeof(EmailHelper).Assembly);

            //retrieving the email template from the resource file
            string emailTemplate = resourceManager.GetString(emailTemplateKey);

            //replace placeholders with appropriate values
            string leaveStatus = (statusId == 2) ? "Approved" : "Rejected";
            string emailBody = string.Format(emailTemplate,
                leaveRequest.StartDate.ToShortDateString(),
                leaveRequest.EndDate.ToShortDateString(),
                leaveRequest.TotalDays,
                leaveRequest.RequestComments,
                leaveStatus);
            //return manager and employee names with this response to display in email template

            EmailHelper emailService = new EmailHelper();
            string emailToAddress = "rajgurupreeti0@gmail.com";
            /*string emailToAddress = employeeEmail.Email;*/
            string subject = "Leave Response";
            emailService.SendEmail(emailToAddress, subject, emailBody);

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