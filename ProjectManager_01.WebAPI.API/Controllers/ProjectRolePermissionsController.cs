using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;
using ProjectManager_01.Application.Contracts.Services;
using ProjectManager_01.Application.DTOs;
using ProjectManager_01.Application.DTOs.ProjectRolePermissions;

namespace ProjectManager_01.Controllers;

[EnableRateLimiting("fixedlimit")]
[Route("api/[controller]")]
[ApiController]
public class ProjectRolePermissionsController : ControllerBase
{

    private readonly IProjectRolePermissionService projectRolePermissionService;

    public ProjectRolePermissionsController(IProjectRolePermissionService projectRolePermissionService)
    {
        this.projectRolePermissionService = projectRolePermissionService;
    }

    // GET: api/projectrolepermissions
    /// <summary>
    /// Get all project role permissions
    /// </summary>
    /// <returns>All project role permissions</returns>
    [HttpGet]
    public async Task<ActionResult<IEnumerable<ProjectRolePermissionDto>>> GetProjectRolePermissions()
    {
        try
        {
            return Ok(await projectRolePermissionService.GetProjectRolePermissionsAsync());
        }
        catch (Exception ex)
        {
            return Problem(ex.Message);
        }
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
        try
        {
            return Ok(await projectRolePermissionService.GetProjectRolePermissionByIdAsync(projectRoleId, permissionId));
        }
        catch (Exception ex)
        {
            return Problem(ex.Message);
        }
    }

    // POST: api/projectrolepermissions
    /// <summary>
    /// Create a new project role permission
    /// </summary>
    /// <param name="projectRolePermission"></param>
    /// <returns></returns>
    [HttpPost]
    public async Task<ActionResult> Post([FromBody] ProjectRolePermissionCreateDto projectRolePermission)
    {
        try
        {
            await projectRolePermissionService.CreateProjectRolePermissionAsync(projectRolePermission);
            return Ok();
        }
        catch (Exception ex)
        {
            return Problem(ex.Message);
        }
    }

    // DELETE: api/projectrolepermissions/{projectRoleId}/{permissionId}
    /// <summary>
    /// Delete a project role permission by project role id and permission id
    /// </summary>
    /// <param name="projectRoleId"></param>
    /// <param name="permissionId"></param>
    /// <returns></returns>
    [HttpDelete("{projectRoleId}/{permissionId}")]
    public async Task<ActionResult> Delete(Guid projectRoleId, Guid permissionId)
    {
        try
        {
            await projectRolePermissionService.DeleteProjectRolePermissionAsync(projectRoleId, permissionId);
            return Ok();
        }
        catch (Exception ex)
        {
            return Problem(ex.Message);
        }
    }
}
