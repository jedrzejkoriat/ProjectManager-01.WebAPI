namespace ProjectManager_01.Application.DTOs.Comments;

public sealed record CommentCreateDto(Guid TicketId, Guid UserId, string Content);
