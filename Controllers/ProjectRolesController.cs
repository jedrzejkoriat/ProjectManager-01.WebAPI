using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;
using ProjectManager_01.WebAPI.Data;

namespace ProjectManager_01.WebAPI.Controllers;

[EnableRateLimiting("fixedlimit")]
[Route("api/[controller]")]
[ApiController]
public class ProjectRolesController : ControllerBase
{
    private static List<ProjectRole> projectRoles = new List<ProjectRole>
        {
            new ProjectRole { Id = Guid.NewGuid(), Name = "Admin" },
            new ProjectRole { Id = Guid.NewGuid(), Name = "User" }
        };

    // GET: api/roles
    /// <summary>
    /// Get all project roles
    /// </summary>
    /// <returns>All project roles</returns>
    [HttpGet]
    public ActionResult<IEnumerable<ProjectRole>> GetProjectRoles()
    {
        return Ok(projectRoles);
    }

    // GET: api/roles/{id}
    /// <summary>
    /// Get a project role by ID
    /// </summary>
    /// <param name="id"></param>
    /// <returns>Project role by its id</returns>
    [HttpGet("{id}")]
    public ActionResult<ProjectRole> GetProjectRole(Guid id)
    {
        var role = projectRoles.FirstOrDefault(r => r.Id == id);

        if (role == null) 
            return NotFound();

        return Ok(role);
    }

    // POST: api/roles
    /// <summary>
    /// Create a new role
    /// </summary>
    /// <param name="role"></param>
    /// <returns></returns>
    [HttpPost]
    public ActionResult Post([FromBody] ProjectRole projectRole)
    {
        projectRole.Id = Guid.NewGuid();
        projectRoles.Add(projectRole);

        return CreatedAtAction(nameof(GetProjectRole), new { id = projectRole.Id }, projectRole);
    }

    // PUT: api/roles/{id}
    /// <summary>
    /// Update an existing role
    /// </summary>
    /// <param name="id"></param>
    /// <param name="updatedProjectRole"></param>
    /// <returns></returns>
    [HttpPut("{id}")]
    public ActionResult Put(Guid id, [FromBody] ProjectRole updatedProjectRole)
    {
        var role = projectRoles.FirstOrDefault(r => r.Id == id);

        if (role == null) 
            return NotFound();

        role.Name = updatedProjectRole.Name;

        return NoContent();
    }

    // DELETE: api/roles/{id}
    /// <summary>
    /// Delete a role
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpDelete("{id}")]
    public ActionResult Delete(Guid id)
    {
        var role = projectRoles.FirstOrDefault(r => r.Id == id);

        if (role == null) 
            return NotFound();

        projectRoles.Remove(role);

        return NoContent();
    }
}
