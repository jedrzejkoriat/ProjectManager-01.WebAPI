using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;

namespace ProjectManager_01.Application.DTOs.Permissions.Validators;
public sealed class PermissionUpdateDtoValidator : AbstractValidator<PermissionUpdateDto>
{
    public PermissionUpdateDtoValidator()
    {
        RuleFor(p => p.Name).NotEmpty().MinimumLength(3).MaximumLength(30);
    }
}