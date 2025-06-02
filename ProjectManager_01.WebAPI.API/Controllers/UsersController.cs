using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;
using ProjectManager_01.Application.Contracts.Services;
using ProjectManager_01.Application.DTOs;
using ProjectManager_01.Application.DTOs.Users;

namespace ProjectManager_01.Controllers;

[EnableRateLimiting("fixedlimit")]
[Route("api/[controller]")]
[ApiController]
public class UsersController : ControllerBase
{
    private readonly IUserService userService;

    public UsersController(IUserService userService)
    {
        this.userService = userService;
    }

    // GET: api/users
    /// <summary>
    /// Get all users
    /// </summary>
    /// <returns>All urses</returns>
    [HttpGet]
    public async Task<ActionResult<IEnumerable<UserDto>>> GetUsers()
    {
        try
        {
            return Ok(await userService.GetAllUsersAsync());
        }
        catch (Exception ex)
        {
            return Problem(ex.Message);
        }
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
        try
        {
            return Ok(await userService.GetUserByIdAsync(id));
        }
        catch (Exception ex)
        {
            return Problem(ex.Message);
        }
    }

    // POST: api/users
    /// <summary>
    /// Create a new user
    /// </summary>
    /// <param name="user"></param>
    /// <returns></returns>
    [HttpPost]
    public async Task<ActionResult> Post([FromBody] UserCreateDto user)
    {
        try
        {
            await userService.CreateUserAsync(user);
            return Ok();
        }
        catch (Exception ex)
        {
            return Problem(ex.Message);
        }
    }

    // PUT: api/users/{id}
    /// <summary>
    /// Update an existing user
    /// </summary>
    /// <param name="updatedUser"></param>
    /// <returns></returns>
    [HttpPut]
    public async Task<ActionResult> Put([FromBody] UserUpdateDto updatedUser)
    {
        try
        {
            await userService.UpdateUserAsync(updatedUser);
            return Ok();
        }
        catch (Exception ex)
        {
            return Problem(ex.Message);
        }
    }

    // DELETE: api/users/{id}
    /// <summary>
    /// Delete a user
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(Guid id)
    {
        try
        {
            await userService.DeleteUserAsync(id);
            return Ok();
        }
        catch (Exception ex)
        {
            return Problem(ex.Message);
        }
    }
}