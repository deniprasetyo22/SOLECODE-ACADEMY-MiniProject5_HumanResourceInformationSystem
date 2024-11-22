using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MiniProject5.Application.DTOs;
using MiniProject5.Application.Interfaces.IServices;
using MiniProject5.Persistence.Models;

namespace MiniProject5.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectController : ControllerBase
    {
        private readonly IProjectService _projectService;

        public ProjectController(IProjectService projectService)
        {
            _projectService = projectService;
        }

        [HttpGet]
        public async Task<ActionResult<object>> GetAllProjects([FromQuery] QueryObjectProject query)
        {
            try
            {
                var projects = await _projectService.GetAllProjectsAsync(query);
                return Ok(projects);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while fetching projects.");
            }
        }

        [HttpGet("NoPages")]
        public async Task<ActionResult<IEnumerable<Project>>> GetAllProjectsNoPages()
        {
            try
            {
                var projects = await _projectService.GetAllProjectsNoPagesAsync();
                return Ok(projects);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while fetching projects.");
            }
        }

        [HttpGet("{projId}")]
        public async Task<ActionResult<Project>> GetProject(int projId)
        {
            try
            {
                var project = await _projectService.GetProjectByIdAsync(projId);
                return Ok(project);
            }
            catch (KeyNotFoundException)
            {
                return NotFound($"Project with ID {projId} not found.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while fetching the project.");
            }
        }

        [HttpPost]
        public async Task<ActionResult<Project>> AddProject([FromBody] Project project)
        {
            try
            {
                var newProject = await _projectService.AddProjectAsync(project);
                return Ok("Project added successfully.");
            }
            catch (InvalidOperationException ex)
            {
                return Conflict(ex.Message); // Conflict if project name already exists
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while adding the project.");
            }
        }

        [HttpPut("{projId}")]
        public async Task<IActionResult> UpdateProject(int projId, [FromBody] Project project)
        {
            try
            {
                await _projectService.UpdateProjectAsync(projId, project);
                return Ok("Project updated successfilly.");
            }
            catch (KeyNotFoundException)
            {
                return NotFound($"Project with ID {projId} not found.");
            }
            catch (InvalidOperationException ex)
            {
                return Conflict(ex.Message); // Conflict if project name already exists
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while updating the project.");
            }
        }

        [HttpDelete("{projId}")]
        public async Task<IActionResult> DeleteProject(int projId)
        {
            try
            {
                await _projectService.DeleteProjectAsync(projId);
                return Ok("Project deleted successfully.");
            }
            catch (KeyNotFoundException)
            {
                return NotFound($"Project with ID {projId} not found.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while deleting the project.");
            }
        }
    }
}
