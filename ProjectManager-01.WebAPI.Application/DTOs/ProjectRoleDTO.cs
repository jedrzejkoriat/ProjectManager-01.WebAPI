namespace ProjectManager_01.WebAPI.Application.DTOs;

public sealed class ProjectRoleDTO
{
    public Guid Id { get; set; }
    public Guid ProjectId { get; set; }
    public string Name { get; set; }
}
