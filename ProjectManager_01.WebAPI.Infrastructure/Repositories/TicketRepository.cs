using System.Data;
using Dapper;
using ProjectManager_01.Application.Contracts.Repositories;
using ProjectManager_01.Domain.Models;

namespace ProjectManager_01.Infrastructure.Repositories;

internal class TicketRepository : ITicketRepository
{
    private readonly IDbConnection dbConnection;

    public TicketRepository(IDbConnection dbConnection)
    {
        this.dbConnection = dbConnection;
    }

    // ============================= QUERIES =============================
    public async Task<IEnumerable<Ticket>> GetAllAsync()
    {
        var sql = @"SELECT t.*, p.*
                    FROM Tickets t
                    JOIN Projects p ON t.ProjectId = p.Id";

        var tickets = await dbConnection.QueryAsync<Ticket, Project, Ticket>(
            sql,
            (ticket, project) =>
            {
                ticket.Project = project;
                return ticket;
            },
            splitOn: "Id"  // "Id" to kolumna, gdzie zaczyna się Project w wynikach, zakładamy, że p.Id
        );

        return tickets.ToList();
    }

    public async Task<IEnumerable<Ticket>> GetByProjectIdAsync(Guid projectId)
    {
        var sql = @"SELECT t.*, p.*
                    FROM Tickets t
                    JOIN Projects p ON t.ProjectId = p.Id
                    WHERE t.ProjectId = @ProjectId";

        var tickets = await dbConnection.QueryAsync<Ticket, Project, Ticket>(
            sql,
            (ticket, project) =>
            {
                ticket.Project = project;
                return ticket;
            },
            new { ProjectId = projectId },
            splitOn: "Id"
        );

        return tickets.ToList();
    }

    public async Task<Ticket> GetByIdAsync(Guid id)
    {
        var sql = @"SELECT 
                        t.*, 
                        proj.Id AS ProjectId, proj.Name AS ProjectName, proj.Key AS ProjectKey, proj.IsDeleted AS ProjectIsDeleted, proj.CreatedAt AS ProjectCreatedAt,
                        prio.Id AS PriorityId, prio.Name AS PriorityName, prio.Level AS PriorityLevel, 
                        a.Id AS AssigneeId, a.UserName AS AssigneeUserName, a.Email AS AssigneeEmail,
                        r.Id AS ReporterId, r.UserName AS ReporterUserName, r.Email AS ReporterEmail
                    FROM Tickets t
                    JOIN Projects proj ON t.ProjectId = proj.Id
                    JOIN Priorities prio ON t.PriorityId = prio.Id
                    LEFT JOIN Users a ON t.AssigneeId = a.Id
                    JOIN Users r ON t.ReporterId = r.Id
                    WHERE t.Id = @Id
                    AND t.IsDeleted = 0";

        var ticket = (await dbConnection.QueryAsync<Ticket, Project, Priority, User, User, Ticket>(
            sql,
            (ticket, project, priority, assignee, reporter) =>
            {
                ticket.Project = project;
                ticket.Priority = priority;
                ticket.Assignee = assignee;
                ticket.Reporter = reporter;
                return ticket;
            },
            new { Id = id },
            splitOn: "ProjectId,PriorityId,AssigneeId,ReporterId"
        )).FirstOrDefault();

        return ticket;
    }

    public async Task<Ticket> GetByKeyAndNumberAsync(string projectKey, int ticketNumber)
    {
        var sql = @"SELECT 
                        t.*, 
                        proj.Id AS ProjectId, proj.Name AS ProjectName, proj.Key AS ProjectKey, proj.IsDeleted AS ProjectIsDeleted, proj.CreatedAt AS ProjectCreatedAt,
                        prio.Id AS PriorityId, prio.Name AS PriorityName, prio.Level AS PriorityLevel, 
                        a.Id AS AssigneeId, a.UserName AS AssigneeUserName, a.Email AS AssigneeEmail,
                        r.Id AS ReporterId, r.UserName AS ReporterUserName, r.Email AS ReporterEmail
                    FROM Tickets t
                    JOIN Projects proj ON t.ProjectId = proj.Id
                    JOIN Priorities prio ON t.PriorityId = prio.Id
                    LEFT JOIN Users a ON t.AssigneeId = a.Id
                    JOIN Users r ON t.ReporterId = r.Id
                    WHERE t.TicketNumber = @TicketNumber
                    AND proj.Key = @ProjectKey
                    AND t.IsDeleted = 0";

        var ticket = (await dbConnection.QueryAsync<Ticket, Project, Priority, User, User, Ticket>
            (sql, (ticket, project, priority, assignee, reporter) =>
            {
                ticket.Project = project;
                ticket.Priority = priority;
                ticket.Assignee = assignee;
                ticket.Reporter = reporter;
                return ticket;
            },
        new { TicketNumber = ticketNumber, ProjectKey = projectKey },
        splitOn: "ProjectId,PriorityId,AssigneeId,ReporterId"
        )).FirstOrDefault();

        return ticket;
    }

    public async Task<IEnumerable<Ticket>> GetByReporterIdAsync(Guid reporterId, IDbTransaction transaction)
    {
        var sql = @"SELECT * FROM Tickets 
                    WHERE ReporterId = @ReporterId";
        var result = await dbConnection.QueryAsync<Ticket>(sql, new { ReporterId = reporterId }, transaction);

        return result.ToList();
    }

    public async Task<IEnumerable<Ticket>> GetByPriorityIdAsync(Guid priorityId)
    {
        var sql = @"SELECT * FROM Tickets 
                    WHERE PriorityId = @PriorityId";
        var result = await dbConnection.QueryAsync<Ticket>(sql, new { PriorityId = priorityId });

        return result.ToList();
    }

    // ============================= COMMANDS =============================

    public async Task<Guid> CreateAsync(Ticket entity, IDbTransaction transaction)
    {
        var sql = @"INSERT INTO Tickets (Id, ProjectId, PriorityId, ReporterId, Status, Resolution, TicketType, TicketNumber, Title, Description, Version, CreatedAt)
					VALUES (@Id, @ProjectId, @PriorityId, @ReporterId, @Status, @Resolution, @TicketType, @TicketNumber, @Title, @Description, @Version, @CreatedAt)";
        entity.Id = Guid.NewGuid();
        entity.CreatedAt = DateTimeOffset.UtcNow;
        var result = await dbConnection.ExecuteAsync(sql, entity, transaction);

        if (result > 0)
            return entity.Id;
        else
            throw new Exception("Creating ticket failed");
    }

    public async Task<bool> DeleteByPriorityIdAsync(Guid priorityId, IDbTransaction transaction)
    {
        var sql = @"DELETE FROM Tickets
                    WHERE PriorityId = @PriorityId";
        var result = await dbConnection.ExecuteAsync(sql, new { PriorityId = priorityId }, transaction);

        return result > 0;
    }

    public async Task<bool> DeleteAsync(Guid id, IDbTransaction transaction)
    {
        var sql = @"DELETE FROM Tickets
                    WHERE Id = @Id";
        var result = await dbConnection.ExecuteAsync(sql, new { Id = id }, transaction);

        return result > 0;
    }

    public async Task<bool> ClearUserAssignmentAsync(Guid userId, IDbTransaction transaction)
    {
        var sql = @"UPDATE Tickets
                    SET AssigneeId = NULL
                    WHERE AssigneeId = @UserId";
        var result = await dbConnection.ExecuteAsync(sql, new { UserId = userId }, transaction);

        return result > 0;
    }

    public async Task<bool> DeleteByUserIdAsync(Guid userId, IDbTransaction transaction)
    {
        var sql = @"DELETE FROM Tickets
                    WHERE ReporterId = @UserId";
        var result = await dbConnection.ExecuteAsync(sql, new { UserId = userId }, transaction);
        return result > 0;
    }

    public async Task<bool> DeleteByProjectIdAsync(Guid projectId, IDbTransaction transaction)
    {
        var sql = @"DELETE FROM Tickets
                    WHERE ProjectId = @ProjectId";
        var result = await dbConnection.ExecuteAsync(sql, new { ProjectId = projectId }, transaction);

        return result > 0;
    }

    public async Task<bool> SoftDeleteAsync(Guid id)
    {
        var sql = @"UPDATE Tickets
					SET IsDeleted = 1
					WHERE Id = @Id";
        var result = await dbConnection.ExecuteAsync(sql, new { Id = id });

        return result > 0;
    }

    public async Task<Guid> CreateAsync(Ticket entity)
    {
        var sql = @"INSERT INTO Tickets (Id, ProjectId, PriorityId, ReporterId, Status, Resolution, TicketType, TicketNumber, Title, Description, Version, CreatedAt)
					VALUES (@Id, @ProjectId, @PriorityId, @ReporterId, @Status, @Resolution, @TicketType, @TicketNumber, @Title, @Description, @Version, @CreatedAt)";
        entity.Id = Guid.NewGuid();
        entity.CreatedAt = DateTimeOffset.UtcNow;
        var result = await dbConnection.ExecuteAsync(sql, entity);

        if (result > 0)
            return entity.Id;
        else
            throw new Exception("Creating ticket failed");
    }

    public async Task<bool> UpdateAsync(Ticket entity)
    {
        var sql = @"UPDATE Tickets
					SET PriorityId = @PriorityId,
						AssigneeId = @AssigneeId,
						Status = @Status,
						Resolution = @Resolution,
						TicketType = @TicketType,
						Title = @Title,
						Description = @Description,
						Version = @Version
					WHERE Id = @Id";
        var result = await dbConnection.ExecuteAsync(sql, entity);

        return result > 0;
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        var sql = @"DELETE FROM Tickets 
                    WHERE Id = @Id";
        var result = await dbConnection.ExecuteAsync(sql, new { Id = id });

        return result > 0;
    }
}
