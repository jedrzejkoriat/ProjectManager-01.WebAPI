using Microsoft.AspNetCore.Mvc;
using ProjectManager_01.Application.Contracts.Authorization;
using ProjectManager_01.Application.DTOs.Users;

namespace ProjectManager_01.WebAPI.Controllers;

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
    /// Authenticate a user and return a JWT token
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
