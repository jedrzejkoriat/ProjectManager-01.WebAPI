namespace ProjectManager_01.Domain.Models;

public sealed class Project
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Key { get; set; }
    public bool IsDeleted { get; set; }
    public DateTimeOffset CreatedAt { get; set; }
}
