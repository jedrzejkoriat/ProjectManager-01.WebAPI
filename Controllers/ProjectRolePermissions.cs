using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;
using ProjectManager_01.WebAPI.Data;
using ProjectManager_01.WebAPI.Enums;

namespace ProjectManager_01.WebAPI.Controllers;

[EnableRateLimiting("fixedlimit")]
[Route("api/[controller]")]
[ApiController]
public class ProjectRolePermissionsController : ControllerBase
{
    private static List<ProjectRolePermission> projectRolePermissions = new List<ProjectRolePermission>
    {
        new ProjectRolePermission { ProjectRoleId = Guid.NewGuid(), Permission = Permission.AddTicket },
        new ProjectRolePermission { ProjectRoleId = Guid.NewGuid(), Permission = Permission.Comment },
    };

    // GET: api/projectrolepermissions
    /// <summary>
    /// Get all project role permissions
    /// </summary>
    /// <returns>All project role permissions</returns>
    [HttpGet]
    public ActionResult<IEnumerable<ProjectRolePermission>> GetProjectRolePermissions()
    {
        return Ok(projectRolePermissions);
    }

    // GET: api/projectrolepermissions/{projectRoleId}/{permission}
    /// <summary>
    /// Get a project role permission by project role id and permission
    /// </summary>
    /// <param name="projectRoleId"></param>
    /// <param name="permission"></param>
    /// <returns>Project role permission by its project role id and permission</returns>
    [HttpGet("{projectRoleId}/{permission}")]
    public ActionResult<ProjectRolePermission> GetProjectRolePermission(Guid projectRoleId, Permission permission)
    {
        var permissionEntry = projectRolePermissions.FirstOrDefault(p => p.ProjectRoleId == projectRoleId && p.Permission == permission);

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
    public ActionResult Post([FromBody] ProjectRolePermission projectRolePermission)
    {
        projectRolePermissions.Add(projectRolePermission);

        return CreatedAtAction(nameof(GetProjectRolePermission), new { projectRoleId = projectRolePermission.ProjectRoleId, permission = projectRolePermission.Permission }, projectRolePermission);
    }

    // PUT: api/projectrolepermissions/{projectRoleId}/{permission}
    /// <summary>
    /// Update an existing project role permission
    /// </summary>
    /// <param name="projectRoleId"></param>
    /// <param name="permission"></param>
    /// <param name="updatedPermission"></param>
    /// <returns></returns>
    [HttpPut("{projectRoleId}/{permission}")]
    public ActionResult Put(Guid projectRoleId, Permission permission, [FromBody] ProjectRolePermission updatedPermission)
    {
        var permissionEntry = projectRolePermissions.FirstOrDefault(p => p.ProjectRoleId == projectRoleId && p.Permission == permission);

        if (permissionEntry == null)
            return NotFound();

        permissionEntry.ProjectRoleId = updatedPermission.ProjectRoleId;
        permissionEntry.Permission = updatedPermission.Permission;

        return NoContent();
    }

    // DELETE: api/projectrolepermissions/{projectRoleId}/{permission}
    /// <summary>
    /// Delete a project role permission by project role id and permission
    /// </summary>
    /// <param name="projectRoleId"></param>
    /// <param name="permission"></param>
    /// <returns></returns>
    [HttpDelete("{projectRoleId}/{permission}")]
    public ActionResult Delete(Guid projectRoleId, Permission permission)
    {
        var permissionEntry = projectRolePermissions.FirstOrDefault(p => p.ProjectRoleId == projectRoleId && p.Permission == permission);

        if (permissionEntry == null)
            return NotFound();

        projectRolePermissions.Remove(permissionEntry);

        return NoContent();
    }
}
