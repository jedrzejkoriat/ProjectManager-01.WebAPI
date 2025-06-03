using Microsoft.AspNetCore.SignalR;
using ProjectManager_01.Application.DTOs.Tickets;

namespace ProjectManager_01.Hubs;

public class TicketsHub : Hub
{
    public async Task SubscribeToTicket(string id)
    {
        if (Guid.TryParse(id, out var guid))
        {
            var ticket = new { Id = guid }; //await ticketsRepository.GetAsync(guid);
            await Clients.Caller.SendAsync("ReceiveTicket", ticket);
        }
        else
        {
            await Clients.Caller.SendAsync("ReceiveTicket", null);
        }
    }
}
