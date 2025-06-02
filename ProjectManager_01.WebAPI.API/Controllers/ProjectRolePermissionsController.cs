using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;
using ProjectManager_01.Application.DTOs;

namespace ProjectManager_01.Controllers;

[EnableRateLimiting("fixedlimit")]
[Route("api/[controller]")]
[ApiController]
public class ProjectRolePermissionsController : ControllerBase
{
    private static List<ProjectRolePermissionDTO> projectRolePermissions = new List<ProjectRolePermissionDTO>
    {
        new ProjectRolePermissionDTO { ProjectRoleId = Guid.NewGuid(), PermissionId = Guid.NewGuid() },
        new ProjectRolePermissionDTO { ProjectRoleId = Guid.NewGuid(), PermissionId = Guid.NewGuid() },
    };

    // GET: api/projectrolepermissions
    /// <summary>
    /// Get all project role permissions
    /// </summary>
    /// <returns>All project role permissions</returns>
    [HttpGet]
    public ActionResult<IEnumerable<ProjectRolePermissionDTO>> GetProjectRolePermissions()
    {
        return Ok(projectRolePermissions);
    }

    // GET: api/projectrolepermissions/{projectRoleId}/{permissionId}
    /// <summary>
    /// Get a project role permission by project role id and permission id
    /// </summary>
    /// <param name="projectRoleId"></param>
    /// <param name="permissionId"></param>
    /// <returns>Project role permission by its project role id and permission id</returns>
    [HttpGet("{projectRoleId}/{permissionId}")]
    public ActionResult<ProjectRolePermissionDTO> GetProjectRolePermission(Guid projectRoleId, Guid permissionId)
    {
        var permissionEntry = projectRolePermissions.FirstOrDefault(p => p.ProjectRoleId == projectRoleId && p.PermissionId == permissionId);

        if (permissionEntry == null)
            return NotFound();

        return Ok(permissionEntry);
    }

    // POST: api/projectrolepermissions
    /// <summary>
    /// Create a new project role permission
    /// </summary>
    /// <param name="projectRolePermission"></param>
    /// <returns></returns>
    [HttpPost]
    public ActionResult Post([FromBody] ProjectRolePermissionDTO projectRolePermission)
    {
        projectRolePermissions.Add(projectRolePermission);

        return CreatedAtAction(nameof(GetProjectRolePermission), new { projectRoleId = projectRolePermission.ProjectRoleId, permissionId = projectRolePermission.PermissionId }, projectRolePermission);
    }

    // PUT: api/projectrolepermissions/{projectRoleId}/{permissionId}
    /// <summary>
    /// Update an existing project role permission
    /// </summary>
    /// <param name="projectRoleId"></param>
    /// <param name="permissionId"></param>
    /// <param name="updatedPermission"></param>
    /// <returns></returns>
    [HttpPut("{projectRoleId}/{permissionId}")]
    public ActionResult Put(Guid projectRoleId, Guid permissionId, [FromBody] ProjectRolePermissionDTO updatedPermission)
    {
        var permissionEntry = projectRolePermissions.FirstOrDefault(p => p.ProjectRoleId == projectRoleId && p.PermissionId == permissionId);

        if (permissionEntry == null)
            return NotFound();

        permissionEntry.ProjectRoleId = updatedPermission.ProjectRoleId;
        permissionEntry.PermissionId = updatedPermission.PermissionId;

        return NoContent();
    }

    // DELETE: api/projectrolepermissions/{projectRoleId}/{permissionId}
    /// <summary>
    /// Delete a project role permission by project role id and permission id
    /// </summary>
    /// <param name="projectRoleId"></param>
    /// <param name="permissionId"></param>
    /// <returns></returns>
    [HttpDelete("{projectRoleId}/{permissionId}")]
    public ActionResult Delete(Guid projectRoleId, Guid permissionId)
    {
        var permissionEntry = projectRolePermissions.FirstOrDefault(p => p.ProjectRoleId == projectRoleId && p.PermissionId == permissionId);

        if (permissionEntry == null)
            return NotFound();

        projectRolePermissions.Remove(permissionEntry);

        return NoContent();
    }
}
