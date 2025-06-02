using MediatR;
using ProjectManager_01.Application.Contracts.Repositories;
using ProjectManager_01.Application.Contracts.Services;

namespace ProjectManager_01.Application.Features.Projects.Queries.GetProjects;

public class GetProjectsHandler : IRequestHandler<GetProjectsQuery, GetProjectsResponse>
{
    private readonly IProjectRepository projectRepository;
    private readonly IProjectService projectService;

    public GetProjectsHandler(IProjectRepository projectRepository, IProjectService projectService)
    {
        this.projectRepository = projectRepository;
        this.projectService = projectService;
    }

    public async Task<GetProjectsResponse> Handle(GetProjectsQuery request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
