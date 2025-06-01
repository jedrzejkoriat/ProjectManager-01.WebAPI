using ProjectManager_01.Application.Contracts.Repositories;
using ProjectManager_01.Application.Contracts.Services;

namespace ProjectManager_01.Application.Services;
public class ProjectUserRoleService : IProjectUserRoleService
{
    private readonly IProjectUserRoleRepository projectUserRoleRepository;

    public ProjectUserRoleService(IProjectUserRoleRepository projectUserRoleRepository)
    {
        this.projectUserRoleRepository = projectUserRoleRepository;
    }
}
