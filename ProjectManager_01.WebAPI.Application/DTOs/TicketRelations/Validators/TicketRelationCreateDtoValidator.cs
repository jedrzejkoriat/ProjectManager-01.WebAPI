using FluentValidation;

namespace ProjectManager_01.Application.DTOs.TicketRelations.Validators;

public sealed class TicketRelationCreateDtoValidator : AbstractValidator<TicketRelationCreateDto>
{
    public TicketRelationCreateDtoValidator()
    {
        RuleFor(t => t.SourceId)
            .Must(id => id != Guid.Empty).WithMessage("SourceId must be a valid GUID.");

        RuleFor(t => t.TargetId)
            .Must(id => id != Guid.Empty).WithMessage("TargetId must be a valid GUID.");

        RuleFor(t => t.TargetId)
            .Must((dto, targetId) => targetId != dto.SourceId)
            .WithMessage("SourceId and TargetId must be different.");
    }
}