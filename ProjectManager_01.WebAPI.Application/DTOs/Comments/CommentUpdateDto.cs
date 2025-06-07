namespace ProjectManager_01.Application.DTOs.Comments;

public sealed record CommentUpdateDto(Guid Id, Guid TicketId, string Content);
