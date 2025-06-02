using MediatR;
using ProjectManager_01.Application.Contracts.Repositories;
using ProjectManager_01.Application.Contracts.Services;

namespace ProjectManager_01.Application.Features.TicketTags.Queries.GetTicketTagsByTicketId;

public class GetTicketTagsByTicketIdHandler : IRequestHandler<GetTicketTagsByTicketIdQuery, GetTicketTagsByTicketIdResponse>
{
    private readonly ITicketTagRepository ticketTagRepository;
    private readonly ITicketTagService ticketTagService;

    public GetTicketTagsByTicketIdHandler(ITicketTagRepository ticketTagRepository, ITicketTagService ticketTagService)
    {
        this.ticketTagRepository = ticketTagRepository;
        this.ticketTagService = ticketTagService;
    }

    public async Task<GetTicketTagsByTicketIdResponse> Handle(GetTicketTagsByTicketIdQuery request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
