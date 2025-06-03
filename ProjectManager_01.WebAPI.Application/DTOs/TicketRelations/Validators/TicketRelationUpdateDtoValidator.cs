using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;

namespace ProjectManager_01.Application.DTOs.TicketRelations.Validators;

public sealed class TicketRelationUpdateDtoValidator : AbstractValidator<TicketRelationUpdateDto>
{
    public TicketRelationUpdateDtoValidator()
    {
        RuleFor(t => t.Id)
            .Must(id => id != Guid.Empty).WithMessage("Id must be a valid GUID.");

        RuleFor(t => t.SourceId)
            .Must(id => id != Guid.Empty).WithMessage("SourceId must be a valid GUID.");

        RuleFor(t => t.TargetId)
            .Must(id => id != Guid.Empty).WithMessage("TargetId must be a valid GUID.");
    }
}