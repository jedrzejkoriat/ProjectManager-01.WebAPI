using MediatR;
using ProjectManager_01.Application.Contracts.Repositories;
using ProjectManager_01.Application.Contracts.Services;

namespace ProjectManager_01.Application.Features.TicketTags.Commands.DeleteTicketTagsByTagId;

public class DeleteTicketTagsByTagIdHandler : IRequestHandler<DeleteTicketTagsByTagIdCommand, DeleteTicketTagsByTagIdResponse>
{
    private readonly ITicketTagRepository ticketTagRepository;
    private readonly ITicketTagService ticketTagService;

    public DeleteTicketTagsByTagIdHandler(ITicketTagRepository ticketTagRepository, ITicketTagService ticketTagService)
    {
        this.ticketTagRepository = ticketTagRepository;
        this.ticketTagService = ticketTagService;
    }

    public async Task<DeleteTicketTagsByTagIdResponse> Handle(DeleteTicketTagsByTagIdCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
