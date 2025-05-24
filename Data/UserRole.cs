using ProjectManager_01.WebAPI.Enums;

namespace ProjectManager_01.WebAPI.Data;

public sealed class UserRole
{
    public Guid UserId { get; set; }
    public Guid RoleId { get; set; }
}
