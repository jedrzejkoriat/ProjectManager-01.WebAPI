﻿using MediatR;

namespace ProjectManager_01.Application.Features.ProjectUserRoles.Queries.GetProjectUserRoleByUserAndProjectId;

public record GetProjectUserRoleByUserAndProjectIdQuery() : IRequest<GetProjectUserRoleByUserAndProjectIdResponse>;
