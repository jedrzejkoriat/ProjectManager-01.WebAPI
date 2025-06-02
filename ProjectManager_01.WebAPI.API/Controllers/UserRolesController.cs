using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;
using ProjectManager_01.Application.DTOs;

namespace ProjectManager_01.Controllers;

[EnableRateLimiting("fixedlimit")]
[Route("api/[controller]")]
[ApiController]
public class UserRolesController : ControllerBase
{
    private static List<UserRoleDTO> userRoles = new List<UserRoleDTO>
    {
        new UserRoleDTO { UserId = Guid.NewGuid(), RoleId = Guid.NewGuid() },
        new UserRoleDTO { UserId = Guid.NewGuid(), RoleId = Guid.NewGuid() },
    };

    // GET: api/userroles
    /// <summary>
    /// Get all user roles
    /// </summary>
    /// <returns>All user roles</returns>
    [HttpGet]
    public ActionResult<IEnumerable<UserRoleDTO>> GetUserRoles()
    {
        return Ok(userRoles);
    }

    // GET: api/userroles/{userId}
    /// <summary>
    /// Get a user role by user id
    /// </summary>
    /// <param name="userId"></param>
    /// <returns>User role by user id</returns>
    [HttpGet("{userId}")]
    public ActionResult<UserRoleDTO> GetUserRole(Guid userId)
    {
        var userRole = userRoles.FirstOrDefault(ur => ur.UserId == userId);

        if (userRole == null)
            return NotFound();

        return Ok(userRole);
    }

    // POST: api/userroles
    /// <summary>
    /// Create a new user role
    /// </summary>
    /// <param name="userRole"></param>
    /// <returns></returns>
    [HttpPost]
    public ActionResult Post([FromBody] UserRoleDTO userRole)
    {
        userRoles.Add(userRole);

        return CreatedAtAction(nameof(GetUserRole), new { userId = userRole.UserId }, userRole);
    }

    // PUT: api/userroles/{userId}
    /// <summary>
    /// Update an existing user role
    /// </summary>
    /// <param name="userId"></param>
    /// <param name="updatedUserRole"></param>
    /// <returns></returns>
    [HttpPut("{userId}")]
    public ActionResult Put(Guid userId, [FromBody] UserRoleDTO updatedUserRole)
    {
        var userRole = userRoles.FirstOrDefault(ur => ur.UserId == userId);

        if (userRole == null)
            return NotFound();

        userRole.UserId = updatedUserRole.UserId;
        userRole.RoleId = updatedUserRole.RoleId;

        return NoContent();
    }

    // DELETE: api/userroles/{userId}
    /// <summary>
    /// Delete a user role by user id
    /// </summary>
    /// <param name="userId"></param>
    /// <returns></returns>
    [HttpDelete("{userId}")]
    public ActionResult Delete(Guid userId)
    {
        var userRole = userRoles.FirstOrDefault(ur => ur.UserId == userId);

        if (userRole == null)
            return NotFound();

        userRoles.Remove(userRole);

        return NoContent();
    }
}