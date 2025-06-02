using ProjectManager_01.Application.Contracts.Repositories;
using ProjectManager_01.Application.Contracts.Services;

namespace ProjectManager_01.Application.Services;

public class UserRoleService : IUserRoleService
{
    private readonly IUserRoleRepository userRoleRepository;

    public UserRoleService(IUserRoleRepository userRoleRepository)
    {
        this.userRoleRepository = userRoleRepository;
    }
}
