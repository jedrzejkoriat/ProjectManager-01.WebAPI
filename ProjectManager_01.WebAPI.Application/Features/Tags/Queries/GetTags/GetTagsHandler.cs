using MediatR;
using ProjectManager_01.Application.Contracts.Repositories;
using ProjectManager_01.Application.Contracts.Services;

namespace ProjectManager_01.Application.Features.Tags.Queries.GetTags;

public class GetTagsHandler : IRequestHandler<GetTagsQuery, GetTagsResponse>
{
    private readonly ITagRepository tagRepository;
    private readonly ITagService tagService;

    public GetTagsHandler(ITagRepository tagRepository, ITagService tagService)
    {
        this.tagRepository = tagRepository;
        this.tagService = tagService;
    }

    public async Task<GetTagsResponse> Handle(GetTagsQuery request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
