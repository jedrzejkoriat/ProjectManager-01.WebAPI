using MediatR;

namespace ProjectManager_01.Application.Features.Projects.Commands.DeleteProject;

public record DeleteProjectCommand() : IRequest<DeleteProjectResponse>;
