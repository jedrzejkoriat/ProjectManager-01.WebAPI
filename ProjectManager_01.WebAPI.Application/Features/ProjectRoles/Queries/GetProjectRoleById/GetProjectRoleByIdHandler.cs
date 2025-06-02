using MediatR;
using ProjectManager_01.Application.Contracts.Repositories;
using ProjectManager_01.Application.Contracts.Services;

namespace ProjectManager_01.Application.Features.ProjectRoles.Queries.GetProjectRoleById;

public class GetProjectRoleByIdHandler : IRequestHandler<GetProjectRoleByIdQuery, GetProjectRoleByIdResponse>
{
    private readonly IProjectRoleRepository projectRoleRepository;
    private readonly IProjectRoleService projectRoleService;

    public GetProjectRoleByIdHandler(IProjectRoleRepository projectRoleRepository, IProjectRoleService projectRoleService)
    {
        this.projectRoleRepository = projectRoleRepository;
        this.projectRoleService = projectRoleService;
    }

    public async Task<GetProjectRoleByIdResponse> Handle(GetProjectRoleByIdQuery request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
