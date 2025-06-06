using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;
using ProjectManager_01.Application.Contracts.Services;
using ProjectManager_01.Application.DTOs.ProjectRoles;

namespace ProjectManager_01.Controllers;

[EnableRateLimiting("fixedlimit")]
[Route("api/[controller]")]
[ApiController]
public class ProjectRolesController : ControllerBase
{
    private readonly IProjectRoleService _projectRoleService;

    public ProjectRolesController(IProjectRoleService projectRoleService)
    {
        _projectRoleService = projectRoleService;
    }

    // GET: api/roles
    /// <summary>
    /// Get all project roles
    /// </summary>
    /// <returns>All project roles</returns>
    [HttpGet]
    public async Task<ActionResult<IEnumerable<ProjectRoleDto>>> GetProjectRoles()
    {
        return Ok(await _projectRoleService.GetAllProjectRolesAsync());
    }

    // GET: api/roles/{id}
    /// <summary>
    /// Get a project role by ID
    /// </summary>
    /// <param name="id"></param>
    /// <returns>Project role by its id</returns>
    [HttpGet("{id}")]
    public async Task<ActionResult<ProjectRoleDto>> GetProjectRole(Guid id)
    {
        return Ok(await _projectRoleService.GetProjectRoleByIdAsync(id));
    }

    // POST: api/roles
    /// <summary>
    /// Create a new role
    /// </summary>
    /// <param name="projectRole"></param>
    /// <returns></returns>
    [HttpPost]
    public async Task<ActionResult> CreateProjectRole([FromBody] ProjectRoleCreateDto projectRole)
    {
        await _projectRoleService.CreateProjectRoleAsync(projectRole);
        return Ok();
    }

    // PUT: api/roles
    /// <summary>
    /// Update an existing role
    /// </summary>
    /// <param name="updatedProjectRole"></param>
    /// <returns></returns>
    [HttpPut]
    public async Task<ActionResult> UpdateProjectRole([FromBody] ProjectRoleUpdateDto updatedProjectRole)
    {
        await _projectRoleService.UpdateProjectRoleAsync(updatedProjectRole);
        return Ok();
    }

    // DELETE: api/roles/{id}
    /// <summary>
    /// Delete a role
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteProjectRole(Guid id)
    {
        await _projectRoleService.DeleteProjectRoleAsync(id);
        return Ok();
    }
}
