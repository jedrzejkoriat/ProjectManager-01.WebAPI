using MediatR;
using ProjectManager_01.Application.Contracts.Repositories;
using ProjectManager_01.Application.Contracts.Services;

namespace ProjectManager_01.Application.Features.TicketTags.Queries.GetTicketTagsByTagId;

public class GetTicketTagsByTagIdHandler : IRequestHandler<GetTicketTagsByTagIdQuery, GetTicketTagsByTagIdResponse>
{
    private readonly ITicketTagRepository ticketTagRepository;
    private readonly ITicketTagService ticketTagService;

    public GetTicketTagsByTagIdHandler(ITicketTagRepository ticketTagRepository, ITicketTagService ticketTagService)
    {
        this.ticketTagRepository = ticketTagRepository;
        this.ticketTagService = ticketTagService;
    }

    public async Task<GetTicketTagsByTagIdResponse> Handle(GetTicketTagsByTagIdQuery request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
