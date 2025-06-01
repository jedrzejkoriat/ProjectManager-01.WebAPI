using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectManager_01.Application.Features.Shared;

namespace ProjectManager_01.Application.Features.Comments.Queries.GetCommentsByTicketId;
public record GetCommentsByTicketIdReponse (List<CommentByTicketId> Comments);
public record CommentByTicketId(Guid Id, UserRecord User, string Content, DateTimeOffset CreatedAt);