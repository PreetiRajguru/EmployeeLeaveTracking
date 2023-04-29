using EmployeeLeaveTracking.Data.DTOs;
using EmployeeLeaveTracking.Services.Interfaces;
using EmployeeLeaveTracking.Services.Services;
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
        public IEnumerable<StatusMasterDTO> GetAllStatusMasters()
        {
            return _statusMasterService.GetAllStatusMasters();
        }

        [HttpGet("{id}")]
        public IActionResult GetStatusMasterById(int id)
        {
            var statusMaster = _statusMasterService.GetStatusMasterById(id);

            if (statusMaster == null)
            {
                return NotFound();
            }

            return Ok(statusMaster);
        }

        [HttpPost]
        public IActionResult AddStatusMaster(StatusMasterDTO statusMaster)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var newStatusMaster = _statusMasterService.AddStatusMaster(statusMaster);

            return CreatedAtAction(nameof(GetStatusMasterById), new { id = newStatusMaster.Id }, newStatusMaster);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateStatusMaster(int id, StatusMasterDTO statusMaster)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var updatedStatusMaster = _statusMasterService.UpdateStatusMaster(id, statusMaster);

            if (updatedStatusMaster == null)
            {
                return NotFound();
            }

            return Ok(updatedStatusMaster);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteStatusMaster(int id)
        {
            var deleted = _statusMasterService.DeleteStatusMaster(id);

            if (!deleted)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}
