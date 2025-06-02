using MediatR;

namespace ProjectManager_01.Application.Features.Priorities.Commands.DeletePriority;

public record DeletePriorityCommand() : IRequest<DeletePriorityResponse>;
