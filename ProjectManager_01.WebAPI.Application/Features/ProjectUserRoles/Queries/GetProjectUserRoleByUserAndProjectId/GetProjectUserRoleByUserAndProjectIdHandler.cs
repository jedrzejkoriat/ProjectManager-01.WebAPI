using MediatR;
using ProjectManager_01.Application.Contracts.Repositories;
using ProjectManager_01.Application.Contracts.Services;

namespace ProjectManager_01.Application.Features.ProjectUserRoles.Queries.GetProjectUserRoleByUserAndProjectId;

public class GetProjectUserRoleByUserAndProjectIdHandler : IRequestHandler<GetProjectUserRoleByUserAndProjectIdQuery, GetProjectUserRoleByUserAndProjectIdResponse>
{
    private readonly IProjectUserRoleRepository projectUserRoleRepository;
    private readonly IProjectUserRoleService projectUserRoleService;

    public GetProjectUserRoleByUserAndProjectIdHandler(IProjectUserRoleRepository projectUserRoleRepository, IProjectUserRoleService projectUserRoleService)
    {
        this.projectUserRoleRepository = projectUserRoleRepository;
        this.projectUserRoleService = projectUserRoleService;
    }

    public async Task<GetProjectUserRoleByUserAndProjectIdResponse> Handle(GetProjectUserRoleByUserAndProjectIdQuery request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
