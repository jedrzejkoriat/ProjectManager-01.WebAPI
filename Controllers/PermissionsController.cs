using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;
using ProjectManager_01.WebAPI.Domain.Models;

namespace ProjectManager_01.WebAPI.Controllers;

[EnableRateLimiting("fixedlimit")]
[Route("api/[controller]")]
[ApiController]
public class PermissionsController : ControllerBase
{
    private static List<Permission> permissions = new List<Permission>
    {
        new Permission { Id = Guid.NewGuid(), Name = "CreateTicket" },
        new Permission { Id = Guid.NewGuid(), Name = "EditUser" }
    };

    // GET: api/permissions
    /// <summary>
    /// Get all permissions
    /// </summary>
    /// <returns>All permissions</returns>
    [HttpGet]
    public ActionResult<IEnumerable<Permission>> GetPermissions()
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
    public ActionResult<Permission> GetPermission(Guid id)
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
    public ActionResult Post([FromBody] Permission permission)
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
    public ActionResult Put(Guid id, [FromBody] Permission updatedPermission)
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
