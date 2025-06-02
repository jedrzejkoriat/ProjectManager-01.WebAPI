using MediatR;
using ProjectManager_01.Application.Contracts.Repositories;
using ProjectManager_01.Application.Contracts.Services;

namespace ProjectManager_01.Application.Features.TicketTags.Queries.GetTicketTagById;

public class GetTicketTagByIdHandler : IRequestHandler<GetTicketTagByIdQuery, GetTicketTagByIdResponse>
{
    private readonly ITicketTagRepository ticketTagRepository;
    private readonly ITicketTagService ticketTagService;

    public GetTicketTagByIdHandler(ITicketTagRepository ticketTagRepository, ITicketTagService ticketTagService)
    {
        this.ticketTagRepository = ticketTagRepository;
        this.ticketTagService = ticketTagService;
    }

    public async Task<GetTicketTagByIdResponse> Handle(GetTicketTagByIdQuery request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
