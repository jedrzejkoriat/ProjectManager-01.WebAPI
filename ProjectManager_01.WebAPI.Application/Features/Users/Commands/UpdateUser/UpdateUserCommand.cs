using MediatR;

namespace ProjectManager_01.Application.Features.Users.Commands.UpdateUser;

public record UpdateUserCommand() : IRequest<UpdateUserResponse>;
