using MediatR;

namespace ProjectManager_01.Application.Features.Projects.Commands.CreateProject;

public record CreateProjectCommand() : IRequest<CreateProjectResponse>;
