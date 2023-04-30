using EmployeeLeaveTracking.Data.DTOs;
using EmployeeLeaveTracking.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeLeaveTracking.WebAPI.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class StatusMasterController : ControllerBase
    {
        private readonly IStatusMaster _statusMasterService;

        public StatusMasterController(IStatusMaster statusMasterService)
        {
            _statusMasterService = statusMasterService;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            try
            {
                var statusMasters = _statusMasterService.GetAll();
                return Ok(statusMasters);
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
                var statusMaster = _statusMasterService.GetById(id);
                if (statusMaster == null)
                {
                    return NotFound();
                }
                return Ok(statusMaster);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost]
        public IActionResult Create([FromBody] StatusMasterDTO statusMaster)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var newStatusMaster = _statusMasterService.Create(statusMaster);

                return CreatedAtAction(nameof(GetById), new { id = newStatusMaster.Id }, newStatusMaster);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] StatusMasterDTO statusMaster)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var updatedStatusMaster = _statusMasterService.Update(id, statusMaster);

                if (updatedStatusMaster == null)
                {
                    return NotFound();
                }

                return Ok(updatedStatusMaster);
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
                var deleted = _statusMasterService.Delete(id);

                if (!deleted)
                {
                    return NotFound();
                }

                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
