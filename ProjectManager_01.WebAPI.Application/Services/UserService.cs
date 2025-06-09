using System.Data;
using AutoMapper;
using Microsoft.Extensions.Logging;
using ProjectManager_01.Application.Contracts.Repositories;
using ProjectManager_01.Application.Contracts.Services;
using ProjectManager_01.Application.DTOs.UserRoles;
using ProjectManager_01.Application.DTOs.Users;
using ProjectManager_01.Application.Exceptions;
using ProjectManager_01.Application.Helpers;
using ProjectManager_01.Domain.Models;

namespace ProjectManager_01.Application.Services;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;
    private readonly IUserRoleService _userRoleService;
    private readonly IProjectUserRoleService _projectUserRoleService;
    private readonly IDbConnection _dbConnection;
    private readonly ICommentService _commentService;
    private readonly ITicketService _ticketService;
    private readonly ILogger<UserService> _logger;

    public UserService(
        IUserRepository userRepository,
        IMapper mapper,
        IUserRoleService userRoleService,
        IProjectUserRoleService projectUserRoleService,
        IDbConnection dbConnection,
        ICommentService commentService,
        ITicketService ticketService,
        ILogger<UserService> logger)
    {
        _userRepository = userRepository;
        _mapper = mapper;
        _userRoleService = userRoleService;
        _projectUserRoleService = projectUserRoleService;
        _dbConnection = dbConnection;
        _commentService = commentService;
        _ticketService = ticketService;
        _logger = logger;
    }

    public async Task CreateUserAsync(UserCreateDto userCreateDto)
    {
        _logger.LogWarning("Creating User called. User: {UserName}", userCreateDto.UserName);

        using var transaction = DbTransactionHelper.BeginTransaction(_dbConnection);

        try
        {
            var user = _mapper.Map<User>(userCreateDto);
            user.PasswordHash = BcryptPasswordHasher.HashPassword(userCreateDto.Password);
            user.Id = Guid.NewGuid();
            user.CreatedAt = DateTimeOffset.UtcNow;

            // Check if operation is successful
            if (!await _userRepository.CreateAsync(user, transaction))
            {
                _logger.LogError("Creating User failed. User: {UserName}", userCreateDto.UserName);
                throw new OperationFailedException("Creating User failed.");
            }

            await _userRoleService.CreateDefaultUserRoleAsync(user.Id, transaction);

            transaction.Commit();

            _logger.LogInformation("Creating User successful. User: {UserId}", user.Id);
        }
        catch
        {
            transaction.Rollback();
            _logger.LogError("Creating User transaction failed. User: {UserName}", userCreateDto.UserName);
            throw;
        }
    }

    public async Task UpdateUserAsync(UserUpdateDto userUpdateDto)
    {
        _logger.LogInformation("Updating User called. User: {UserId}", userUpdateDto.Id);

        var user = _mapper.Map<User>(userUpdateDto);

        // Check if operation is successful
        if (!await _userRepository.UpdateAsync(user))
        {
            _logger.LogError("Updating User failed. User: {UserId}", user.Id);
            throw new OperationFailedException("Updating User failed.");
        }

        _logger.LogInformation("Updating User successful. User: {UserId}", user.Id);
    }

    public async Task DeleteUserAsync(Guid userId)
    {
        _logger.LogWarning("Deleting User transaction called. User: {UserId}", userId);

        using var transaction = DbTransactionHelper.BeginTransaction(_dbConnection);

        try
        {
            // Check if operation is successful
            if (!await _userRepository.DeleteByIdAsync(userId, transaction))
            {
                _logger.LogError("Deleting User failed. User: {UserId}", userId);
                throw new OperationFailedException("Deleting User transaction failed.");
            }

            await _userRoleService.DeleteByUserIdAsync(userId, transaction);
            await _projectUserRoleService.DeleteByUserIdAsync(userId, transaction);
            await _commentService.DeleteByUserIdAsync(userId, transaction);
            await _ticketService.ClearUserAssignmentAsync(userId, transaction);
            await _ticketService.DeleteTicketByUserIdAsync(userId, transaction);

            transaction.Commit();
            _logger.LogInformation("Deleting User transaction successful. User: {UserId}", userId);
        }
        catch
        {
            transaction.Rollback();
            _logger.LogError("Deleting User transaction failed. User: {UserId}", userId);
            throw;
        }
    }

    public async Task<UserDto> GetUserByIdAsync(Guid userId)
    {
        _logger.LogInformation("Getting User called. User: {UserId}", userId);

        var user = await _userRepository.GetByIdAsync(userId);

        // Check if operation is successful
        if (user == null)
        {
            _logger.LogError("Getting User failed. User: {UserId}", userId);
            throw new NotFoundException("User not found.");
        }

        _logger.LogInformation("Getting User successful. User: {UserId}", userId);
        return _mapper.Map<UserDto>(user);
    }

    public async Task<IEnumerable<UserDto>> GetAllUsersAsync()
    {
        _logger.LogInformation("Getting all Users called.");

        var users = await _userRepository.GetAllAsync();

        _logger.LogInformation("Getting all Users ({Count}) successful.", users.Count());
        return _mapper.Map<IEnumerable<UserDto>>(users);
    }

    public async Task SoftDeleteUserAsync(Guid userId)
    {
        _logger.LogWarning("Soft deleting User called. User: {UserId}", userId);

        // Check if operation is successful
        if (!await _userRepository.SoftDeleteByIdAsync(userId))
        {
            _logger.LogError("Soft deleting User failed. User: {UserId}", userId);
            throw new OperationFailedException("Soft deleting User failed.");
        }

        _logger.LogInformation("Soft deleting User successful. User: {UserId}", userId);
    }

    public async Task<IEnumerable<UserDto>> GetUsersByProjectIdAsync(Guid projectId)
    {
        _logger.LogInformation("Getting Users by ProjectId called. Project: {ProjectId}", projectId);

        var users = await _userRepository.GetAllByProjectIdAsync(projectId);

        _logger.LogInformation("Getting Users by ProjectId ({Count}) successful. Project: {ProjectId}", users.Count(), projectId);
        return _mapper.Map<IEnumerable<UserDto>>(users);
    }
}
