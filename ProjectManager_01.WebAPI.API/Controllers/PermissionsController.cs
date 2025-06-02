using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;
using ProjectManager_01.Application.DTOs;

namespace ProjectManager_01.Controllers;

[EnableRateLimiting("fixedlimit")]
[Route("api/[controller]")]
[ApiController]
public class PermissionsController : ControllerBase
{
    private static List<PermissionDTO> permissions = new List<PermissionDTO>
    {
        new PermissionDTO { Id = Guid.NewGuid(), Name = "CreateTicket" },
        new PermissionDTO { Id = Guid.NewGuid(), Name = "EditUser" }
    };

    // GET: api/permissions
    /// <summary>
    /// Get all permissions
    /// </summary>
    /// <returns>All permissions</returns>
    [HttpGet]
    public ActionResult<IEnumerable<PermissionDTO>> GetPermissions()
    {
        return Ok(permissions);
    }

    // GET api/permissions/{id}
    /// <summary>
    /// Get a permission by ID
    /// </summary>
    /// <param name="id"></param>
    /// <returns>Permission by its id</returns>
    [HttpGet("{id}")]
    public ActionResult<PermissionDTO> GetPermission(Guid id)
    {
        var permission = permissions.FirstOrDefault(p => p.Id == id);

        if (permission == null)
            return NotFound();

        return Ok(permission);
    }

    // POST api/permissions
    /// <summary>
    /// Create a new permission
    /// </summary>
    /// <param name="permission"></param>
    /// <returns></returns>
    [HttpPost]
    public ActionResult Post([FromBody] PermissionDTO permission)
    {
        permission.Id = Guid.NewGuid();
        permissions.Add(permission);

        return CreatedAtAction(nameof(GetPermission), new { id = permission.Id }, permission);
    }

    // PUT api/permissions/{id}
    /// <summary>
    /// Update an existing permission
    /// </summary>
    /// <param name="id"></param>
    /// <param name="updatedPermission"></param>
    /// <returns></returns>
    [HttpPut("{id}")]
    public ActionResult Put(Guid id, [FromBody] PermissionDTO updatedPermission)
    {
        var permission = permissions.FirstOrDefault(p => p.Id == id);

        if (permission == null)
            return NotFound();

        permission.Name = updatedPermission.Name;

        return NoContent();
    }

    // DELETE api/permissions/{id}
    /// <summary>
    /// Delete a permission
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpDelete("{id}")]
    public ActionResult Delete(Guid id)
    {
        var permission = permissions.FirstOrDefault(p => p.Id == id);

        if (permission == null)
            return NotFound();

        permissions.Remove(permission);

        return NoContent();
    }
}
