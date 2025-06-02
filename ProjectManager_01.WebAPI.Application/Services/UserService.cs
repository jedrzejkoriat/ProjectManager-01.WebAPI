using AutoMapper;
using ProjectManager_01.Application.Contracts.Repositories;
using ProjectManager_01.Application.Contracts.Services;
using ProjectManager_01.Application.DTOs.Users;
using ProjectManager_01.Domain.Models;

namespace ProjectManager_01.Application.Services;

public class UserService : IUserService
{
    private readonly IUserRepository userRepository;
    private readonly IMapper mapper;

    public UserService(IUserRepository userRepository, IMapper mapper)
    {
        this.userRepository = userRepository;
        this.mapper = mapper;
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
        await userRepository.DeleteAsync(userId);
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
}
