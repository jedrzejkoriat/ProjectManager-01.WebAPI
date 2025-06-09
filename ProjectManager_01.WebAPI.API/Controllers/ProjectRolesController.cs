using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;
using ProjectManager_01.Application.Constants;
using ProjectManager_01.Application.Contracts.Services;
using ProjectManager_01.Application.DTOs.ProjectRoles;

namespace ProjectManager_01.Controllers;

/// <summary>
/// Controller for managing ProjectRoles - Admin authorization.
/// </summary>
[EnableRateLimiting("fixedlimit")]
[Route("api/[controller]")]
[ApiController]
[Authorize(Roles = Roles.Admin)]
public class ProjectRolesController : ControllerBase
{
    private readonly IProjectRoleService _projectRoleService;

    public ProjectRolesController(IProjectRoleService projectRoleService)
    {
        _projectRoleService = projectRoleService;
    }

    // GET: api/roles
    /// <summary>
    /// Get all ProjectRoles - Admin only
    /// </summary>
    /// <returns>All ProjectRoles</returns>
    [HttpGet]
    public async Task<ActionResult<IEnumerable<ProjectRoleDto>>> GetProjectRoles()
    {
        return Ok(await _projectRoleService.GetAllProjectRolesAsync());
    }

    // GET: api/roles/{id}
    /// <summary>
    /// Get ProjectRole by Id - Admin only
    /// </summary>
    /// <param name="id"></param>
    /// <returns>ProjectRole by Id</returns>
    [HttpGet("{id}")]
    public async Task<ActionResult<ProjectRoleDto>> GetProjectRole(Guid id)
    {
        return Ok(await _projectRoleService.GetProjectRoleByIdAsync(id));
    }

    // POST: api/roles
    /// <summary>
    /// Create ProjectRole - Admin only
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
    /// Update ProjectRole - Admin only
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
    /// Delete ProjectRole by Id - Admin only
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
