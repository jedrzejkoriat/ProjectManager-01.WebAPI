using ProjectManager_01.Application.Contracts.Repositories.Base;
using ProjectManager_01.Domain.Models;

namespace ProjectManager_01.Application.Contracts.Repositories;

public interface IProjectRepository :
    ICreateable<Project>, IReadable<Project>, IUpdateable<Project>, IDeleteable, ISoftDeletable
{
}
