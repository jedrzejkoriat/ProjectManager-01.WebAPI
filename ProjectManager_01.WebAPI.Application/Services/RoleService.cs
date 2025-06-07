using System.Data;
using AutoMapper;
using Microsoft.Extensions.Logging;
using ProjectManager_01.Application.Contracts.Repositories;
using ProjectManager_01.Application.Contracts.Services;
using ProjectManager_01.Application.DTOs.Roles;
using ProjectManager_01.Application.Exceptions;
using ProjectManager_01.Application.Helpers;
using ProjectManager_01.Domain.Models;

namespace ProjectManager_01.Application.Services;

public class RoleService : IRoleService
{
    private readonly IRoleRepository _roleRepository;
    private readonly IMapper _mapper;
    private readonly IDbConnection _dbConnection;
    private readonly IUserRoleService _userRoleService;
    private readonly ILogger<RoleService> _logger;

    public RoleService(
        IRoleRepository roleRepository,
        IMapper mapper,
        IDbConnection dbConnection,
        IUserRoleService userRoleService,
        ILogger<RoleService> logger)
    {
        _roleRepository = roleRepository;
        _mapper = mapper;
        _dbConnection = dbConnection;
        _userRoleService = userRoleService;
        _logger = logger;
    }

    public async Task CreateRoleAsync(RoleCreateDto roleCreateDto)
    {
        _logger.LogInformation("Creating Role called. Role: {RoleName}", roleCreateDto.Name);

        var role = _mapper.Map<Role>(roleCreateDto);
        role.Id = Guid.NewGuid();

        // Check if operation is successful
        if (!await _roleRepository.CreateAsync(role))
        {
            _logger.LogError("Creating Role failed. Role: {RoleName}", role.Name);
            throw new OperationFailedException("Creating Role failed.");
        }

        _logger.LogInformation("Creating Role successful. RoleId: {RoleId}", role.Id);
    }

    public async Task UpdateRoleAsync(RoleUpdateDto roleUpdateDto)
    {
        _logger.LogInformation("Updating Role called. RoleId: {RoleId}", roleUpdateDto.Id);

        var role = _mapper.Map<Role>(roleUpdateDto);

        // Check if operation is successful
        if (!await _roleRepository.UpdateAsync(role))
        {
            _logger.LogError("Updating Role failed. RoleId: {RoleId}", role.Id);
            throw new OperationFailedException("Updating Role failed.");
        }

        _logger.LogInformation("Updating Role successful. RoleId: {RoleId}", role.Id);
    }

    public async Task DeleteRoleAsync(Guid roleId)
    {
        _logger.LogWarning("Deleting Role transaction called. RoleId: {RoleId}", roleId);

        using var transaction = DbTransactionHelper.BeginTransaction(_dbConnection);

        try
        {
            // Check if operation is successful
            if (!await _roleRepository.DeleteByIdAsync(roleId, transaction))
            {
                _logger.LogError("Deleting Role failed. RoleId: {RoleId}", roleId);
                throw new OperationFailedException("Deleting Role transaction failed.");
            }

            await _userRoleService.DeleteByRoleIdAsync(roleId, transaction);

            transaction.Commit();
            _logger.LogInformation("Deleting Role transaction successful. RoleId: {RoleId}", roleId);
        }
        catch
        {
            transaction.Rollback();
            _logger.LogError("Deleting Role transaction failed. RoleId: {RoleId}", roleId);
            throw;
        }
    }

    public async Task<RoleDto> GetRoleByIdAsync(Guid roleId)
    {
        _logger.LogInformation("Getting Role called. RoleId: {RoleId}", roleId);

        var role = await _roleRepository.GetByIdAsync(roleId);

        // Check if operation is successful
        if (role == null)
        {
            _logger.LogError("Getting Role failed. RoleId: {RoleId}", roleId);
            throw new NotFoundException("Role not found.");
        }

        _logger.LogInformation("Getting Role successful. RoleId: {RoleId}", roleId);
        return _mapper.Map<RoleDto>(role);
    }

    public async Task<IEnumerable<RoleDto>> GetAllRolesAsync()
    {
        _logger.LogInformation("Getting Roles called.");

        var roles = await _roleRepository.GetAllAsync();

        _logger.LogInformation("Getting Roles successful. Count: {Count}", roles.Count());
        return _mapper.Map<IEnumerable<RoleDto>>(roles);
    }
}
