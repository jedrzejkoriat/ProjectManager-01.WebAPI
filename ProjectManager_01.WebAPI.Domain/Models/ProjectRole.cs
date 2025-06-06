namespace ProjectManager_01.Domain.Models;

public sealed class ProjectRole
{
    public Guid Id { get; set; }
    public Guid ProjectId { get; set; }
    public string Name { get; set; }
    public List<Permission> Permissions { get; set; }
}
