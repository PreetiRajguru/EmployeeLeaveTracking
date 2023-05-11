using EmployeeLeaveTracking.Data.DTOs;
using EmployeeLeaveTracking.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeLeaveTracking.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DesignationMasterController : ControllerBase
    {
        private readonly IDesignationMaster _designationMasterService;

        public DesignationMasterController(IDesignationMaster designationMasterService)
        {
            _designationMasterService = designationMasterService;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            try
            {
                IEnumerable<DesignationMasterDTO> designationMasters = _designationMasterService.GetAll();
                return Ok(designationMasters);
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
                DesignationMasterDTO designationMaster = _designationMasterService.GetById(id);
                if (designationMaster == null)
                {
                    return NotFound();
                }
                return Ok(designationMaster);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost]
        public IActionResult Create([FromBody] DesignationMasterDTO designationMaster)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                DesignationMasterDTO newDesignationMaster = _designationMasterService.Create(designationMaster);

                return CreatedAtAction(nameof(GetById), new { id = newDesignationMaster.Id }, newDesignationMaster);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] DesignationMasterDTO designationMaster)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                DesignationMasterDTO updatedDesignationMaster = _designationMasterService.Update(id, designationMaster);

                if (updatedDesignationMaster == null)
                {
                    return NotFound();
                }

                return Ok(updatedDesignationMaster);
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
                bool deleted = _designationMasterService.Delete(id);

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