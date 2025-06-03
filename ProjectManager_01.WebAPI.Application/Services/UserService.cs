using AutoMapper;
using Microsoft.Data.SqlClient;
using ProjectManager_01.Application.Contracts.Factories;
using ProjectManager_01.Application.Contracts.Repositories;
using ProjectManager_01.Application.Contracts.Services;
using ProjectManager_01.Application.DTOs.Users;
using ProjectManager_01.Domain.Models;

namespace ProjectManager_01.Application.Services;

public class UserService : IUserService
{

    private readonly IUserRepository userRepository;
    private readonly IMapper mapper;
    private readonly IDbConnectionFactory dbConnectionFactory;
    private readonly IUserRoleService userRoleService;
    private readonly IProjectUserRoleService projectUserRoleService;
    private readonly ICommentService commentService;
    private readonly ITicketService ticketService;

    public UserService(IUserRepository userRepository, 
        IMapper mapper, 
        IDbConnectionFactory dbConnectionFactory, 
        IUserRoleService userRoleService,
        IProjectUserRoleService projectUserRoleService,
        ICommentService commentService,
        ITicketService ticketService)
    {
        this.userRepository = userRepository;
        this.mapper = mapper;
        this.dbConnectionFactory = dbConnectionFactory;
        this.userRoleService = userRoleService;
        this.projectUserRoleService = projectUserRoleService;
        this.commentService = commentService;
        this.ticketService = ticketService;
    }

    public async Task CreateUserAsync(UserCreateDto userCreateDto)
    {
        User user = mapper.Map<User>(userCreateDto);
        await userRepository.CreateAsync(user);
    }

    public async Task UpdateUserAsync(UserUpdateDto userUpdateDto)
    {
        User user = mapper.Map<User>(userUpdateDto);
        await userRepository.UpdateAsync(user);
    }

    public async Task DeleteUserAsync(Guid userId)
    {
        using var connection = dbConnectionFactory.CreateConnection();

        switch (connection)
        {
            case SqlConnection sqlConnection:
                await sqlConnection.OpenAsync();
                break;
            default:
                connection.Open();
                break;
        }

        using var transaction = connection.BeginTransaction();

        try
        {
            await userRepository.DeleteAsync(userId, connection, transaction);
            await userRoleService.DeleteByUserIdAsync(userId, connection, transaction);
            await projectUserRoleService.DeleteByUserIdAsync(userId, connection, transaction);
            await commentService.DeleteByUserIdAsync(userId, connection, transaction);
            await ticketService.ClearUserAssignmentAsync(userId, connection, transaction);
            await ticketService.DeleteTicketByUserIdAsync(userId, connection, transaction);

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
