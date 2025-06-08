using System.Data;
using AutoMapper;
using Microsoft.Extensions.Logging;
using ProjectManager_01.Application.Contracts.Repositories;
using ProjectManager_01.Application.Contracts.Services;
using ProjectManager_01.Application.DTOs.UserRoles;
using ProjectManager_01.Application.Exceptions;
using ProjectManager_01.Domain.Models;

namespace ProjectManager_01.Application.Services;

public class UserRoleService : IUserRoleService
{
    private readonly IUserRoleRepository _userRoleRepository;
    private readonly IMapper _mapper;
    private readonly ILogger<UserRoleService> _logger;

    public UserRoleService(
        IUserRoleRepository userRoleRepository,
        IMapper mapper,
        ILogger<UserRoleService> logger)
    {
        _userRoleRepository = userRoleRepository;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task CreateUserRoleAsync(UserRoleCreateDto userRoleCreateDto)
    {
        _logger.LogInformation("Creating UserRole called. UserId: {UserId}, RoleId {RoleId}", userRoleCreateDto.UserId, userRoleCreateDto.RoleId);

        var userRole = _mapper.Map<UserRole>(userRoleCreateDto);

        // Check if operation is successful
        if (!await _userRoleRepository.CreateAsync(userRole))
        {
            _logger.LogError("Creating UserRole failed. UserId: UserId: {UserId}, RoleId {RoleId}", userRoleCreateDto.UserId, userRoleCreateDto.RoleId);
            throw new OperationFailedException("Creating UserRole failed.");
        }

        _logger.LogInformation("Creating UserRole successful. UserId: UserId: {UserId}, RoleId {RoleId}", userRoleCreateDto.UserId, userRoleCreateDto.RoleId);
    }

    public async Task CreateUserRoleAsync(UserRoleCreateDto userRoleCreateDto, IDbTransaction transaction)
    {
        _logger.LogInformation("Creating UserRole transaction called. UserId: {UserId}, RoleId {RoleId}", userRoleCreateDto.UserId, userRoleCreateDto.RoleId);

        var userRole = _mapper.Map<UserRole>(userRoleCreateDto);

        // Check if operation is successful
        if (!await _userRoleRepository.CreateAsync(userRole, transaction))
        {
            _logger.LogError("Creating UserRole transaction failed. UserId: {UserId}, RoleId {RoleId}", userRoleCreateDto.UserId, userRoleCreateDto.RoleId);
            throw new OperationFailedException("Creating UserRole transaction failed.");
        }

        _logger.LogInformation("Creating UserRole transaction successful. UserId: {UserId}, RoleId {RoleId}", userRoleCreateDto.UserId, userRoleCreateDto.RoleId);
    }

    public async Task UpdateUserRoleAsync(UserRoleUpdateDto userRoleUpdateDto)
    {
        _logger.LogInformation("Updating UserRole called. UserId: {UserId}", userRoleUpdateDto.UserId);

        var userRole = _mapper.Map<UserRole>(userRoleUpdateDto);

        // Check if operation is successful
        if (!await _userRoleRepository.UpdateAsync(userRole))
        {
            _logger.LogError("Updating UserRole failed. UserId: {UserId}", userRoleUpdateDto.UserId);
            throw new OperationFailedException("Updating UserRole failed.");
        }

        _logger.LogInformation("Updating UserRole successful. UserId: {UserId}", userRoleUpdateDto.UserId);
    }

    public async Task DeleteUserRoleAsync(Guid userId)
    {
        _logger.LogWarning("Deleting UserRole called. UserId: {UserId}", userId);

        // Check if operation is successful
        if (!await _userRoleRepository.DeleteByIdAsync(userId))
        {
            _logger.LogError("Deleting UserRole failed. UserId: {UserId}", userId);
            throw new OperationFailedException("Deleting UserRole failed.");
        }

        _logger.LogInformation("Deleting UserRole successful. UserId: {UserId}", userId);
    }

    public async Task<UserRoleDto> GetUserRoleByUserIdAsync(Guid userId)
    {
        _logger.LogInformation("Getting UserRole called. UserId: {UserId}", userId);

        var userRole = await _userRoleRepository.GetByUserIdAsync(userId);

        // Check if operation is successful
        if (userRole == null)
        {
            _logger.LogError("Getting UserRole failed. UserId: {UserId}", userId);
            throw new NotFoundException("UserRole not found.");
        }

        _logger.LogInformation("Getting UserRole successful. UserId: {UserId}", userId);
        return _mapper.Map<UserRoleDto>(userRole);
    }

    public async Task<IEnumerable<UserRoleDto>> GetAllUserRolesAsync()
    {
        _logger.LogInformation("Getting all UserRoles called.");

        var userRoles = await _userRoleRepository.GetAllAsync();

        _logger.LogInformation("Getting all UserRoles successful. Count: {Count}", userRoles.Count());
        return _mapper.Map<IEnumerable<UserRoleDto>>(userRoles);
    }

    public async Task DeleteByRoleIdAsync(Guid roleId, IDbTransaction transaction)
    {
        _logger.LogWarning("Deleting UserRoles by RoleId transaction called. RoleId: {RoleId}", roleId);

        // Check if operation is successful
        if (!await _userRoleRepository.DeleteAllByRoleIdAsync(roleId, transaction))
        {
            _logger.LogError("No UserRoles found related to RoleId: {RoleId}", roleId);
            return;
        }

        _logger.LogInformation("Deleting UserRoles by RoleId transaction successful. RoleId: {RoleId}", roleId);
    }

    public async Task DeleteByUserIdAsync(Guid userId, IDbTransaction transaction)
    {
        _logger.LogWarning("Deleting UserRoles by UserId transaction called. UserId: {UserId}", userId);

        // Check if operation is successful
        if (!await _userRoleRepository.DeleteAllByUserIdAsync(userId, transaction))
        {
            _logger.LogError("No UserRoles found related to UserId: {UserId}", userId);
            return;
        }

        _logger.LogInformation("Deleting UserRoles by UserId transaction successful. UserId: {UserId}", userId);
    }

    public async Task CreateDefaultUserRoleAsync(Guid userId, IDbTransaction transaction)
    {
        _logger.LogInformation("Creating default UserRole called. UserId: {UserId}", userId);

        // Check if operation is successful
        if (!await _userRoleRepository.CreateDefaultAsync(userId, transaction))
        {
            _logger.LogError("Creating default UserRole failed. UserId: {UserId}", userId);
            throw new OperationFailedException("Creating default UserRole failed.");
        }

        _logger.LogInformation("Creating default UserRole successful. UserId: {UserId}", userId);
    }
}
