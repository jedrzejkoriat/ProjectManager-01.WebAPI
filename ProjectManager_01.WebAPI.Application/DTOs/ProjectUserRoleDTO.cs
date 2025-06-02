namespace ProjectManager_01.Application.DTOs;

public sealed class ProjectUserRoleDTO
{
    public Guid Id { get; set; }
    public Guid ProjectId { get; set; }
    public Guid ProjectRoleId { get; set; }
    public Guid UserId { get; set; }
}