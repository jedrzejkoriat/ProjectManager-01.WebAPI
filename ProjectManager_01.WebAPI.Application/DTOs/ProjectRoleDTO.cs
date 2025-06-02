namespace ProjectManager_01.Application.DTOs;

public sealed class ProjectRoleDTO
{
    public Guid Id { get; set; }
    public Guid ProjectId { get; set; }
    public string Name { get; set; }
}
