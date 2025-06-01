namespace ProjectManager_01.Domain.Models;

public sealed class User
{
    public Guid Id { get; set; }
    public string UserName { get; set; }
    public string Email { get; set; }
    public string PasswordHash { get; set; }
    public bool IsDeleted { get; set; }
    public DateTimeOffset CreatedAt { get; set; }
    public UserRole UserRole { get; set; }
    public List<ProjectUserRole> ProjectUserRole { get; set; }
}