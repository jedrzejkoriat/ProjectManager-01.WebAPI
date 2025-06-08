using AutoMapper;
using Microsoft.Extensions.Logging;
using ProjectManager_01.Application.Contracts.Auth;
using ProjectManager_01.Application.Contracts.Repositories;
using ProjectManager_01.Application.Contracts.Services;
using ProjectManager_01.Application.DTOs.Auth;
using ProjectManager_01.Application.DTOs.Users;
using ProjectManager_01.Application.Exceptions;
using ProjectManager_01.Application.Helpers;
using ProjectManager_01.Domain.Models;

namespace ProjectManager_01.Application.Auth;

internal sealed class AuthService : IAuthService
{
    private readonly IJwtGenerator _jwtGenerator;
    private readonly IUserRepository _userRepository;
    private readonly ILogger<AuthService> _logger;
    private readonly IUserService _userService;
    private readonly IMapper _mapper;

    public AuthService(
        IJwtGenerator jwtGenerator,
        IUserRepository userRepository,
        ILogger<AuthService> logger,
        IUserService userService,
        IMapper mapper)
    {
        _jwtGenerator = jwtGenerator;
        _userRepository = userRepository;
        _logger = logger;
        _userService = userService;
        _mapper = mapper;
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

    public async Task RegisterUser(UserRegisterDto userRegisterDto)
    {
        _logger.LogInformation("User registration called: {Email}.", userRegisterDto.Email);

        var userCreateDto = _mapper.Map<UserCreateDto>(userRegisterDto);

        try
        {
            await _userService.CreateUserAsync(userCreateDto);
        }
        catch (Exception ex)
        {
            _logger.LogError("User registration failed: {Email}.", userRegisterDto.Email);
            throw new OperationFailedException("User registration failed");
        }

        _logger.LogInformation("User registered successfully: {Email}.", userRegisterDto.Email);
    }
}