namespace ProjectManager_01.Application.DTOs.Comments;

public sealed class CommentUpdateDto
{
    public Guid Id { get; set; }
    public string Content { get; set; } = string.Empty;
}
