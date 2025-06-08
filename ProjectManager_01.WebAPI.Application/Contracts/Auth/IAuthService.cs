using System.Runtime.CompilerServices;
using ProjectManager_01.Application.DTOs.Auth;

namespace ProjectManager_01.Application.Contracts.Auth;

public interface IAuthService
{
    Task<string> AuthenticateUser(UserLoginDto userLoginDto);
    Task RegisterUser(UserRegisterDto userRegisterDto);
}
