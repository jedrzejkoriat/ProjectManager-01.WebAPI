using FluentValidation;

namespace ProjectManager_01.Application.DTOs.ProjectRoles.Validators;

public sealed class ProjectRoleCreateDtoValidator : AbstractValidator<ProjectRoleCreateDto>
{
    public ProjectRoleCreateDtoValidator()
    {
        RuleFor(p => p.ProjectId)
            .Must(id => id != Guid.Empty).WithMessage("ProjectId must be a valid GUID.");

        RuleFor(p => p.Name)
            .NotEmpty().WithMessage("Name cannot be empty.")
            .MinimumLength(3).WithMessage("Name must be at least 3 characters long.")
            .MaximumLength(30).WithMessage("Name cannot be longer that 30 characters.");
    }
}