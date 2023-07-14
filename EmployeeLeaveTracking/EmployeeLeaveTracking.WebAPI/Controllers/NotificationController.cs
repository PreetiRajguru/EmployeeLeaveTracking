using EmployeeLeaveTracking.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeLeaveTracking.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class NotificationsController : ControllerBase
    {
        private readonly INotification _notificationService;

        public NotificationsController(INotification notificationService)
        {
            _notificationService = notificationService;
        }

        [HttpGet("manager")]
        public IActionResult GetAllNotificationsForManager()
        {
            IEnumerable<Data.DTOs.DetailedNotificationDTO> unviewedNotifications = _notificationService.GetAllNotificationsForManager();
            return Ok(unviewedNotifications);
        }

        [HttpGet("employee")]
        public IActionResult GetAllNotificationsForEmployee()
        {
            IEnumerable<Data.DTOs.DetailedNotificationDTO> unviewedNotifications = _notificationService.GetAllNotificationsForEmployee();
            return Ok(unviewedNotifications);
        }

        [HttpPost("{id}/viewed")]
        public IActionResult MarkNotificationAsViewed(int id)
        {
            _notificationService.MarkNotificationAsViewed(id);
            return Ok();
        }


        [HttpGet("count")]
        public ActionResult<int> GetNotViewedNotificationCount()
        {
            int count = _notificationService.GetNotViewedNotificationCount();
            return count;
        }
    }
}