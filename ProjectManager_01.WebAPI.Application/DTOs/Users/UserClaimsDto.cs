namespace ProjectManager_01.Application.DTOs.Users;

public sealed record UserClaimsDto(Guid Id, string Role, List<string> projectPermissions);