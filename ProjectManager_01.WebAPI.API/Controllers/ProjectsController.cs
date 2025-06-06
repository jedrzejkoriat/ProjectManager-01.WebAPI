using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;
using ProjectManager_01.Application.Contracts.Services;
using ProjectManager_01.Application.DTOs.Projects;

namespace ProjectManager_01.Controllers;

[EnableRateLimiting("fixedlimit")]
[Route("api/[controller]")]
[ApiController]
public class ProjectsController : ControllerBase
{
    private readonly IProjectService _projectService;

    public ProjectsController(IProjectService projectService)
    {
        _projectService = projectService;
    }


    // GET: api/projects
    /// <summary>
    /// Get all projects
    /// </summary>
    /// <returns>All projects</returns>
    [HttpGet]
    public async Task<ActionResult<IEnumerable<ProjectDto>>> GetProjects()
    {
        return Ok(await _projectService.GetAllProjectsAsync());
    }

    // GET api/projects
    /// <summary>
    /// Get a project by ID
    /// </summary>
    /// <param name="id"></param>
    /// <returns>Project by its id</returns>
    [HttpGet("{id}")]
    public async Task<ActionResult<ProjectDto>> GetProject(Guid id)
    {
        return Ok(await _projectService.GetProjectByIdAsync(id));
    }

    // POST api/projects
    /// <summary>
    /// Create a new project
    /// </summary>
    /// <param name="project"></param>
    /// <returns></returns>
    [HttpPost]
    public async Task<ActionResult> CreateProject([FromBody] ProjectCreateDto project)
    {
        await _projectService.CreateProjectAsync(project);
        return Ok();
    }

    // PUT api/projects
    /// <summary>
    /// Update an existing project
    /// </summary>
    /// <param name="updatedProject"></param>
    /// <returns></returns>
    [HttpPut]
    public async Task<ActionResult> UpdateProject([FromBody] ProjectUpdateDto updatedProject)
    {
        await _projectService.UpdateProjectAsync(updatedProject);
        return Ok();
    }

    // DELETE api/projects
    /// <summary>
    /// Delete a project
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteProject(Guid id)
    {
        await _projectService.DeleteProjectAsync(id);
        return Ok();
    }

    // PATCH api/projects/{id}/soft-delete
    /// <summary>
    /// Soft delete a project
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpPatch("{id}/soft-delete")]
    public async Task<ActionResult> SoftDeleteProject(Guid id)
    {
        await _projectService.SoftDeleteProjectAsync(id);
        return Ok();
    }
}