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

        [HttpGet("unviewed")]
        public IActionResult GetUnviewedNotifications()
        {
            var unviewedNotifications = _notificationService.GetUnviewedNotifications();
            return Ok(unviewedNotifications);
        }

        [HttpPost("{id}/viewed")]
        public IActionResult MarkNotificationAsViewed(int id)
        {
            _notificationService.MarkNotificationAsViewed(id);
            return Ok();
        }
    }
}