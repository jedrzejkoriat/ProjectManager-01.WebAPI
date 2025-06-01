using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace ProjectManager_01.Application.Features.Comments.Queries.GetCommentsByTicketId;
public record GetCommentsByTicketIdQuery (Guid TicketId) : IRequest<GetCommentsByTicketIdReponse>;