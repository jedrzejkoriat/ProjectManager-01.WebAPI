using ProjectManager_01.Application.DTOs.Users;

namespace ProjectManager_01.Application.Contracts.Services;

public interface IUserService
{
    Task CreateUserAsync(UserCreateDto userCreateDto);
    Task UpdateUserAsync(UserUpdateDto userUpdateDto);
    Task DeleteUserAsync(Guid userId);
    Task<UserDto> GetUserByIdAsync(Guid userId);
    Task<List<UserDto>> GetAllUsersAsync();
}
