using ProjectManager_01.WebAPI.Enums;

namespace ProjectManager_01.WebAPI.Data;

public sealed class ProjectRolePermission
{
    public Guid ProjectRoleId { get; set; }
    public Permission Permission { get; set; }
}
