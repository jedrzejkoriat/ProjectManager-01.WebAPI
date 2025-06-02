using MediatR;
using ProjectManager_01.Application.Contracts.Repositories;
using ProjectManager_01.Application.Contracts.Services;

namespace ProjectManager_01.Application.Features.Projects.Commands.UpdateProject;

public class UpdateProjectHandler : IRequestHandler<UpdateProjectCommand, UpdateProjectResponse>
{
    private readonly IProjectRepository projectRepository;
    private readonly IProjectService projectService;

    public UpdateProjectHandler(IProjectRepository projectRepository, IProjectService projectService)
    {
        this.projectRepository = projectRepository;
        this.projectService = projectService;
    }

    public async Task<UpdateProjectResponse> Handle(UpdateProjectCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
