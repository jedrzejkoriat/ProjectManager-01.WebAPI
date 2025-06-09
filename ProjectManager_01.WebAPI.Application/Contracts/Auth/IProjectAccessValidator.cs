using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManager_01.Application.Contracts.Auth;

public interface IProjectAccessValidator
{
    void ValidateProjectIds(Guid projectId, Guid providedProjectId);
    Task ValidateTagProjectIdAsync(Guid tagId, Guid projectId);
    Task ValidateTicketProjectIdAsync(Guid ticketId, Guid projectId);
}
