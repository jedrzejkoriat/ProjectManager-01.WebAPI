using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;

namespace ProjectManager_01.Application.DTOs.Priorities.Validators;
public sealed class PriorityCreateDtoValidator : AbstractValidator<PriorityCreateDto>
{
    public PriorityCreateDtoValidator()
    {
        RuleFor(p => p.Name).NotEmpty().MinimumLength(2).MaximumLength(20);
    }
}