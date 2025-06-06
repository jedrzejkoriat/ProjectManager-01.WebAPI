using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;
using ProjectManager_01.Application.Constants;
using ProjectManager_01.Application.Contracts.Services;
using ProjectManager_01.Application.DTOs.UserRoles;

namespace ProjectManager_01.Controllers;

/// <summary>
/// Controller for managing UserRoles - Admin authorization.
/// </summary>
[EnableRateLimiting("fixedlimit")]
[Route("api/[controller]")]
[ApiController]
[Authorize(Roles = Roles.Admin)]
public class UserRolesController : ControllerBase
{
    private readonly IUserRoleService _userRoleService;

    public UserRolesController(IUserRoleService userRoleService)
    {
        _userRoleService = userRoleService;
    }

    // GET: api/userroles
    /// <summary>
    /// Get all UserRoles - Admin only
    /// </summary>
    /// <returns>All UserRoles</returns>
    [HttpGet]
    public ActionResult<IEnumerable<UserRoleDto>> GetUserRoles()
    {
        return Ok(_userRoleService.GetAllUserRolesAsync());
    }

    // GET: api/userroles/{userId}
    /// <summary>
    /// Get UserRole by Id - Admin only
    /// </summary>
    /// <param name="userId"></param>
    /// <returns>UserRole by UserId</returns>
    [HttpGet("{userId}")]
    public async Task<ActionResult<UserRoleDto>> GetUserRole(Guid userId)
    {
        return Ok(await _userRoleService.GetUserRoleByUserIdAsync(userId));
    }

    // POST: api/userroles
    /// <summary>
    /// Create UserRole - Admin only
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
    /// Update UserRole - Admin only
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
    /// Delete UserRole by UserId - Admin only
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