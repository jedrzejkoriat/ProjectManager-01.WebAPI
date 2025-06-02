using MediatR;
using ProjectManager_01.Application.Contracts.Repositories;
using ProjectManager_01.Application.Contracts.Services;

namespace ProjectManager_01.Application.Features.Tags.Commands.UpdateTag;

public class UpdateTagHandler : IRequestHandler<UpdateTagCommand, UpdateTagResponse>
{
    private readonly ITagRepository tagRepository;
    private readonly ITagService tagService;

    public UpdateTagHandler(ITagRepository tagRepository, ITagService tagService)
    {
        this.tagRepository = tagRepository;
        this.tagService = tagService;
    }

    public async Task<UpdateTagResponse> Handle(UpdateTagCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
