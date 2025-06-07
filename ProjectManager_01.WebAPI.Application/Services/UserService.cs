using System.Data;
using AutoMapper;
using ProjectManager_01.Application.Contracts.Repositories;
using ProjectManager_01.Application.Contracts.Services;
using ProjectManager_01.Application.DTOs.UserRoles;
using ProjectManager_01.Application.DTOs.Users;
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

    public UserService(
        IUserRepository userRepository,
        IMapper mapper,
        IUserRoleService userRoleService,
        IProjectUserRoleService projectUserRoleService,
        IDbConnection dbConnection,
        ICommentService commentService,
        ITicketService ticketService)
    {
        _userRepository = userRepository;
        _mapper = mapper;
        _userRoleService = userRoleService;
        _projectUserRoleService = projectUserRoleService;
        _dbConnection = dbConnection;
        _commentService = commentService;
        _ticketService = ticketService;
    }

    public async Task CreateUserAsync(UserCreateDto userCreateDto)
    {
        using var transaction = DbTransactionHelper.BeginTransaction(_dbConnection);

        try
        {
            var user = _mapper.Map<User>(userCreateDto);
            user.PasswordHash = BcryptPasswordHasher.HashPassword(userCreateDto.Password);
            user.Id = Guid.NewGuid();
            await _userRepository.CreateAsync(user, transaction);

            var userRoleCreateDto = new UserRoleCreateDto(user.Id);
            await _userRoleService.CreateUserRoleAsync(userRoleCreateDto, transaction);

            transaction.Commit();
        }
        catch
        {
            transaction.Rollback();
            throw new Exception("Error while performing user creation transaction.");
        }
    }

    public async Task UpdateUserAsync(UserUpdateDto userUpdateDto)
    {
        var user = _mapper.Map<User>(userUpdateDto);
        await _userRepository.UpdateAsync(user);
    }

    public async Task DeleteUserAsync(Guid userId)
    {
        using var transaction = DbTransactionHelper.BeginTransaction(_dbConnection);

        try
        {
            await _userRepository.DeleteByIdAsync(userId, transaction);
            await _userRoleService.DeleteByUserIdAsync(userId, transaction);
            await _projectUserRoleService.DeleteByUserIdAsync(userId, transaction);
            await _commentService.DeleteByUserIdAsync(userId, transaction);
            await _ticketService.ClearUserAssignmentAsync(userId, transaction);
            await _ticketService.DeleteTicketByUserIdAsync(userId, transaction);

            transaction.Commit();
        }
        catch
        {
            transaction.Rollback();
            throw new Exception("Error while performing user deletion transaction.");
        }
    }

    public async Task<UserDto> GetUserByIdAsync(Guid userId)
    {
        var user = await _userRepository.GetByIdAsync(userId);

        return _mapper.Map<UserDto>(user);
    }

    public async Task<IEnumerable<UserDto>> GetAllUsersAsync()
    {
        var users = await _userRepository.GetAllAsync();

        return _mapper.Map<IEnumerable<UserDto>>(users);
    }

    public async Task SoftDeleteUserAsync(Guid userId)
    {
        await _userRepository.SoftDeleteByIdAsync(userId);
    }

    public async Task<IEnumerable<UserDto>> GetUsersByProjectIdAsync(Guid projectId)
    {
        var users = await _userRepository.GetAllByProjectIdAsync(projectId);

        return _mapper.Map<IEnumerable<UserDto>>(users);
    }
}
