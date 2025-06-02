using MediatR;

namespace ProjectManager_01.Application.Features.Users.Commands.SoftDeleteUser;

public record SoftDeleteUserCommand() : IRequest<SoftDeleteUserResponse>;
