using MediatR;

namespace ProjectManager_01.Application.Features.Priorities.Commands.UpdatePriority;

public record UpdatePriorityCommand() : IRequest<UpdatePriorityResponse>;
