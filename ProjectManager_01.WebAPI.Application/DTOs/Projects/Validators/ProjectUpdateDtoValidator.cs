using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;

namespace ProjectManager_01.Application.DTOs.Projects.Validators;

public sealed class ProjectUpdateDtoValidator : AbstractValidator<ProjectUpdateDto>
{
    public ProjectUpdateDtoValidator()
    {
        RuleFor(p => p.Id)
            .Must(id => id != Guid.Empty).WithMessage("Id must be a valid GUID.");

        RuleFor(p => p.Name)
            .NotEmpty().WithMessage("Name cannot be empty.")
            .MinimumLength(3).WithMessage("Name must be at least 3 characters long.")
            .MaximumLength(30).WithMessage("Name cannot be longer that 100 characters.");

        RuleFor(p => p.Key)
            .NotEmpty().WithMessage("Key cannot be empty.")
            .MinimumLength(3).WithMessage("Name must be at least 3 characters long.")
            .MaximumLength(4).WithMessage("Name cannot be longer that 4 characters.");
    }
}