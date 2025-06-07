using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;
using ProjectManager_01.Application.Constants;
using ProjectManager_01.Application.Contracts.Services;
using ProjectManager_01.Application.DTOs.ProjectUserRoles;

namespace ProjectManager_01.Controllers;

/// <summary>
/// Controller for managing ProjectUserRoles - Admin authorization.
/// </summary>
[EnableRateLimiting("fixedlimit")]
[Route("api/[controller]")]
[ApiController]
[Authorize(Roles = Roles.Admin)]
public class ProjectUserRolesController : ControllerBase
{
    private readonly IProjectUserRoleService _projectUserRoleService;

    public ProjectUserRolesController(IProjectUserRoleService projectUserRoleService)
    {
        _projectUserRoleService = projectUserRoleService;
    }

    // GET api/projectuserroles
    /// <summary>
    /// Get all ProjectUserRoles - Admin only
    /// </summary>
    /// <returns>All ProjectUserRoles</returns>
    [HttpGet]
    public async Task<ActionResult<IEnumerable<ProjectUserRoleDto>>> GetProjectUserRoles()
    {
        return Ok(await _projectUserRoleService.GetAllProjectUserRolesAsync());
    }

    // GET api/projectuserroles/{id}
    /// <summary>
    /// Get ProjectUserRole by Id - Admin only
    /// </summary>
    /// <param name="id"></param>
    /// <returns>ProjectUserRole by Id</returns>
    [HttpGet("{id}")]
    public async Task<ActionResult<ProjectUserRoleDto>> GetProjectUserRole(Guid id)
    {
        return Ok(await _projectUserRoleService.GetProjectUserRoleByIdAsync(id));
    }

    // POST api/projectuserroles
    /// <summary>
    /// Create ProjectUserRole - Admin only
    /// </summary>
    /// <param name="projectUserRole"></param>
    /// <returns></returns>
    [HttpPost]
    public async Task<ActionResult> CreateProjectUserRole([FromBody] ProjectUserRoleCreateDto projectUserRole)
    {
        await _projectUserRoleService.CreateProjectUserRoleAsync(projectUserRole);
        return Ok();
    }

    // PUT api/projectuserrole
    /// <summary>
    /// Update ProjectUserRole - Admin only
    /// </summary>
    /// <param name="updatedProjectUserRole"></param>
    /// <returns></returns>
    [HttpPut]
    public async Task<ActionResult> UpdateProjectUserRole([FromBody] ProjectUserRoleUpdateDto updatedProjectUserRole)
    {
        await _projectUserRoleService.UpdateProjectUserRoleAsync(updatedProjectUserRole);
        return Ok();
    }

    // DELETE api/projectuserrole/{id}
    /// <summary>
    /// Delete ProjectUserRole by Id - Admin only
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteProjectUserRole(Guid id)
    {
        await _projectUserRoleService.DeleteProjectUserRoleAsync(id);
        return Ok();
    }
}
