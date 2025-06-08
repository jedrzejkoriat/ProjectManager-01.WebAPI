using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.SignalR;
using ProjectManager_01.Application.Contracts.Auth;
using ProjectManager_01.Application.Contracts.Services;

namespace ProjectManager_01.Hubs;

/// <summary>
/// SignalR hub for live updates on tickets.
/// </summary>
public class TicketsHub : Hub
{
    private readonly ITicketService _ticketService;
    private readonly ILogger<TicketsHub> _logger;
    private readonly IProjectPermissionSignalR _projectPermissionSignalR;

    public TicketsHub(
        ITicketService ticketService, 
        ILogger<TicketsHub> logger,
        IProjectPermissionSignalR projectPermissionSignalR)
    {
        _ticketService = ticketService;
        _logger = logger;
        _projectPermissionSignalR = projectPermissionSignalR;
    }

    public override async Task OnConnectedAsync()
    {
        try
        {
            var httpContext = Context.GetHttpContext();
            var projectId = httpContext.Request.Query["projectId"].ToString();

            if (!_projectPermissionSignalR.AuthorizeSignalR(Context, projectId))
            {
                _logger.LogError("Unauthorized access attempt to TicketsHub for project {ProjectId}", projectId);
                Context.Abort();
                return;
            }

            _logger.LogInformation("Client connected to TicketsHub for project {ProjectId}", projectId);
            await base.OnConnectedAsync();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error during connection to TicketsHub");
            Context.Abort();
        }
    }

    // Add subscriber to a group based on ticketId
    public async Task SubscribeToTicket(string ticketId, string projectId)
    {
        try
        {
            Guid ticketGuid = Guid.Parse(ticketId);
            Guid projectGuid = Guid.Parse(projectId);

            await Groups.AddToGroupAsync(Context.ConnectionId, GetTicketGroupName(ticketGuid));
            var ticket = await _ticketService.GetTicketByIdAsync(ticketGuid, projectGuid);

            await Clients.Caller.SendAsync("ReceiveTicket", ticket);
            _logger.LogInformation("Subscribed to ticket {TicketId} for project {ProjectId}", ticketId, projectId);
        }
        catch
        {
            _logger.LogError("Error subscribing to ticket {TicketId} for project {ProjectId}", ticketId, projectId);
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
