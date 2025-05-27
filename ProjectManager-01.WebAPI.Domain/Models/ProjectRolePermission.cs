namespace ProjectManager_01.WebAPI.Domain.Models;

public sealed class ProjectRolePermission
{
    public Guid ProjectRoleId { get; set; }
    public Guid PermissionId { get; set; }
}
