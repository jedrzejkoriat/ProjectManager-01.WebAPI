namespace ProjectManager_01.Application.DTOs.Users;

public sealed record UserDto(Guid Id, string UserName, string Email, bool IsDeleted, DateTimeOffset CreatedAt);