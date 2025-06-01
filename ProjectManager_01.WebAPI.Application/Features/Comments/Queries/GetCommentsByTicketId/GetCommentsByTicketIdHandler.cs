using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using ProjectManager_01.Application.Contracts.Repositories;
using ProjectManager_01.Application.Features.Shared;

namespace ProjectManager_01.Application.Features.Comments.Queries.GetCommentsByTicketId;
public class GetCommentsByTicketIdHandler : IRequestHandler<GetCommentsByTicketIdQuery, GetCommentsByTicketIdReponse>
{
    private readonly ICommentRepository commentRepository;

    public GetCommentsByTicketIdHandler(ICommentRepository commentRepository)
    {
        this.commentRepository = commentRepository;
    }

    public async Task<GetCommentsByTicketIdReponse> Handle(GetCommentsByTicketIdQuery request, CancellationToken cancellationToken)
    {
        var comments = await commentRepository.GetByTicketIdAsync(request.TicketId);

        var commentDtos = comments.Select(c => new CommentByTicketId(
            c.Id,
            new UserRecord(c.User.Id, c.User.UserName, c.User.Email, c.User.IsDeleted),
            c.Content,
            c.CreatedAt
            )).ToList();

        return new GetCommentsByTicketIdReponse(commentDtos);
    }
}