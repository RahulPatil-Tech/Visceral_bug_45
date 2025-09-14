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
    public class TaskController : ControllerBase
    {
        private readonly TaskService _taskService;
        private readonly ILogger<TaskController> _logger;

        public TaskController(TaskService taskService, ILogger<TaskController> logger)
        {
            _taskService = taskService;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var tasks = await _taskService.GetAllAsync();
                return Ok(tasks);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving all tasks");
                return StatusCode(500, "An error occurred while retrieving tasks");
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var task = await _taskService.GetByIdAsync(id);
                if (task == null)
                    return NotFound();
                return Ok(task);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving task with ID: {TaskId}", id);
                return StatusCode(500, "An error occurred while retrieving the task");
            }
        }

        [HttpGet("project/{projectId}")]
        public async Task<IActionResult> GetByProjectId(int projectId)
        {
            try
            {
                var tasks = await _taskService.GetByProjectIdAsync(projectId);
                return Ok(tasks);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving tasks for project ID: {ProjectId}", projectId);
                return StatusCode(500, "An error occurred while retrieving tasks for the project");
            }
        }

        [HttpGet("assigned/{assignedToId}")]
        public async Task<IActionResult> GetByAssignedToId(int assignedToId)
        {
            try
            {
                var tasks = await _taskService.GetByAssignedToIdAsync(assignedToId);
                return Ok(tasks);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving tasks assigned to user ID: {UserId}", assignedToId);
                return StatusCode(500, "An error occurred while retrieving assigned tasks");
            }
        }

        [HttpPost]
        [Authorize(Roles = "Developer,Admin")]
        public async Task<IActionResult> Create([FromBody] WorkTask task)
        {
            try
            {
                await _taskService.AddAsync(task);
                _logger.LogInformation("Task created successfully: {TaskTitle}", task.Title);
                return CreatedAtAction(nameof(GetById), new { id = task.Id }, task);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating task: {TaskTitle}", task.Title);
                return StatusCode(500, "An error occurred while creating the task");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] WorkTask task)
        {
            try
            {
                if (id != task.Id)
                    return BadRequest();
                await _taskService.UpdateAsync(task);
                _logger.LogInformation("Task updated successfully: {TaskId}", id);
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating task with ID: {TaskId}", id);
                return StatusCode(500, "An error occurred while updating the task");
            }
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _taskService.DeleteAsync(id);
                _logger.LogInformation("Task deleted successfully: {TaskId}", id);
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting task with ID: {TaskId}", id);
                return StatusCode(500, "An error occurred while deleting the task");
            }
        }
    }
}
