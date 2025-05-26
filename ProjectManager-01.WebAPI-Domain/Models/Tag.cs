namespace ProjectManager_01.WebAPI.Domain.Models;

public sealed class Tag
{
    public Guid Id { get; set; }
    public Guid ProjectId { get; set; }
    public string Name { get; set; }
}