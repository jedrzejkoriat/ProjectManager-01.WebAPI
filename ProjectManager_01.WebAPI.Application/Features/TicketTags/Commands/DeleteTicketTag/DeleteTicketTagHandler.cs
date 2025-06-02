using MediatR;
using ProjectManager_01.Application.Contracts.Repositories;
using ProjectManager_01.Application.Contracts.Services;

namespace ProjectManager_01.Application.Features.TicketTags.Commands.DeleteTicketTag;

public class DeleteTicketTagHandler : IRequestHandler<DeleteTicketTagCommand, DeleteTicketTagResponse>
{
    private readonly ITicketTagRepository ticketTagRepository;
    private readonly ITicketTagService ticketTagService;

    public DeleteTicketTagHandler(ITicketTagRepository ticketTagRepository, ITicketTagService ticketTagService)
    {
        this.ticketTagRepository = ticketTagRepository;
        this.ticketTagService = ticketTagService;
    }

    public async Task<DeleteTicketTagResponse> Handle(DeleteTicketTagCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
