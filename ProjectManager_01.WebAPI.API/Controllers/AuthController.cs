using Microsoft.AspNetCore.Mvc;
using ProjectManager_01.Application.Contracts.Auth;
using ProjectManager_01.Application.DTOs.Auth;

namespace ProjectManager_01.WebAPI.Controllers;

/// <summary>
/// Controller for user authentication - no authorization
/// </summary>
[Route("api/[controller]")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;

    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }

    // POST: api/auth/login
    /// <summary>
    /// Authenticate user
    /// </summary>
    /// <param name="userLoginDto"></param>
    /// <returns>JWT token</returns>
    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] UserLoginDto userLoginDto)
    {
        var token = await _authService.AuthenticateUser(userLoginDto);
        return Ok(new { token });
    }

}
