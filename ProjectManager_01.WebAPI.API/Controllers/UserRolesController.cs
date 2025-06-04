using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;
using ProjectManager_01.Application.Contracts.Services;
using ProjectManager_01.Application.DTOs.UserRoles;

namespace ProjectManager_01.Controllers;

[EnableRateLimiting("fixedlimit")]
[Route("api/[controller]")]
[ApiController]
public class UserRolesController : ControllerBase
{
    private readonly IUserRoleService _userRoleService;

    public UserRolesController(IUserRoleService userRoleService)
    {
        _userRoleService = userRoleService;
    }

    // GET: api/userroles
    /// <summary>
    /// Get all user roles
    /// </summary>
    /// <returns>All user roles</returns>
    [HttpGet]
    public ActionResult<IEnumerable<UserRoleDto>> GetUserRoles()
    {
        return Ok(_userRoleService.GetAllUserRolesAsync());
    }

    // GET: api/userroles/{userId}
    /// <summary>
    /// Get a user role by user id
    /// </summary>
    /// <param name="userId"></param>
    /// <returns>User role by user id</returns>
    [HttpGet("{userId}")]
    public async Task<ActionResult<UserRoleDto>> GetUserRole(Guid userId)
    {
        return Ok(await _userRoleService.GetUserRoleByUserIdAsync(userId));
    }

    // POST: api/userroles
    /// <summary>
    /// Create a new user role
    /// </summary>
    /// <param name="userRole"></param>
    /// <returns></returns>
    [HttpPost]
    public async Task<ActionResult> CreateUserRole([FromBody] UserRoleCreateDto userRole)
    {
        await _userRoleService.CreateUserRoleAsync(userRole);
        return Ok();
    }

    // PUT: api/userroles/{userId}
    /// <summary>
    /// Update an existing user role
    /// </summary>
    /// <param name="updatedUserRole"></param>
    /// <returns></returns>
    [HttpPut]
    public async Task<ActionResult> UpdateUserRole([FromBody] UserRoleUpdateDto updatedUserRole)
    {
        await _userRoleService.UpdateUserRoleAsync(updatedUserRole);
        return Ok();
    }

    // DELETE: api/userroles/{userId}
    /// <summary>
    /// Delete a user role by user id
    /// </summary>
    /// <param name="userId"></param>
    /// <returns></returns>
    [HttpDelete("{userId}")]
    public async Task<ActionResult> DeleteUserRole(Guid userId)
    {
        await _userRoleService.DeleteUserRoleAsync(userId);
        return Ok();
    }
}