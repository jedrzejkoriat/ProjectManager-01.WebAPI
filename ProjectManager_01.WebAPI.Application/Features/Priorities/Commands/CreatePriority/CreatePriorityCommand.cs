using MediatR;

namespace ProjectManager_01.Application.Features.Priorities.Commands.CreatePriority;

public record CreatePriorityCommand() : IRequest<CreatePriorityResponse>;
