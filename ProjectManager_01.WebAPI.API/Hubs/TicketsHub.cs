using Microsoft.AspNetCore.SignalR;
using ProjectManager_01.Application.Contracts.Services;

namespace ProjectManager_01.Hubs;

/// <summary>
/// SignalR hub for live updates on tickets.
/// </summary>
public class TicketsHub : Hub
{
    private readonly ITicketService _ticketService;

    public TicketsHub(ITicketService ticketService)
    {
        _ticketService = ticketService;
    }

    // Add subscriber to a group based on ticketId
    public async Task SubscribeToTicket(string ticketId)
    {
        if (Guid.TryParse(ticketId, out var guid))
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, GetTicketGroupName(guid));

            var ticket = await _ticketService.GetTicketByIdAsync(guid);
            await Clients.Caller.SendAsync("ReceiveTicket", ticket);
        }
        else
        {
            await Clients.Caller.SendAsync("ReceiveTicket", null);
        }
    }

    // Unsubscribe from the group by ticketId
    public async Task UnsubscribeFromTicket(string ticketId)
    {
        if (Guid.TryParse(ticketId, out var guid))
        {
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, GetTicketGroupName(guid));
        }
    }

    // Get group name helper
    private string GetTicketGroupName(Guid ticketId)
    {
        return $"ticket-{ticketId}";
    }
}
