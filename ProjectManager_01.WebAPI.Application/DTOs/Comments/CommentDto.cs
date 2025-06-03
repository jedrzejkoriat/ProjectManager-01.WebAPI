namespace ProjectManager_01.Application.DTOs.Comments;

public sealed record CommentDto (Guid Id, Guid TicketId, Guid UserId, string Content, DateTimeOffset CreatedAt);
