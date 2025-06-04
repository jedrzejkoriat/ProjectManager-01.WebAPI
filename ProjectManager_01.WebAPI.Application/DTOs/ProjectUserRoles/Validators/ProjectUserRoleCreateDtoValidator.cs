using FluentValidation;

namespace ProjectManager_01.Application.DTOs.ProjectUserRoles.Validators;

public sealed class ProjectUserRoleCreateDtoValidator : AbstractValidator<ProjectUserRoleCreateDto>
{
    public ProjectUserRoleCreateDtoValidator()
    {
        RuleFor(p => p.ProjectId)
            .Must(id => id != Guid.Empty).WithMessage("ProjectId must be a valid GUID.");

        RuleFor(p => p.ProjectRoleId)
            .Must(id => id != Guid.Empty).WithMessage("ProjectRoleId must be a valid GUID.");

        RuleFor(p => p.UserId)
            .Must(id => id != Guid.Empty).WithMessage("UserId must be a valid GUID.");
    }
}