namespace ProjectManager_01.Domain.Models;

public sealed class ProjectRolePermission
{
    public Guid ProjectRoleId { get; set; }
    public Guid PermissionId { get; set; }
    public Permission Permission { get; set; }
}
