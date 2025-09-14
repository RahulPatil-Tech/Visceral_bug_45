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
    public class ProjectController : ControllerBase
    {
        private readonly ProjectService _projectService;
        private readonly ILogger<ProjectController> _logger;

        public ProjectController(ProjectService projectService, ILogger<ProjectController> logger)
        {
            _projectService = projectService;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var projects = await _projectService.GetAllAsync();
                return Ok(projects);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving all projects");
                return StatusCode(500, "An error occurred while retrieving projects");
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var project = await _projectService.GetByIdAsync(id);
                if (project == null)
                    return NotFound();
                return Ok(project);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving project with ID: {ProjectId}", id);
                return StatusCode(500, "An error occurred while retrieving the project");
            }
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create([FromBody] Project project)
        {
            try
            {
                await _projectService.AddAsync(project);
                _logger.LogInformation("Project created successfully: {ProjectName}", project.Name);
                return CreatedAtAction(nameof(GetById), new { id = project.Id }, project);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating project: {ProjectName}", project.Name);
                return StatusCode(500, "An error occurred while creating the project");
            }
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Update(int id, [FromBody] Project project)
        {
            try
            {
                if (id != project.Id)
                    return BadRequest();
                await _projectService.UpdateAsync(project);
                _logger.LogInformation("Project updated successfully: {ProjectId}", id);
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating project with ID: {ProjectId}", id);
                return StatusCode(500, "An error occurred while updating the project");
            }
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _projectService.DeleteAsync(id);
                _logger.LogInformation("Project deleted successfully: {ProjectId}", id);
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting project with ID: {ProjectId}", id);
                return StatusCode(500, "An error occurred while deleting the project");
            }
        }

        [HttpPut("{id}/assign")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> AssignUser(int id, [FromQuery] int userId, [FromQuery] string role)
        {
            try
            {
                await _projectService.AssignUserAsync(id, userId, role);
                _logger.LogInformation("User {UserId} assigned to project {ProjectId} with role {Role}", userId, id, role);
                return Ok("User assigned successfully");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error assigning user {UserId} to project {ProjectId}", userId, id);
                return StatusCode(500, "An error occurred while assigning user to project");
            }
        }

        [HttpPut("{id}/unassign")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UnassignUser(int id, [FromQuery] int userId, [FromQuery] string role)
        {
            try
            {
                await _projectService.UnassignUserAsync(id, userId, role);
                _logger.LogInformation("User {UserId} unassigned from project {ProjectId} with role {Role}", userId, id, role);
                return Ok("User unassigned successfully");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error unassigning user {UserId} from project {ProjectId}", userId, id);
                return StatusCode(500, "An error occurred while unassigning user from project");
            }
        }
    }
}
