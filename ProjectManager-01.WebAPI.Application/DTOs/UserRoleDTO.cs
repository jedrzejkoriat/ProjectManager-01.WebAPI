using ProjectManager_01.WebAPI.Domain.Enums;

namespace ProjectManager_01.WebAPI.Application.DTOs;

public sealed class UserRoleDTO
{
    public Guid UserId { get; set; }
    public Guid RoleId { get; set; }
}
