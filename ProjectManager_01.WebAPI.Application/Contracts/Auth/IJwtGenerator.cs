using ProjectManager_01.Application.DTOs.Auth;

namespace ProjectManager_01.Application.Contracts.Auth;

public interface IJwtGenerator
{
    string GenerateToken(UserClaimsDto userClaimsDto);
}
