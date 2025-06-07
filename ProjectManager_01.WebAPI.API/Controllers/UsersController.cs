using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;
using ProjectManager_01.Application.Constants;
using ProjectManager_01.Application.Contracts.Services;
using ProjectManager_01.Application.DTOs.Users;

namespace ProjectManager_01.Controllers;

/// <summary>
/// Controller for managing Users - Admin authorization.
/// </summary>
[EnableRateLimiting("fixedlimit")]
[Route("api/[controller]")]
[ApiController]
[Authorize(Roles = Roles.Admin)]
public class UsersController : ControllerBase
{
    private readonly IUserService _userService;

    public UsersController(IUserService userService)
    {
        _userService = userService;
    }

    // GET: api/users
    /// <summary>
    /// Get all Users - Admin only
    /// </summary>
    /// <returns>All users</returns>
    [HttpGet]
    public async Task<ActionResult<IEnumerable<UserDto>>> GetUsers()
    {
        return Ok(await _userService.GetAllUsersAsync());
    }

    // GET: api/users/{id}
    /// <summary>
    /// Get User by Id - Admin only
    /// </summary>
    /// <param name="id"></param>
    /// <returns>User by Id</returns>
    [HttpGet("{id}")]
    public async Task<ActionResult<UserDto>> GetUser(Guid id)
    {
        return Ok(await _userService.GetUserByIdAsync(id));
    }

    // GET: api/users/project/{projectId}
    /// <summary>
    /// Get Users by ProjectId - Admin only
    /// </summary>
    /// <param name="projectId"></param>
    /// <returns></returns>
    [HttpGet("project/{projectId}")]
    public async Task<ActionResult<IEnumerable<UserDto>>> GetUsersByProjectId(Guid projectId)
    {
        return Ok(await _userService.GetUsersByProjectIdAsync(projectId));
    }

    // POST: api/users
    /// <summary>
    /// Create User - Admin only
    /// </summary>
    /// <param name="user"></param>
    /// <returns></returns>
    [HttpPost]
    public async Task<ActionResult> CreateUser([FromBody] UserCreateDto user)
    {
        await _userService.CreateUserAsync(user);
        return Ok();
    }

    // PUT: api/users/{id}
    /// <summary>
    /// Update User - Admin only
    /// </summary>
    /// <param name="updatedUser"></param>
    /// <returns></returns>
    [HttpPut]
    public async Task<ActionResult> UpdateUser([FromBody] UserUpdateDto updatedUser)
    {
        await _userService.UpdateUserAsync(updatedUser);
        return Ok();
    }

    // DELETE: api/users/{id}
    /// <summary>
    /// Delete User - Admin only (DELETE is denied on db side)
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteUser(Guid id)
    {
        await _userService.DeleteUserAsync(id);
        return Ok();
    }

    // PATCH api/users/{id}/soft-delete
    /// <summary>
    /// Soft-delete User - Admin only
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpPatch("{id}/soft-delete")]
    public async Task<ActionResult> SoftDeleteUser(Guid id)
    {
        await _userService.SoftDeleteUserAsync(id);
        return Ok();
    }
}