using ProjectManager_01.WebAPI.Domain.Enums;

namespace ProjectManager_01.WebAPI.Domain.Models;

public sealed class UserRole
{
    public Guid UserId { get; set; }
    public Guid RoleId { get; set; }
}
