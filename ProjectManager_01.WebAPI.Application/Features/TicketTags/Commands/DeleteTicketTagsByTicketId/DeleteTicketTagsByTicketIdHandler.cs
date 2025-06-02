using MediatR;
using ProjectManager_01.Application.Contracts.Repositories;
using ProjectManager_01.Application.Contracts.Services;

namespace ProjectManager_01.Application.Features.TicketTags.Commands.DeleteTicketTagsByTicketId;

public class DeleteTicketTagsByTicketIdHandler : IRequestHandler<DeleteTicketTagsByTicketIdCommand, DeleteTicketTagsByTicketIdResponse>
{
    private readonly ITicketTagRepository ticketTagRepository;
    private readonly ITicketTagService ticketTagService;

    public DeleteTicketTagsByTicketIdHandler(ITicketTagRepository ticketTagRepository, ITicketTagService ticketTagService)
    {
        this.ticketTagRepository = ticketTagRepository;
        this.ticketTagService = ticketTagService;
    }

    public async Task<DeleteTicketTagsByTicketIdResponse> Handle(DeleteTicketTagsByTicketIdCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
