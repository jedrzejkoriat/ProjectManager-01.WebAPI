using MediatR;
using ProjectManager_01.Application.Contracts.Repositories;
using ProjectManager_01.Application.Contracts.Services;

namespace ProjectManager_01.Application.Features.Tags.Commands.DeleteTag;

public class DeleteTagHandler : IRequestHandler<DeleteTagCommand, DeleteTagResponse>
{
    private readonly ITagRepository tagRepository;
    private readonly ITagService tagService;

    public DeleteTagHandler(ITagRepository tagRepository, ITagService tagService)
    {
        this.tagRepository = tagRepository;
        this.tagService = tagService;
    }

    public async Task<DeleteTagResponse> Handle(DeleteTagCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
