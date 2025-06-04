using FluentValidation;

namespace ProjectManager_01.Application.DTOs.ProjectRolePermissions.Validators;

public sealed class ProjectRolePermissionCreateDtoValidator : AbstractValidator<ProjectRolePermissionCreateDto>
{
    public ProjectRolePermissionCreateDtoValidator()
    {
        RuleFor(p => p.ProjectRoleId)
            .Must(id => id != Guid.Empty).WithMessage("ProjectRoleId must be a valid GUID.");

        RuleFor(p => p.PermissionId)
            .Must(id => id != Guid.Empty).WithMessage("PermissionId must be a valid GUID.");
    }
}