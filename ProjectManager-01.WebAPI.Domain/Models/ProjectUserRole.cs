namespace ProjectManager_01.Domain.Models;

public sealed class ProjectUserRole
{
    public Guid Id { get; set; }
    public Guid ProjectId { get; set; }
    public Guid ProjectRoleId { get; set; }
    public Guid UserId { get; set; }
}