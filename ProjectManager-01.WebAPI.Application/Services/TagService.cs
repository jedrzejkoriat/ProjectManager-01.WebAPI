using ProjectManager_01.Application.Contracts.Repositories;
using ProjectManager_01.Application.Contracts.Services;

namespace ProjectManager_01.Application.Services;
public class TagService : ITagService
{
    private readonly ITagRepository tagRepository;

    public TagService(ITagRepository tagRepository)
    {
        this.tagRepository = tagRepository;
    }
}
