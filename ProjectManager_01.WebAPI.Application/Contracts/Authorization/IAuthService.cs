using ProjectManager_01.Application.DTOs.Users;

namespace ProjectManager_01.Application.Contracts.Authorization;

public interface IAuthService
{
    Task<string> AuthenticateUser(UserLoginDto userLoginDto);
}
