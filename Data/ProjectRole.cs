namespace ProjectManager_01.WebAPI.Data;

public sealed class ProjectRole
{
    public Guid Id { get; set; }
    public Guid ProjectId { get; set; }
    public string Name { get; set; }
}
