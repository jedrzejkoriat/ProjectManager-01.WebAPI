namespace ProjectManager_01.Application.DTOs.Users;

public sealed record UserClaimsDto(Guid UserId, string Role, List<string> projectPermissions);