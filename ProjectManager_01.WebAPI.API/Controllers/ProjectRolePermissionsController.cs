using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;
using ProjectManager_01.Application.Contracts.Services;
using ProjectManager_01.Application.DTOs.ProjectRolePermissions;

namespace ProjectManager_01.Controllers;

[EnableRateLimiting("fixedlimit")]
[Route("api/[controller]")]
[ApiController]
public class ProjectRolePermissionsController : ControllerBase
{
    private readonly IProjectRolePermissionService _projectRolePermissionService;

    public ProjectRolePermissionsController(IProjectRolePermissionService projectRolePermissionService)
    {
        _projectRolePermissionService = projectRolePermissionService;
    }

    // GET: api/projectrolepermissions
    /// <summary>
    /// Get all project role permissions
    /// </summary>
    /// <returns>All project role permissions</returns>
    [HttpGet]
    public async Task<ActionResult<IEnumerable<ProjectRolePermissionDto>>> GetProjectRolePermissions()
    {
        return Ok(await _projectRolePermissionService.GetProjectRolePermissionsAsync());
    }

    // GET: api/projectrolepermissions/{projectRoleId}/{permissionId}
    /// <summary>
    /// Get a project role permission by project role id and permission id
    /// </summary>
    /// <param name="projectRoleId"></param>
    /// <param name="permissionId"></param>
    /// <returns>Project role permission by its project role id and permission id</returns>
    [HttpGet("{projectRoleId}/{permissionId}")]
    public async Task<ActionResult<ProjectRolePermissionDto>> GetProjectRolePermission(Guid projectRoleId, Guid permissionId)
    {
        return Ok(await _projectRolePermissionService.GetProjectRolePermissionByIdAsync(projectRoleId, permissionId));
    }

    // POST: api/projectrolepermissions
    /// <summary>
    /// Create a new project role permission
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
    /// Delete a project role permission by project role id and permission id
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
