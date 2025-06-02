using MediatR;
using ProjectManager_01.Application.Contracts.Repositories;
using ProjectManager_01.Application.Contracts.Services;

namespace ProjectManager_01.Application.Features.Tags.Queries.GetTagsByProjectId;

public class GetTagsByProjectIdHandler : IRequestHandler<GetTagsByProjectIdQuery, GetTagsByProjectIdResponse>
{
    private readonly ITagRepository tagRepository;
    private readonly ITagService tagService;

    public GetTagsByProjectIdHandler(ITagRepository tagRepository, ITagService tagService)
    {
        this.tagRepository = tagRepository;
        this.tagService = tagService;
    }

    public async Task<GetTagsByProjectIdResponse> Handle(GetTagsByProjectIdQuery request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
