using MediatR;
using ProjectManager_01.Application.Contracts.Repositories;
using ProjectManager_01.Application.Contracts.Services;

namespace ProjectManager_01.Application.Features.Projects.Queries.GetProjectById;

public class GetProjectByIdHandler : IRequestHandler<GetProjectByIdQuery, GetProjectByIdResponse>
{
    private readonly IProjectRepository projectRepository;
    private readonly IProjectService projectService;

    public GetProjectByIdHandler(IProjectRepository projectRepository, IProjectService projectService)
    {
        this.projectRepository = projectRepository;
        this.projectService = projectService;
    }

    public async Task<GetProjectByIdResponse> Handle(GetProjectByIdQuery request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
