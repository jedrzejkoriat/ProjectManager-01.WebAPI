using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using ProjectManager_01.Application.Contracts.Auth;
using ProjectManager_01.Application.Exceptions;

namespace ProjectManager_01.Application.Auth;

// This is helper class that validates if the userId retrieved from the token matches with the userId of the entity provided with it
public sealed class UserAccessValidator : IUserAccessValidator
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public UserAccessValidator(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public void ValidateUserIdAsync(Guid providedUserId)
    {
        var userIdString = _httpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        if (!Guid.TryParse(userIdString, out var userId))
            throw new UnauthorizedAccessException("User ID from token is invalid.");

        if (userId != providedUserId)
            throw new ForbiddenException("User from token does not match resource owner.");
    }

    public Guid GetAuthenticatedUser()
    {
        var userIdString = _httpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        if (!Guid.TryParse(userIdString, out var userId))
            throw new UnauthorizedAccessException("User ID from token is invalid.");

        return userId;
    }
}