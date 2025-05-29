namespace ProjectManager_01.API.DTOs;

public sealed class ProjectDTO
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Key { get; set; }
    public bool IsDeleted { get; set; }
    public DateTimeOffset CreatedAt { get; set; }
}
