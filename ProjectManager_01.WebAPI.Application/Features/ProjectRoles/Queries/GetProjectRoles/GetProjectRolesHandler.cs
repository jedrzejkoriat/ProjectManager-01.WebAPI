using MediatR;
using ProjectManager_01.Application.Contracts.Repositories;
using ProjectManager_01.Application.Contracts.Services;

namespace ProjectManager_01.Application.Features.ProjectRoles.Queries.GetProjectRoles;

public class GetProjectRolesHandler : IRequestHandler<GetProjectRolesQuery, GetProjectRolesResponse>
{
    private readonly IProjectRoleRepository projectRoleRepository;
    private readonly IProjectRoleService projectRoleService;

    public GetProjectRolesHandler(IProjectRoleRepository projectRoleRepository, IProjectRoleService projectRoleService)
    {
        this.projectRoleRepository = projectRoleRepository;
        this.projectRoleService = projectRoleService;
    }

    public async Task<GetProjectRolesResponse> Handle(GetProjectRolesQuery request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
