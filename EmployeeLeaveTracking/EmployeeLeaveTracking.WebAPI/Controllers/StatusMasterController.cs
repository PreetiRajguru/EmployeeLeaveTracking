using EmployeeLeaveTracking.Data.DTOs;
using EmployeeLeaveTracking.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

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
        public IActionResult GetAllStatusMasters()
        {
            try
            {
                var statusMasters = _statusMasterService.GetAllStatusMasters();
                return Ok(statusMasters);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("{id}")]
        public IActionResult GetStatusMasterById(int id)
        {
            try
            {
                var statusMaster = _statusMasterService.GetStatusMasterById(id);
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
        public IActionResult AddStatusMaster([FromBody] StatusMasterDTO statusMaster)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var newStatusMaster = _statusMasterService.AddStatusMaster(statusMaster);

                return CreatedAtAction(nameof(GetStatusMasterById), new { id = newStatusMaster.Id }, newStatusMaster);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPut("{id}")]
        public IActionResult UpdateStatusMaster(int id, [FromBody] StatusMasterDTO statusMaster)
        {
            try
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
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteStatusMaster(int id)
        {
            try
            {
                var deleted = _statusMasterService.DeleteStatusMaster(id);

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
