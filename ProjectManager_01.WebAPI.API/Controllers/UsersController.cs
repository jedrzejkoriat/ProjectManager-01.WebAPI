using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;
using ProjectManager_01.Application.Constants;
using ProjectManager_01.Application.Contracts.Services;
using ProjectManager_01.Application.DTOs.Users;

namespace ProjectManager_01.Controllers;

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
    /// Get all users
    /// </summary>
    /// <returns>All urses</returns>
    [HttpGet]
    public async Task<ActionResult<IEnumerable<UserDto>>> GetUsers()
    {
        return Ok(await _userService.GetAllUsersAsync());
    }

    // GET: api/users/{id}
    /// <summary>
    /// Get a user by ID
    /// </summary>
    /// <param name="id"></param>
    /// <returns>User by its id</returns>
    [HttpGet("{id}")]
    public async Task<ActionResult<UserDto>> GetUser(Guid id)
    {
        return Ok(await _userService.GetUserByIdAsync(id));
    }

    // GET: api/users/project/{projectId}
    /// <summary>
    /// Get users by project ID
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
    /// Create a new user
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
    /// Update an existing user
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
    /// Delete a user
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
    /// Soft delete a user
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