using EmployeeLeaveTracking.Data.DTOs;
using EmployeeLeaveTracking.Services.Interfaces;
using EmployeeLeaveTracking.Services.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeLeaveTracking.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class NotificationsController : ControllerBase
    {
        private readonly INotification _notificationService;
        private readonly UserService _userService;

        public NotificationsController(INotification notificationService, UserService userService)
        {
            _notificationService = notificationService;
            _userService = userService;
        }


        [HttpGet("all")]
        [Authorize(Roles = "Manager,Employee")]
        public IActionResult GetAllNotifications()
        {
            try
            {
                string id = _userService.GetCurrentUserById();
                string userRole = _userService.GetCurrentUserByRole();

                if (id == null)
                {
                    return BadRequest("User ID is null.");
                }

                IEnumerable<DetailedNotificationDTO> unviewedNotifications = _notificationService.GetAllNotifications(id,userRole);

                if (unviewedNotifications == null)
                {
                    return NotFound("No notifications found for the manager.");
                }

                return Ok(unviewedNotifications);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while processing the request.");
            }
        }

        [HttpPost("{id}/viewed")]
        /*[Authorize(Roles = "Manager,Employee")]*/
        public IActionResult MarkNotificationAsViewed(int id)
        {
            try
            {
                if (id <= 0)
                {
                    return BadRequest("Invalid notification ID.");
                }

                _notificationService.MarkNotificationAsViewed(id);

                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while processing the request.");
            }
        }



        [HttpGet("count")]
/*        [Authorize(Roles = "Manager,Employee")]*/
        public ActionResult<int> GetNotViewedNotificationCount()
        {
            try
            {
                string id = _userService.GetCurrentUserById();

                if (id == null)
                {
                    return BadRequest("User ID is null.");
                }

                int count = _notificationService.GetNotViewedNotificationCount(id);

                return Ok(count);
            }
            catch (Exception)
            {
                return StatusCode(500, "An error occurred while processing the request.");
            }
        }
    }
}