using EmployeeLeaveTracking.Data.DTOs;
using EmployeeLeaveTracking.Data.Models;
using EmployeeLeaveTracking.Services.Interfaces;
using EmployeeLeaveTracking.Services.Services;
using Microsoft.AspNetCore.Mvc;
using System;

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
            try
            {
                var managers = _managerService.GetAllManagers();
                return Ok(managers);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("{id}")]
        public IActionResult GetManagerById(int id)
        {
            try
            {
                var manager = _managerService.GetManagerById(id);
                if (manager == null)
                {
                    return NotFound();
                }
                return Ok(manager);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost]
        public IActionResult CreateManager([FromBody] ManagerDTO manager)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest();
                }
                var createdManager = _managerService.CreateManager(manager);
                return CreatedAtAction(nameof(GetManagerById), new { id = createdManager.Id }, createdManager);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPut("{id}")]
        public IActionResult UpdateManager(int id, [FromBody] ManagerDTO manager)
        {
            try
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
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }


        [HttpDelete("{id}")]
        public IActionResult DeleteManager(int id)
        {
            try
            {
                var result = _managerService.DeleteManager(id);
                if (!result)
                {
                    return NotFound();
                }
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
