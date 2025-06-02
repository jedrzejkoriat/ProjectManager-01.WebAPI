using MediatR;
using ProjectManager_01.Application.Contracts.Repositories;
using ProjectManager_01.Application.Contracts.Services;

namespace ProjectManager_01.Application.Features.Projects.Commands.CreateProject;

public class CreateProjectHandler : IRequestHandler<CreateProjectCommand, CreateProjectResponse>
{
    private readonly IProjectRepository projectRepository;
    private readonly IProjectService projectService;

    public CreateProjectHandler(IProjectRepository projectRepository, IProjectService projectService)
    {
        this.projectRepository = projectRepository;
        this.projectService = projectService;
    }

    public async Task<CreateProjectResponse> Handle(CreateProjectCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
