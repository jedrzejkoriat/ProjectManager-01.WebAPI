using ProjectManager_01.Application.DTOs.Tickets;

namespace ProjectManager_01.Application.DTOs.TicketRelations;

public sealed record TicketRelationDto(Guid Id, TicketOverviewDto Source, TicketOverviewDto Target, int RelationType);