using ProjectManager_01.Application.Contracts.Repositories.Base;
using ProjectManager_01.Domain.Models;

namespace ProjectManager_01.Application.Contracts.Repositories;
public interface ITagRepository :
    ICreateable<Tag>, IReadable<Tag>, IUpdateable<Tag>, IDeleteable
{
    Task<List<Tag>> GetByProjectIdAsync(Guid projectId);
}
