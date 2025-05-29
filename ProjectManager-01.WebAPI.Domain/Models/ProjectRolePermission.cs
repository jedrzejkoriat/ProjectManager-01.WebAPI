namespace ProjectManager_01.Domain.Models;

public sealed class ProjectRolePermission
{
    public Guid ProjectRoleId { get; set; }
    public Guid PermissionId { get; set; }
    List<Permission> Permissions { get; set; } = new List<Permission>();
}
