using MediatR;
using ProjectManager_01.Application.Contracts.Repositories;
using ProjectManager_01.Application.Contracts.Services;

namespace ProjectManager_01.Application.Features.Projects.Commands.SoftDeleteProject;

public class SoftDeleteProjectHandler : IRequestHandler<SoftDeleteProjectCommand, SoftDeleteProjectResponse>
{
    private readonly IProjectRepository projectRepository;
    private readonly IProjectService projectService;

    public SoftDeleteProjectHandler(IProjectRepository projectRepository, IProjectService projectService)
    {
        this.projectRepository = projectRepository;
        this.projectService = projectService;
    }

    public async Task<SoftDeleteProjectResponse> Handle(SoftDeleteProjectCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
