using EmployeeLeaveTracking.Data.DTOs;
using EmployeeLeaveTracking.Data.Models;
using EmployeeLeaveTracking.Services.Interfaces;
using EmployeeLeaveTracking.Services.Services;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeLeaveTracking.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ManagerController : Controller
    {
        private readonly IManager _managerService;

        public ManagerController(IManager managerService)
        {
            _managerService = managerService;
        }

        [HttpGet]
        public IActionResult GetAllManagers()
        {
            var managers = _managerService.GetAllManagers();
            return Ok(managers);
        }

        [HttpGet("{id}")]
        public IActionResult GetManagerById(int id)
        {
            var manager = _managerService.GetManagerById(id);
            if (manager == null)
            {
                return NotFound();
            }
            return Ok(manager);
        }

        [HttpPost]
        public IActionResult CreateManager([FromBody] ManagerDTO manager)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            var createdManager = _managerService.CreateManager(manager);
            return CreatedAtAction(nameof(GetManagerById), new { id = createdManager.Id }, createdManager);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateManager(int id, [FromBody] ManagerDTO manager)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            manager.Id = id;
            var updatedManager = _managerService.UpdateManager(manager);
            if (updatedManager == null)
            {
                return NotFound();
            }
            return Ok(updatedManager);
        }


        [HttpDelete("{id}")]
        public IActionResult DeleteManager(int id)
        {
            var result = _managerService.DeleteManager(id);
            if (!result)
            {
                return NotFound();
            }
            return Ok(result);
        }
    }
}
