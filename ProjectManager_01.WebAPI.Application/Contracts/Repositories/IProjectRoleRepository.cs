using ProjectManager_01.Application.Contracts.Repositories.Base;
using ProjectManager_01.Domain.Models;

namespace ProjectManager_01.Application.Contracts.Repositories;

public interface IProjectRoleRepository :
    ICreateable<ProjectRole>, IReadable<ProjectRole>, IUpdateable<ProjectRole>, IDeleteable
{
}
