using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;
using ProjectManager_01.Application.Constants;
using ProjectManager_01.Application.Contracts.Services;
using ProjectManager_01.Application.DTOs.ProjectRolePermissions;

namespace ProjectManager_01.Controllers;

/// <summary>
/// Controller for managing ProjectRolePermissions - Admin authorization.
/// </summary>
[EnableRateLimiting("fixedlimit")]
[Route("api/[controller]")]
[ApiController]
[Authorize(Roles = Roles.Admin)]
public class ProjectRolePermissionsController : ControllerBase
{
    private readonly IProjectRolePermissionService _projectRolePermissionService;

    public ProjectRolePermissionsController(IProjectRolePermissionService projectRolePermissionService)
    {
        _projectRolePermissionService = projectRolePermissionService;
    }

    // GET: api/projectrolepermissions
    /// <summary>
    /// Get all ProjectRolePermissions - Admin only
    /// </summary>
    /// <returns>All ProjectRolePermissions</returns>
    [HttpGet]
    public async Task<ActionResult<IEnumerable<ProjectRolePermissionDto>>> GetProjectRolePermissions()
    {
        return Ok(await _projectRolePermissionService.GetProjectRolePermissionsAsync());
    }

    // GET: api/projectrolepermissions/{projectRoleId}/{permissionId}
    /// <summary>
    /// Get ProjectRolePermission by ProjectRole Id and Permission Id - Admin only
    /// </summary>
    /// <param name="projectRoleId"></param>
    /// <param name="permissionId"></param>
    /// <returns>ProjectRolePermission by ProjectRoleId and PermissionId</returns>
    [HttpGet("{projectRoleId}/{permissionId}")]
    public async Task<ActionResult<ProjectRolePermissionDto>> GetProjectRolePermission(Guid projectRoleId, Guid permissionId)
    {
        return Ok(await _projectRolePermissionService.GetProjectRolePermissionByIdAsync(projectRoleId, permissionId));
    }

    // POST: api/projectrolepermissions
    /// <summary>
    /// Create ProjectRolePermission - Admin only
    /// </summary>
    /// <param name="projectRolePermission"></param>
    /// <returns></returns>
    [HttpPost]
    public async Task<ActionResult> CreateProjectRolePermission([FromBody] ProjectRolePermissionCreateDto projectRolePermission)
    {
        await _projectRolePermissionService.CreateProjectRolePermissionAsync(projectRolePermission);
        return Ok();
    }

    // DELETE: api/projectrolepermissions/{projectRoleId}/{permissionId}
    /// <summary>
    /// Delete ProjectRolePermission by ProjectRole Id and Permission Id - Admin only
    /// </summary>
    /// <param name="projectRoleId"></param>
    /// <param name="permissionId"></param>
    /// <returns></returns>
    [HttpDelete("{projectRoleId}/{permissionId}")]
    public async Task<ActionResult> DeleteProjectRolePermission(Guid projectRoleId, Guid permissionId)
    {
        await _projectRolePermissionService.DeleteProjectRolePermissionAsync(projectRoleId, permissionId);
        return Ok();
    }
}
