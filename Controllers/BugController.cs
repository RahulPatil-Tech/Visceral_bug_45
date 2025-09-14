using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Viseralbug.Models;
using Viseralbug.Services;
using System.Threading.Tasks;

namespace Viseralbug.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class BugController : ControllerBase
    {
        private readonly BugService _bugService;
        private readonly ILogger<BugController> _logger;

        public BugController(BugService bugService, ILogger<BugController> logger)
        {
            _bugService = bugService;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var bugs = await _bugService.GetAllAsync();
                return Ok(bugs);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving all bugs");
                return StatusCode(500, "An error occurred while retrieving bugs");
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var bug = await _bugService.GetByIdAsync(id);
                if (bug == null)
                    return NotFound();
                return Ok(bug);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving bug with ID: {BugId}", id);
                return StatusCode(500, "An error occurred while retrieving the bug");
            }
        }

        [HttpGet("project/{projectId}")]
        public async Task<IActionResult> GetByProjectId(int projectId)
        {
            try
            {
                var bugs = await _bugService.GetByProjectIdAsync(projectId);
                return Ok(bugs);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving bugs for project ID: {ProjectId}", projectId);
                return StatusCode(500, "An error occurred while retrieving bugs for the project");
            }
        }

        [HttpGet("assigned/{assignedToId}")]
        public async Task<IActionResult> GetByAssignedToId(int assignedToId)
        {
            try
            {
                var bugs = await _bugService.GetByAssignedToIdAsync(assignedToId);
                return Ok(bugs);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving bugs assigned to user ID: {UserId}", assignedToId);
                return StatusCode(500, "An error occurred while retrieving assigned bugs");
            }
        }

        [HttpPost]
        [Authorize(Roles = "Tester,Admin")]
        public async Task<IActionResult> Create([FromBody] Bug bug)
        {
            try
            {
                await _bugService.AddAsync(bug);
                _logger.LogInformation("Bug created successfully: {BugTitle}", bug.Title);
                return CreatedAtAction(nameof(GetById), new { id = bug.Id }, bug);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating bug: {BugTitle}", bug.Title);
                return StatusCode(500, "An error occurred while creating the bug");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] Bug bug)
        {
            try
            {
                if (id != bug.Id)
                    return BadRequest();
                await _bugService.UpdateAsync(bug);
                _logger.LogInformation("Bug updated successfully: {BugId}", id);
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating bug with ID: {BugId}", id);
                return StatusCode(500, "An error occurred while updating the bug");
            }
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _bugService.DeleteAsync(id);
                _logger.LogInformation("Bug deleted successfully: {BugId}", id);
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting bug with ID: {BugId}", id);
                return StatusCode(500, "An error occurred while deleting the bug");
            }
        }
    }
}
