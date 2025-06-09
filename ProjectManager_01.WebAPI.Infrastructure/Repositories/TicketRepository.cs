using System.Data;
using Dapper;
using ProjectManager_01.Application.Contracts.Repositories;
using ProjectManager_01.Domain.Models;

namespace ProjectManager_01.Infrastructure.Repositories;

internal class TicketRepository : ITicketRepository
{
    private readonly IDbConnection _dbConnection;

    public TicketRepository(IDbConnection dbConnection)
    {
        _dbConnection = dbConnection;
    }

    // ============================= QUERIES =============================
    public async Task<IEnumerable<Ticket>> GetAllAsync()
    {
        var sql = @"SELECT t.*, p.*
                    FROM Tickets t
                    JOIN Projects p ON t.ProjectId = p.Id";

        var tickets = await _dbConnection.QueryAsync<Ticket, Project, Ticket>(
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

    public async Task<IEnumerable<Ticket>> GetAllByProjectIdAsync(Guid projectId)
    {
        var sql = @"SELECT t.*, p.*
                    FROM Tickets t
                    JOIN Projects p ON t.ProjectId = p.Id
                    WHERE t.ProjectId = @ProjectId
                    AND t.IsDeleted = 0
                    AND p.IsDeleted = 0";

        var tickets = await _dbConnection.QueryAsync<Ticket, Project, Ticket>(
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

    public async Task<IEnumerable<Ticket>> GetAllByProjectIdAsync(Guid projectId, IDbTransaction transaction)
    {
        var sql = @"SELECT t.*, p.*
                    FROM Tickets t
                    JOIN Projects p ON t.ProjectId = p.Id
                    WHERE t.ProjectId = @ProjectId
                    AND t.IsDeleted = 0
                    AND p.IsDeleted = 0";

        var tickets = await _dbConnection.QueryAsync<Ticket, Project, Ticket>(
            sql,
            (ticket, project) =>
            {
                ticket.Project = project;
                return ticket;
            },
            new { ProjectId = projectId },
            transaction,
            splitOn: "Id"
        );

        return tickets.ToList();
    }

    public async Task<Ticket> GetByIdAsync(Guid id)
    {
        var sql = @"SELECT 
                        t.*, 
                        proj.*, 
                        prio.*, 
                        a.*, 
                        r.*
                    FROM Tickets t
                    JOIN Projects proj ON t.ProjectId = proj.Id
                    JOIN Priorities prio ON t.PriorityId = prio.Id
                    LEFT JOIN Users a ON t.AssigneeId = a.Id
                    JOIN Users r ON t.ReporterId = r.Id
                    WHERE t.Id = @Id
                    AND t.IsDeleted = 0
                    AND p.IsDeleted = 0";

        var result = (await _dbConnection.QueryAsync<Ticket, Project, Priority, User, User, Ticket>(
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
            splitOn: "Id,Id,Id,Id"
        ));

        return result.FirstOrDefault();
    }

    public async Task<Ticket> GetByProjectKeyAndTicketNumberAsync(string projectKey, int ticketNumber)
    {
        var sql = @"SELECT 
                        t.*, 
                        proj.*, 
                        prio.*, 
                        a.*, 
                        r.*
                    FROM Tickets t
                    JOIN Projects proj ON t.ProjectId = proj.Id
                    JOIN Priorities prio ON t.PriorityId = prio.Id
                    LEFT JOIN Users a ON t.AssigneeId = a.Id
                    JOIN Users r ON t.ReporterId = r.Id
                    WHERE t.TicketNumber = @TicketNumber
                    AND proj.[Key] = @ProjectKey
                    AND t.IsDeleted = 0
                    AND p.IsDeleted = 0";

        var ticket = (await _dbConnection.QueryAsync<Ticket, Project, Priority, User, User, Ticket>
            (sql, (ticket, project, priority, assignee, reporter) =>
            {
                ticket.Project = project;
                ticket.Priority = priority;
                ticket.Assignee = assignee;
                ticket.Reporter = reporter;
                return ticket;
            },
        new { TicketNumber = ticketNumber, ProjectKey = projectKey },
        splitOn: "Id,Id,Id,Id"
        ));

        return ticket.FirstOrDefault();
    }

    public async Task<IEnumerable<Ticket>> GetAllByReporterIdAsync(Guid reporterId, IDbTransaction transaction)
    {
        var sql = @"SELECT * FROM Tickets 
                    WHERE ReporterId = @ReporterId";
        var result = await _dbConnection.QueryAsync<Ticket>(sql, new { ReporterId = reporterId }, transaction);

        return result.ToList();
    }

    public async Task<IEnumerable<Ticket>> GetAllByPriorityIdAsync(Guid priorityId)
    {
        var sql = @"SELECT * FROM Tickets 
                    WHERE PriorityId = @PriorityId";
        var result = await _dbConnection.QueryAsync<Ticket>(sql, new { PriorityId = priorityId });

        return result.ToList();
    }

    public async Task<IEnumerable<Ticket>> GetAllByPriorityIdAsync(Guid priorityId, IDbTransaction transaction)
    {
        var sql = @"SELECT * FROM Tickets 
                    WHERE PriorityId = @PriorityId";
        var result = await _dbConnection.QueryAsync<Ticket>(sql, new { PriorityId = priorityId }, transaction);

        return result.ToList();
    }

    // ============================= COMMANDS =============================

    public async Task<bool> CreateAsync(Ticket entity, IDbTransaction transaction)
    {
        var sql = @"INSERT INTO Tickets (Id, ProjectId, PriorityId, ReporterId, Status, Resolution, TicketType, TicketNumber, Title, Description, Version, CreatedAt)
					VALUES (@Id, @ProjectId, @PriorityId, @ReporterId, @Status, @Resolution, @TicketType, @TicketNumber, @Title, @Description, @Version, @CreatedAt)";
        entity.Id = Guid.NewGuid();
        entity.CreatedAt = DateTimeOffset.UtcNow;
        var result = await _dbConnection.ExecuteAsync(sql, entity, transaction);

        return result > 0;
    }

    public async Task<bool> DeleteAllByPriorityIdAsync(Guid priorityId, IDbTransaction transaction)
    {
        var sql = @"DELETE FROM Tickets
                    WHERE PriorityId = @PriorityId";
        var result = await _dbConnection.ExecuteAsync(sql, new { PriorityId = priorityId }, transaction);

        return result > 0;
    }

    public async Task<bool> DeleteAsync(Guid id, IDbTransaction transaction)
    {
        var sql = @"DELETE FROM Tickets
                    WHERE Id = @Id";
        var result = await _dbConnection.ExecuteAsync(sql, new { Id = id }, transaction);

        return result > 0;
    }

    public async Task<bool> ClearUserAssignmentsAsync(Guid userId, IDbTransaction transaction)
    {
        var sql = @"UPDATE Tickets
                    SET AssigneeId = NULL
                    WHERE AssigneeId = @UserId";
        var result = await _dbConnection.ExecuteAsync(sql, new { UserId = userId }, transaction);

        return result > 0;
    }

    public async Task<bool> DeleteAllByUserIdAsync(Guid userId, IDbTransaction transaction)
    {
        var sql = @"DELETE FROM Tickets
                    WHERE ReporterId = @UserId";
        var result = await _dbConnection.ExecuteAsync(sql, new { UserId = userId }, transaction);
        return result > 0;
    }

    public async Task<bool> DeleteAllByProjectIdAsync(Guid projectId, IDbTransaction transaction)
    {
        var sql = @"DELETE FROM Tickets
                    WHERE ProjectId = @ProjectId";
        var result = await _dbConnection.ExecuteAsync(sql, new { ProjectId = projectId }, transaction);

        return result > 0;
    }

    public async Task<bool> SoftDeleteByIdAsync(Guid id)
    {
        var sql = @"UPDATE Tickets
					SET IsDeleted = 1
					WHERE Id = @Id";
        var result = await _dbConnection.ExecuteAsync(sql, new { Id = id });

        return result > 0;
    }

    public async Task<bool> CreateAsync(Ticket entity)
    {
        var sql = @"INSERT INTO Tickets (Id, ProjectId, PriorityId, ReporterId, Status, Resolution, TicketType, TicketNumber, Title, Description, Version, CreatedAt)
					VALUES (@Id, @ProjectId, @PriorityId, @ReporterId, @Status, @Resolution, @TicketType, @TicketNumber, @Title, @Description, @Version, @CreatedAt)";
        entity.Id = Guid.NewGuid();
        entity.CreatedAt = DateTimeOffset.UtcNow;
        var result = await _dbConnection.ExecuteAsync(sql, entity);

        return result > 0;
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
        var result = await _dbConnection.ExecuteAsync(sql, entity);

        return result > 0;
    }

    public async Task<bool> DeleteByIdAsync(Guid id)
    {
        var sql = @"DELETE FROM Tickets 
                    WHERE Id = @Id";
        var result = await _dbConnection.ExecuteAsync(sql, new { Id = id });

        return result > 0;
    }
}
