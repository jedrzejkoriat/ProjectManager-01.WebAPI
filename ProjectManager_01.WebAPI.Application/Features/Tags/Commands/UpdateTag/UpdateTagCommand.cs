using MediatR;

namespace ProjectManager_01.Application.Features.Tags.Commands.UpdateTag;

public record UpdateTagCommand() : IRequest<UpdateTagResponse>;
