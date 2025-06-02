using MediatR;
using ProjectManager_01.Application.Contracts.Repositories;
using ProjectManager_01.Application.Contracts.Services;

namespace ProjectManager_01.Application.Features.ProjectUserRoles.Queries.GetProjectUserRoleByUserId;

public class GetProjectUserRoleByUserIdHandler : IRequestHandler<GetProjectUserRoleByUserIdQuery, GetProjectUserRoleByUserIdResponse>
{
    private readonly IProjectUserRoleRepository projectUserRoleRepository;
    private readonly IProjectUserRoleService projectUserRoleService;

    public GetProjectUserRoleByUserIdHandler(IProjectUserRoleRepository projectUserRoleRepository, IProjectUserRoleService projectUserRoleService)
    {
        this.projectUserRoleRepository = projectUserRoleRepository;
        this.projectUserRoleService = projectUserRoleService;
    }

    public async Task<GetProjectUserRoleByUserIdResponse> Handle(GetProjectUserRoleByUserIdQuery request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
