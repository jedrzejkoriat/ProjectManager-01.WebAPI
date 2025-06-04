using ProjectManager_01.Application.DTOs.Projects;

namespace ProjectManager_01.Application.DTOs.Tickets;
public sealed record TicketOverviewDto(Guid Id, ProjectDto Project, string Status, string TicketType, int TicketNumber, string Title);