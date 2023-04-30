using EmployeeLeaveTracking.Data.DTOs;
using EmployeeLeaveTracking.Services.Interfaces;
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
        public IActionResult GetAll()
        {
            try
            {
                var managers = _managerService.GetAll();
                return Ok(managers);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            try
            {
                var manager = _managerService.GetById(id);
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
        public IActionResult Create([FromBody] ManagerDTO manager)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest();
                }
                var createdManager = _managerService.Create(manager);
                return CreatedAtAction(nameof(GetById), new { id = createdManager.Id }, createdManager);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] ManagerDTO manager)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest();
                }
                manager.Id = id;
                var updatedManager = _managerService.Update(manager);
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
        public IActionResult Delete(int id)
        {
            try
            {
                var result = _managerService.Delete(id);
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
