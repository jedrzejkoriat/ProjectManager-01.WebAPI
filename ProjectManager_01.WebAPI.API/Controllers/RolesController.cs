using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;
using ProjectManager_01.Application.DTOs;

namespace ProjectManager_01.Controllers;

[EnableRateLimiting("fixedlimit")]
[Route("api/[controller]")]
[ApiController]
public class RolesController : ControllerBase
{
    private static List<RoleDTO> roles = new List<RoleDTO>
    {
        new RoleDTO { Id = Guid.NewGuid(), Name = "Admin" },
        new RoleDTO { Id = Guid.NewGuid(), Name = "User" }
    };

    // GET: api/roles
    /// <summary>
    /// Get all roles
    /// </summary>
    /// <returns>All roles</returns>
    [HttpGet]
    public ActionResult<IEnumerable<RoleDTO>> GetRoles()
    {
        return Ok(roles);
    }

    // GET api/roles/{id}
    /// <summary>
    /// Get a role by ID
    /// </summary>
    /// <param name="id"></param>
    /// <returns>Role by its id</returns>
    [HttpGet("{id}")]
    public ActionResult<RoleDTO> GetRole(Guid id)
    {
        var role = roles.FirstOrDefault(r => r.Id == id);

        if (role == null)
            return NotFound();

        return Ok(role);
    }

    // POST api/roles
    /// <summary>
    /// Create a new role
    /// </summary>
    /// <param name="role"></param>
    /// <returns></returns>
    [HttpPost]
    public ActionResult Post([FromBody] RoleDTO role)
    {
        role.Id = Guid.NewGuid();
        roles.Add(role);

        return CreatedAtAction(nameof(GetRole), new { id = role.Id }, role);
    }

    // PUT api/roles/{id}
    /// <summary>
    /// Update an existing role
    /// </summary>
    /// <param name="id"></param>
    /// <param name="updatedRole"></param>
    /// <returns></returns>
    [HttpPut("{id}")]
    public ActionResult Put(Guid id, [FromBody] RoleDTO updatedRole)
    {
        var role = roles.FirstOrDefault(r => r.Id == id);

        if (role == null)
            return NotFound();

        role.Name = updatedRole.Name;

        return NoContent();
    }

    // DELETE api/roles/{id}
    /// <summary>
    /// Delete a role
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpDelete("{id}")]
    public ActionResult Delete(Guid id)
    {
        var role = roles.FirstOrDefault(r => r.Id == id);

        if (role == null)
            return NotFound();

        roles.Remove(role);

        return NoContent();
    }
}
