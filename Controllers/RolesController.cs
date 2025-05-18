using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProjectManager_01.WebAPI.Data;

namespace ProjectManager_01.WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class RolesController : ControllerBase
{
    private static List<Role> roles = new List<Role>
        {
            new Role { Id = 1, Name = "Admin" },
            new Role { Id = 2, Name = "User" }
        };

    // GET: api/roles
    /// <summary>
    /// Get all roles
    /// </summary>
    /// <returns>All roles</returns>
    [HttpGet]
    public ActionResult<IEnumerable<Role>> GetRoles()
    {
        return Ok(roles);
    }

    // GET: api/roles/{id}
    /// <summary>
    /// Get a role by ID
    /// </summary>
    /// <param name="id"></param>
    /// <returns>Role by its id</returns>
    [HttpGet("{id}")]
    public ActionResult<Role> GetRole(int id)
    {
        var role = roles.FirstOrDefault(r => r.Id == id);
        if (role == null) return NotFound();
        return Ok(role);
    }

    // POST: api/roles
    /// <summary>
    /// Create a new role
    /// </summary>
    /// <param name="role"></param>
    /// <returns></returns>
    [HttpPost]
    public ActionResult Post([FromBody] Role role)
    {
        role.Id = roles.Max(r => r.Id) + 1;
        roles.Add(role);
        return CreatedAtAction(nameof(GetRole), new { id = role.Id }, role);
    }

    // PUT: api/roles/{id}
    /// <summary>
    /// Update an existing role
    /// </summary>
    /// <param name="id"></param>
    /// <param name="updatedRole"></param>
    /// <returns></returns>
    [HttpPut("{id}")]
    public ActionResult Put(int id, [FromBody] Role updatedRole)
    {
        var role = roles.FirstOrDefault(r => r.Id == id);
        if (role == null) return NotFound();

        role.Name = updatedRole.Name;
        return NoContent();
    }

    // DELETE: api/roles/{id}
    /// <summary>
    /// Delete a role
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpDelete("{id}")]
    public ActionResult Delete(int id)
    {
        var role = roles.FirstOrDefault(r => r.Id == id);
        if (role == null) return NotFound();

        roles.Remove(role);
        return NoContent();
    }
}
