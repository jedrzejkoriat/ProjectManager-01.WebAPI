using MediatR;

namespace ProjectManager_01.Application.Features.Tags.Commands.CreateTag;

public record CreateTagCommand() : IRequest<CreateTagResponse>;
