using MediatR;
using ProjectManager_01.Application.Contracts.Repositories;
using ProjectManager_01.Application.Contracts.Services;

namespace ProjectManager_01.Application.Features.TicketTags.Commands.CreateTicketTag;

public class CreateTicketTagHandler : IRequestHandler<CreateTicketTagCommand, CreateTicketTagResponse>
{
    private readonly ITicketTagRepository ticketTagRepository;
    private readonly ITicketTagService ticketTagService;

    public CreateTicketTagHandler(ITicketTagRepository ticketTagRepository, ITicketTagService ticketTagService)
    {
        this.ticketTagRepository = ticketTagRepository;
        this.ticketTagService = ticketTagService;
    }

    public async Task<CreateTicketTagResponse> Handle(CreateTicketTagCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
