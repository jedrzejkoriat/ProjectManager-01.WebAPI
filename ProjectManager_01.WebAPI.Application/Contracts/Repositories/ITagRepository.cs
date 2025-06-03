using ProjectManager_01.Domain.Models;

namespace ProjectManager_01.Application.Contracts.Repositories;

public interface ITagRepository : IRepository<Tag>
{
    Task<List<Tag>> GetByProjectIdAsync(Guid projectId);
}
