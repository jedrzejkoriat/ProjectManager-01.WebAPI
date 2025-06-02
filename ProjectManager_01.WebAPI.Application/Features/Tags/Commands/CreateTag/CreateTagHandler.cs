using MediatR;
using ProjectManager_01.Application.Contracts.Repositories;
using ProjectManager_01.Application.Contracts.Services;

namespace ProjectManager_01.Application.Features.Tags.Commands.CreateTag;

public class CreateTagHandler : IRequestHandler<CreateTagCommand, CreateTagResponse>
{
    private readonly ITagRepository tagRepository;
    private readonly ITagService tagService;

    public CreateTagHandler(ITagRepository tagRepository, ITagService tagService)
    {
        this.tagRepository = tagRepository;
        this.tagService = tagService;
    }

    public async Task<CreateTagResponse> Handle(CreateTagCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
