namespace ProjectManager_01.API.DTOs;

public sealed class TagDTO
{
    public Guid Id { get; set; }
    public Guid ProjectId { get; set; }
    public string Name { get; set; }
}