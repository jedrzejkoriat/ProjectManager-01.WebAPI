using MediatR;

namespace ProjectManager_01.Application.Features.Comments.Queries.GetComments;

public class GetCommentsHandler : IRequestHandler<GetCommentsQuery, GetCommentsResponse>
{
	public async Task<GetCommentsResponse> Handle(GetCommentsQuery request, CancellationToken cancellationToken)
	{
		throw new NotImplementedException();
	}
}
