using ProjectManager_01.Application.Contracts.Repositories;
using ProjectManager_01.Application.Contracts.Services;

namespace ProjectManager_01.Application.Services;
public class ProjectRolePermissionService : IProjectRolePermissionService
{
    private readonly IProjectRolePermissionRepository projectRolePermissionRepository;

    public ProjectRolePermissionService(IProjectRolePermissionRepository projectRolePermissionRepository)
    {
        this.projectRolePermissionRepository = projectRolePermissionRepository;
    }
}
