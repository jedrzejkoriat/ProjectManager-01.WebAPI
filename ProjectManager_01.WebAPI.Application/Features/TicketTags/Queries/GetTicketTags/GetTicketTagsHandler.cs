using MediatR;
using ProjectManager_01.Application.Contracts.Repositories;
using ProjectManager_01.Application.Contracts.Services;

namespace ProjectManager_01.Application.Features.TicketTags.Queries.GetTicketTags;

public class GetTicketTagsHandler : IRequestHandler<GetTicketTagsQuery, GetTicketTagsResponse>
{
    private readonly ITicketTagRepository ticketTagRepository;
    private readonly ITicketTagService ticketTagService;

    public GetTicketTagsHandler(ITicketTagRepository ticketTagRepository, ITicketTagService ticketTagService)
    {
        this.ticketTagRepository = ticketTagRepository;
        this.ticketTagService = ticketTagService;
    }

    public async Task<GetTicketTagsResponse> Handle(GetTicketTagsQuery request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
