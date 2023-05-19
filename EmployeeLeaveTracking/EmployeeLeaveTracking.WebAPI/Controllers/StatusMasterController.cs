using EmployeeLeaveTracking.Data.DTOs;
using EmployeeLeaveTracking.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;

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
       /* [Authorize(Roles = "Manager,Employee")]*/
        public IActionResult GetAll()
        {
            try
            {
                IEnumerable<StatusMasterDTO> statusMasters = _statusMasterService.GetAll();
                return Ok(statusMasters);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("{id}")]
       /* [Authorize(Roles = "Manager,Employee")]*/
        public IActionResult GetById(int id)
        {
            try
            {
                StatusMasterDTO statusMaster = _statusMasterService.GetById(id);
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

                StatusMasterDTO newStatusMaster = _statusMasterService.Create(statusMaster);

                return CreatedAtAction(nameof(GetById), new { id = newStatusMaster.Id }, newStatusMaster);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPut("{id}")]
       /* [Authorize(Roles = "Manager")]*/
        public IActionResult Update(int id, [FromBody] StatusMasterDTO statusMaster)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                StatusMasterDTO updatedStatusMaster = _statusMasterService.Update(id, statusMaster);

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
        /*[Authorize(Roles = "Manager")]*/
        public IActionResult Delete(int id)
        {
            try
            {
                bool deleted = _statusMasterService.Delete(id);

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