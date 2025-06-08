namespace ProjectManager_01.Application.DTOs.Auth;

public sealed record UserClaimsDto(Guid UserId, string Role, List<string> projectPermissions);