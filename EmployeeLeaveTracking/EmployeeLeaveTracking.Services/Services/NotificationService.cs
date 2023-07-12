using AutoMapper;
using EmployeeLeaveTracking.Data.Context;
using EmployeeLeaveTracking.Data.DTOs;
using EmployeeLeaveTracking.Services.Interfaces;

namespace EmployeeLeaveTracking.Services.Services
{
    public class NotificationService : INotification
    {
        private readonly IMapper _mapper;
        private readonly EmployeeLeaveDbContext _dbContext;
        private readonly UserService _userService;

        public NotificationService(IMapper mapper, EmployeeLeaveDbContext dbContext, UserService userService)
        {
            _mapper = mapper;
            _dbContext = dbContext;
            _userService = userService;
        }



        //manager method
        /*  public IEnumerable<DetailedNotificationDTO> GetAllNotificationsForManager()
          {
              string Id = _userService.GetCurrentUserById();

              var notifications = _dbContext.Notifications
                  .Where(n => n.UserId == Id)
                  .ToList();

              var notificationDTOs = _mapper.Map<List<DetailedNotificationDTO>>(notifications);

              foreach (var notificationDTO in notificationDTOs)
              {

                  // Fetch leave details based on LeaveRequestId and update the DTO
                  var leave = _dbContext.LeaveRequests.FirstOrDefault(l => l.Id == notificationDTO.LeaveRequestId);
                  if (leave != null)
                  {
                      var leaveDTO = new DetailedLeaveDTO
                      {
                          LeaveId = leave.Id,
                          StartDate = leave.StartDate,
                          EndDate = leave.EndDate,
                          RequestComments = leave.RequestComments,
                          StatusId = leave.StatusId,
                          LeaveTypeId = leave.LeaveTypeId,
                          TotalDays = leave.TotalDays,
                      };

                      // Fetch leave type details based on LeaveTypeId and update the DTO
                      var leaveType = _dbContext.LeaveTypes.FirstOrDefault(lt => lt.Id == leaveDTO.LeaveTypeId);
                      if (leaveType != null)
                      {
                          leaveDTO.LeaveTypeName = leaveType.LeaveTypeName;
                      }

                      notificationDTO.Leave = leaveDTO;

                      // Fetch employee details based on EmployeeId and update the DTO
                      var employee = _dbContext.Users.FirstOrDefault(u => u.Id == leave.EmployeeId);
                      if (employee != null)
                      {
                          leaveDTO.FirstName = employee.FirstName;
                          leaveDTO.LastName = employee.LastName;
                      }

                      var statusName = _dbContext.Status.FirstOrDefault(lt => lt.Id == leaveDTO.StatusId);
                      if (statusName != null)
                      {
                          leaveDTO.StatusName = statusName.StatusType;
                      }
                  }
              }

              return notificationDTOs;
          }*/








        public IEnumerable<DetailedNotificationDTO> GetAllNotificationsForManager()
        {
            string id = _userService.GetCurrentUserById();

            var notifications = _dbContext.Notifications
                .Where(n => n.UserId == id)
                .ToList();

            var notificationDTOs = new List<DetailedNotificationDTO>();

            foreach (var notification in notifications)
            {
                var notificationDTO = new DetailedNotificationDTO
                {
                    Id = notification.Id, // Set the Id property in the DTO
                    UserId = notification.UserId,
                    LeaveRequestId = notification.LeaveRequestId,
                    NotificationTypeId = notification.NotificationTypeId,
                    IsViewed = notification.IsViewed
                };

                // Fetch leave details based on LeaveRequestId and update the DTO
                var leave = _dbContext.LeaveRequests.FirstOrDefault(l => l.Id == notificationDTO.LeaveRequestId);
                if (leave != null)
                {
                    var leaveDTO = new DetailedLeaveDTO
                    {
                        LeaveId = leave.Id,
                        StartDate = leave.StartDate,
                        EndDate = leave.EndDate,
                        RequestComments = leave.RequestComments,
                        StatusId = leave.StatusId,
                        LeaveTypeId = leave.LeaveTypeId,
                        TotalDays = leave.TotalDays
                    };

                    // Fetch leave type details based on LeaveTypeId and update the DTO
                    var leaveType = _dbContext.LeaveTypes.FirstOrDefault(lt => lt.Id == leaveDTO.LeaveTypeId);
                    if (leaveType != null)
                    {
                        leaveDTO.LeaveTypeName = leaveType.LeaveTypeName;
                    }

                    notificationDTO.Leave = leaveDTO;

                    // Fetch employee details based on EmployeeId and update the DTO
                    var employee = _dbContext.Users.FirstOrDefault(u => u.Id == leave.EmployeeId);
                    if (employee != null)
                    {
                        leaveDTO.FirstName = employee.FirstName;
                        leaveDTO.LastName = employee.LastName;
                    }

                    var statusName = _dbContext.Status.FirstOrDefault(s => s.Id == leaveDTO.StatusId);
                    if (statusName != null)
                    {
                        leaveDTO.StatusName = statusName.StatusType;
                    }
                }

                notificationDTOs.Add(notificationDTO);
            }

            return notificationDTOs;
        }




















        public IEnumerable<DetailedNotificationDTO> GetAllNotificationsForEmployee()
        {
            string id = _userService.GetCurrentUserById();

            var notifications = _dbContext.Notifications
                .Where(n => n.UserId == id)
                .ToList();

            var notificationDTOs = new List<DetailedNotificationDTO>();

            foreach (var notification in notifications)
            {
                var notificationDTO = new DetailedNotificationDTO
                {
                    Id = notification.Id, // Set the Id property in the DTO
                    UserId = notification.UserId,
                    LeaveRequestId = notification.LeaveRequestId,
                    NotificationTypeId = notification.NotificationTypeId,
                    IsViewed = notification.IsViewed
                };

                // Fetch leave details based on LeaveRequestId and update the DTO
                var leave = _dbContext.LeaveRequests.FirstOrDefault(l => l.Id == notificationDTO.LeaveRequestId);
                if (leave != null)
                {
                    var leaveDTO = new DetailedLeaveDTO
                    {
                        LeaveId = leave.Id,
                        StartDate = leave.StartDate,
                        EndDate = leave.EndDate,
                        RequestComments = leave.RequestComments,
                        StatusId = leave.StatusId,
                        LeaveTypeId = leave.LeaveTypeId,
                        TotalDays = leave.TotalDays
                    };

                    // Fetch leave type details based on LeaveTypeId and update the DTO
                    var leaveType = _dbContext.LeaveTypes.FirstOrDefault(lt => lt.Id == leaveDTO.LeaveTypeId);
                    if (leaveType != null)
                    {
                        leaveDTO.LeaveTypeName = leaveType.LeaveTypeName;
                    }

                    notificationDTO.Leave = leaveDTO;

                    // Fetch employee details based on EmployeeId and update the DTO
                    var manager = _dbContext.Users.FirstOrDefault(u => u.Id == leave.ManagerId);
                    if (manager != null)
                    {
                        leaveDTO.FirstName = manager.FirstName;
                        leaveDTO.LastName = manager.LastName;
                    }

                    var statusName = _dbContext.Status.FirstOrDefault(s => s.Id == leaveDTO.StatusId);
                    if (statusName != null)
                    {
                        leaveDTO.StatusName = statusName.StatusType;
                    }
                }

                notificationDTOs.Add(notificationDTO);
            }

            return notificationDTOs;
        }






        //employee method
        /*public IEnumerable<DetailedNotificationDTO> GetAllNotificationsForEmployee()
        {
            string Id = _userService.GetCurrentUserById();

            var notifications = _dbContext.Notifications
                .Where(n => n.UserId == Id)
                .ToList();

            var notificationDTOs = _mapper.Map<List<DetailedNotificationDTO>>(notifications);

            foreach (var notificationDTO in notificationDTOs)
            {

                // Fetch leave details based on LeaveRequestId and update the DTO
                var leave = _dbContext.LeaveRequests.FirstOrDefault(l => l.Id == notificationDTO.LeaveRequestId);
                if (leave != null)
                {
                    var leaveDTO = new DetailedLeaveDTO
                    {
                        LeaveId = leave.Id,
                        StartDate = leave.StartDate,
                        EndDate = leave.EndDate,
                        RequestComments = leave.RequestComments,
                        StatusId = leave.StatusId,
                        LeaveTypeId = leave.LeaveTypeId,
                    };

                    notificationDTO.Leave = leaveDTO;

                    // Fetch employee details based on EmployeeId and update the DTO
                    var manager = _dbContext.Users.FirstOrDefault(u => u.Id == leave.ManagerId);
                    if (manager != null)
                    {
                        leaveDTO.FirstName = manager.FirstName;
                        leaveDTO.LastName = manager.LastName;
                    }
                }
            }

            return notificationDTOs;
        }*/




        /*public IEnumerable<DetailedNotificationDTO> GetAllNotificationsForEmployee()
        {
            string Id = _userService.GetCurrentUserById();

            var notifications = _dbContext.Notifications
                .Where(n => n.UserId == Id)
                .ToList();

            var notificationDTOs = _mapper.Map<List<DetailedNotificationDTO>>(notifications);

            foreach (var notificationDTO in notificationDTOs)
            {
                var leave = _dbContext.LeaveRequests.FirstOrDefault(l => l.Id == notificationDTO.LeaveRequestId);
                if (leave != null)
                {
                    var leaveDTO = new DetailedLeaveDTO
                    {
                        LeaveId = leave.Id,
                        StartDate = leave.StartDate,
                        EndDate = leave.EndDate,
                        RequestComments = leave.RequestComments,
                        StatusId = leave.StatusId,
                        LeaveTypeId = leave.LeaveTypeId,
                        TotalDays = leave.TotalDays,
                    };

                    // Fetch leave type details based on LeaveTypeId and update the DTO
                    var leaveType = _dbContext.LeaveTypes.FirstOrDefault(lt => lt.Id == leaveDTO.LeaveTypeId);
                    if (leaveType != null)
                    {
                        leaveDTO.LeaveTypeName = leaveType.LeaveTypeName;
                    }

                    notificationDTO.Leave = leaveDTO;

                    var manager = _dbContext.Users.FirstOrDefault(u => u.Id == leave.ManagerId);
                    if (manager != null)
                    {
                        leaveDTO.FirstName = manager.FirstName;
                        leaveDTO.LastName = manager.LastName;
                    }

                    var statusName = _dbContext.Status.FirstOrDefault(lt => lt.Id == leaveDTO.StatusId);
                    if (statusName != null)
                    {
                        leaveDTO.StatusName = statusName.StatusType;
                    }
                }
            }

            return notificationDTOs;
        }*/




        // isviewed to 1 
        public void MarkNotificationAsViewed(int notificationId)
        {
            var notification = _dbContext.Notifications.Find(notificationId);
            if (notification != null)
            {
                notification.IsViewed = true;
                _dbContext.SaveChanges();
            }
        }
    }
}





   
