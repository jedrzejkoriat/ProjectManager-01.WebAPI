namespace ProjectManager_01.Application.DTOs.Users;

public sealed record UserUpdateDto(Guid Id, string UserName, string Email, string Password);