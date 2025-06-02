using MediatR;

namespace ProjectManager_01.Application.Features.Tags.Commands.DeleteTag;

public record DeleteTagCommand() : IRequest<DeleteTagResponse>;
