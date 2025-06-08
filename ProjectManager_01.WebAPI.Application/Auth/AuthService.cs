using Microsoft.Extensions.Logging;
using ProjectManager_01.Application.Contracts.Auth;
using ProjectManager_01.Application.Contracts.Repositories;
using ProjectManager_01.Application.DTOs.Users;
using ProjectManager_01.Application.Helpers;

namespace ProjectManager_01.Application.Auth;

internal sealed class AuthService : IAuthService
{
    private readonly IJwtGenerator _jwtGenerator;
    private readonly IUserRepository _userRepository;
    private readonly ILogger<AuthService> _logger;

    public AuthService(
        IJwtGenerator jwtGenerator,
        IUserRepository userRepository,
        ILogger<AuthService> logger)
    {
        _jwtGenerator = jwtGenerator;
        _userRepository = userRepository;
        _logger = logger;
    }

    public async Task<string> AuthenticateUser(UserLoginDto userLoginDto)
    {
        var user = await _userRepository.GetByUserNameAsync(userLoginDto.UserName);

        if (user == null || !BcryptPasswordHasher.VerifyPassword(userLoginDto.Password, user.PasswordHash))
        {
            _logger.LogWarning("User authentication failed: {UserName}.", userLoginDto.UserName);
            throw new UnauthorizedAccessException("Invalid username or password.");
        }

        var userWithClaims = await _userRepository.GetByIdWithRolesAndPermissionsAsync(user.Id);

        var projectPermissions = new List<string>();

        foreach (var projectRole in userWithClaims.ProjectRoles)
        {
            var currentProjectId = projectRole.ProjectId;

            foreach (var permission in projectRole.Permissions)
            {
                projectPermissions.Add($"{currentProjectId}:{permission.Name}");
            }
        }

        var userClaimsDto = new UserClaimsDto(userWithClaims.Id, userWithClaims.Role.Name, projectPermissions);


        _logger.LogInformation("User authenticated successfully: {UserName}.", userLoginDto.UserName);
        return _jwtGenerator.GenerateToken(userClaimsDto);
    }
}