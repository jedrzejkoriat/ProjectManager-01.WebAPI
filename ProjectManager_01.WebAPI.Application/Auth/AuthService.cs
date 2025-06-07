using ProjectManager_01.Application.Contracts.Auth;
using ProjectManager_01.Application.Contracts.Repositories;
using ProjectManager_01.Application.DTOs.Users;
using ProjectManager_01.Application.Helpers;

namespace ProjectManager_01.Application.Auth;

internal sealed class AuthService : IAuthService
{
    private readonly IJwtGenerator _jwtGenerator;
    private readonly IUserRepository _userRepository;

    public AuthService(
        IJwtGenerator jwtGenerator,
        IUserRepository userRepository)
    {
        _jwtGenerator = jwtGenerator;
        _userRepository = userRepository;
    }

    public async Task<string> AuthenticateUser(UserLoginDto userLoginDto)
    {
        var user = await _userRepository.GetByUserNameAsync(userLoginDto.UserName);

        if (user == null || !BcryptPasswordHasher.VerifyPassword(userLoginDto.Password, user.PasswordHash))
        {
            throw new UnauthorizedAccessException("Invalid username or password.");
        }

        var userWithClaims = await _userRepository.GetUserClaimsByIdAsync(user.Id);

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

        return _jwtGenerator.GenerateToken(userClaimsDto);
    }
}