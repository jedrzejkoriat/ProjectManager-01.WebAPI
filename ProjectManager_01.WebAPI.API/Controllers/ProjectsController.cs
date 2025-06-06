using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;
using ProjectManager_01.Application.Constants;
using ProjectManager_01.Application.Contracts.Services;
using ProjectManager_01.Application.DTOs.Projects;

namespace ProjectManager_01.Controllers;

/// <summary>
/// Controller for managing Projects - Admin or User authorization.
/// </summary>
[EnableRateLimiting("fixedlimit")]
[Route("api/[controller]")]
[ApiController]
[Authorize]
public class ProjectsController : ControllerBase
{
    private readonly IProjectService _projectService;

    public ProjectsController(IProjectService projectService)
    {
        _projectService = projectService;
    }

    // GET: api/projects
    /// <summary>
    /// Get all Projects - Admin only
    /// </summary>
    /// <returns>All Projects</returns>
    [HttpGet]
    [Authorize(Roles = Roles.Admin)]
    public async Task<ActionResult<IEnumerable<ProjectDto>>> GetProjects()
    {
        return Ok(await _projectService.GetAllProjectsAsync());
    }

    // GET api/projects
    /// <summary>
    /// Get Project by Id - User with ReadProject permission and matching Project access
    /// </summary>
    /// <param name="id"></param>
    /// <returns>Project by Id</returns>
    [HttpGet("{id}")]
    [Authorize(Policy = Permissions.ReadProject)]
    public async Task<ActionResult<ProjectDto>> GetProject(Guid id)
    {
        return Ok(await _projectService.GetProjectByIdAsync(id));
    }

    // GET api/projects/users/{userId}
    /// <summary>
    /// Get Projects by UserId - User with ReadProject permission and matching Project access
    /// </summary>
    /// <param name="userId"></param>
    /// <returns>List of user ProjectDto</returns>
    [HttpGet("users/{userId}")]
    [Authorize(Policy = Permissions.ReadProject)]
    public async Task<ActionResult<IEnumerable<ProjectDto>>> GetProjectsByUserId(Guid userId)
    {
        var projects = await _projectService.GetProjectsByUserIdAsync(userId);
        return Ok(projects);
    }

    // POST api/projects
    /// <summary>
    /// Create Project - Admin only
    /// </summary>
    /// <param name="project"></param>
    /// <returns></returns>
    [HttpPost]
    [Authorize(Roles = Roles.Admin)]
    public async Task<ActionResult> CreateProject([FromBody] ProjectCreateDto project)
    {
        await _projectService.CreateProjectAsync(project);
        return Ok();
    }

    // PUT api/projects
    /// <summary>
    /// Update Project - Admin only
    /// </summary>
    /// <param name="updatedProject"></param>
    /// <returns></returns>
    [HttpPut]
    [Authorize(Roles = Roles.Admin)]
    public async Task<ActionResult> UpdateProject([FromBody] ProjectUpdateDto updatedProject)
    {
        await _projectService.UpdateProjectAsync(updatedProject);
        return Ok();
    }

    // DELETE api/projects
    /// <summary>
    /// Delete Project by Id - Admin only (DELETE is denied on db side)
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpDelete("{id}")]
    [Authorize(Roles = Roles.Admin)]
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
    [Authorize(Roles = Roles.Admin)]
    public async Task<ActionResult> SoftDeleteProject(Guid id)
    {
        await _projectService.SoftDeleteProjectAsync(id);
        return Ok();
    }
}