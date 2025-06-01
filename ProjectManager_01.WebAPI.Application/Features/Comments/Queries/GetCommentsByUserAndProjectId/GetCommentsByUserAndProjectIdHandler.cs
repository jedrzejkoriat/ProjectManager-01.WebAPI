using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace ProjectManager_01.Application.Features.Comments.Queries.GetCommentsByUserAndProjectId
{
    public class GetCommentsByUserAndProjectIdHandler : IRequestHandler<GetCommentsByUserAndProjectIdQuery, GetCommentsByUserAndProjectIdResponse>
    {
        public async Task<GetCommentsByUserAndProjectIdResponse> Handle(GetCommentsByUserAndProjectIdQuery request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
