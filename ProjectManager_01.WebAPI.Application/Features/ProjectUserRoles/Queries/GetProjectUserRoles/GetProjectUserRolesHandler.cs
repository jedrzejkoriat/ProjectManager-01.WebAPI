using MediatR;
using ProjectManager_01.Application.Contracts.Repositories;
using ProjectManager_01.Application.Contracts.Services;

namespace ProjectManager_01.Application.Features.ProjectUserRoles.Queries.GetProjectUserRoles;

public class GetProjectUserRolesHandler : IRequestHandler<GetProjectUserRolesQuery, GetProjectUserRolesResponse>
{
    private readonly IProjectUserRoleRepository projectUserRoleRepository;
    private readonly IProjectUserRoleService projectUserRoleService;

    public GetProjectUserRolesHandler(IProjectUserRoleRepository projectUserRoleRepository, IProjectUserRoleService projectUserRoleService)
    {
        this.projectUserRoleRepository = projectUserRoleRepository;
        this.projectUserRoleService = projectUserRoleService;
    }

    public async Task<GetProjectUserRolesResponse> Handle(GetProjectUserRolesQuery request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
