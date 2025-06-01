using ProjectManager_01.Application.Contracts.Repositories;
using ProjectManager_01.Application.Contracts.Services;

namespace ProjectManager_01.Application.Services;
public class ProjectRoleService : IProjectRoleService
{
    private readonly IProjectRoleRepository projectRoleRepository;

    public ProjectRoleService(IProjectRoleRepository projectRoleRepository)
    {
        this.projectRoleRepository = projectRoleRepository;
    }
}
