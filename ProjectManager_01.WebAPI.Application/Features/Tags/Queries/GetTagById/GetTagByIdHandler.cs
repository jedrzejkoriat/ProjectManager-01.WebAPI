using MediatR;
using ProjectManager_01.Application.Contracts.Repositories;
using ProjectManager_01.Application.Contracts.Services;

namespace ProjectManager_01.Application.Features.Tags.Queries.GetTagById;

public class GetTagByIdHandler : IRequestHandler<GetTagByIdQuery, GetTagByIdResponse>
{
    private readonly ITagRepository tagRepository;
    private readonly ITagService tagService;

    public GetTagByIdHandler(ITagRepository tagRepository, ITagService tagService)
    {
        this.tagRepository = tagRepository;
        this.tagService = tagService;
    }

    public async Task<GetTagByIdResponse> Handle(GetTagByIdQuery request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
