using ProjectManager_01.Application.DTOs.Users;

namespace ProjectManager_01.Application.DTOs.Comments;

public sealed record CommentDto(Guid Id, Guid TicketId, UserDto User, string Content, DateTimeOffset CreatedAt);
