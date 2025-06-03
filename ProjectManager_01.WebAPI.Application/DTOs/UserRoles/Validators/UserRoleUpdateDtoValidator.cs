using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;

namespace ProjectManager_01.Application.DTOs.UserRoles.Validators;

public sealed class UserRoleUpdateDtoValidator : AbstractValidator<UserRoleUpdateDto>
{
    public UserRoleUpdateDtoValidator()
    {
        RuleFor(u => u.UserId)
            .Must(id => id != Guid.Empty).WithMessage("UserId must be a valid GUID.");

        RuleFor(u => u.RoleId)
            .Must(id => id != Guid.Empty).WithMessage("RoleId must be a valid GUID.");
    }
}