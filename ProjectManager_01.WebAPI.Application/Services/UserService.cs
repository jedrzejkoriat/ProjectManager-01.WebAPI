using System.Data;
using AutoMapper;
using Microsoft.Data.SqlClient;
using ProjectManager_01.Application.Contracts.Repositories;
using ProjectManager_01.Application.Contracts.Services;
using ProjectManager_01.Application.DTOs.UserRoles;
using ProjectManager_01.Application.DTOs.Users;
using ProjectManager_01.Application.Helpers;
using ProjectManager_01.Domain.Models;

namespace ProjectManager_01.Application.Services;

public class UserService : IUserService
{
    private readonly IUserRepository userRepository;
    private readonly IMapper mapper;
    private readonly IUserRoleService userRoleService;
    private readonly IProjectUserRoleService projectUserRoleService;
    private readonly IDbConnection dbConnection;
    private readonly ICommentService commentService;
    private readonly ITicketService ticketService;

    public UserService(IUserRepository userRepository, 
        IMapper mapper, 
        IUserRoleService userRoleService,
        IProjectUserRoleService projectUserRoleService,
        IDbConnection dbConnection,
        ICommentService commentService,
        ITicketService ticketService)
    {
        this.userRepository = userRepository;
        this.mapper = mapper;
        this.userRoleService = userRoleService;
        this.projectUserRoleService = projectUserRoleService;
        this.dbConnection = dbConnection;
        this.commentService = commentService;
        this.ticketService = ticketService;
    }

    public async Task CreateUserAsync(UserCreateDto userCreateDto)
    {
        using var transaction = dbConnection.BeginTransaction();

        try
        {
            User user = mapper.Map<User>(userCreateDto);
            user.PasswordHash = BcryptPasswordHasher.HashPassword(userCreateDto.Password);
            var userId = await userRepository.CreateAsync(user, transaction);

            var userRoleCreateDto = new UserRoleCreateDto(userId);
            await userRoleService.CreateUserRoleAsync(userRoleCreateDto, transaction);

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
        User user = mapper.Map<User>(userUpdateDto);
        await userRepository.UpdateAsync(user);
    }

    public async Task DeleteUserAsync(Guid userId)
    {
        using var transaction = dbConnection.BeginTransaction();

        try
        {
            await userRepository.DeleteAsync(userId, transaction);
            await userRoleService.DeleteByUserIdAsync(userId, transaction);
            await projectUserRoleService.DeleteByUserIdAsync(userId, transaction);
            await commentService.DeleteByUserIdAsync(userId, transaction);
            await ticketService.ClearUserAssignmentAsync(userId, transaction);
            await ticketService.DeleteTicketByUserIdAsync(userId, transaction);

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
        User user = await userRepository.GetByIdAsync(userId);

        return mapper.Map<UserDto>(user);
    }

    public async Task<List<UserDto>> GetAllUsersAsync()
    {
        List<User> users = await userRepository.GetAllAsync();

        return mapper.Map<List<UserDto>>(users);
    }

    public async Task SoftDeleteUserAsync(Guid userId)
    {
        await userRepository.SoftDeleteAsync(userId);
    }
}
