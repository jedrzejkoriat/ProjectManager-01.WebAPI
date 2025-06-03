using System.Data;
using Dapper;
using ProjectManager_01.Application.Contracts.Repositories;
using ProjectManager_01.Domain.Enums;
using ProjectManager_01.Domain.Models;

namespace ProjectManager_01.Infrastructure.Repositories;

internal class TicketRepository : ITicketRepository
{
    private readonly IDbConnection dbConnection;

    public TicketRepository(IDbConnection dbConnection)
    {
        this.dbConnection = dbConnection;
    }

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

    public async Task<List<Ticket>> GetByReporterIdAsync(Guid reporterId, IDbTransaction transaction)
    {
        var sql = @"SELECT * FROM Tickets 
                    WHERE ReporterId = @ReporterId";
        var result = await dbConnection.QueryAsync<Ticket>(sql, new { ReporterId = reporterId }, transaction);

        return result.ToList();
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

    public async Task<Ticket> GetByKeyAndNumberWithDetailsAsync(string projectKey, int ticketNumber)
    {
        var ticketSql = @"SELECT 
                            t.*, 
                            proj.Id AS ProjectId, proj.Key, 
                            prio.Id AS PriorityId, prio.Name, 
                            a.Id AS AssigneeId, a.Username, a.Email,
                            r.Id AS ReporterId, r.Username, r.Email
                        FROM Tickets t
                        JOIN Projects proj ON t.ProjectId = proj.Id
                        JOIN Priorities prio ON t.PriorityId = prio.Id
                        LEFT JOIN Users a ON t.AssigneeId = a.Id
                        JOIN Users r ON t.ReporterId = r.Id
                        WHERE t.TicketNumber = @TicketNumber
                        AND proj.Key = @ProjectKey";

        var ticket = (await dbConnection.QueryAsync<Ticket, Project, Priority, User, User, Ticket>
            (ticketSql, (ticket, project, priority, assignee, reporter) =>
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

        if (ticket == null)
            throw new Exception("Finding ticket failed");

        ticket.Comments = (await dbConnection.QueryAsync<Comment>
            ("SELECT * FROM Comments WHERE TicketId = @TicketId", new { TicketId = ticket.Id })).ToList();

        ticket.RelationsAsSource = (await dbConnection.QueryAsync<TicketRelation>
            ("SELECT * FROM TicketRelations WHERE SourceId = @TicketId", new { TicketId = ticket.Id })).ToList();

        ticket.RelationsAsTarget = (await dbConnection.QueryAsync<TicketRelation>
            ("SELECT * FROM TicketRelations WHERE TargetId = @TicketId", new { TicketId = ticket.Id })).ToList();

        return ticket;
    }

    public async Task<bool> SoftDeleteAsync(Guid id)
    {
        var sql = @"UPDATE Tickets
					SET IsDeleted = 1
					WHERE Id = @Id";
        var result = await dbConnection.ExecuteAsync(sql, new { Id = id });

        return result > 0;
    }

    public async Task<Ticket> GetByKeyAndNumberAsync(string projectKey, int ticketNumber)
    {
        var sql = @"SELECT t.* FROM Tickets t
					JOIN Projects p ON t.ProjectId = p.Id
					WHERE t.TicketNumber = @TicketNumber
					AND p.Key = @ProjectKey
					AND p.IsDeleted = 0
					AND t.IsDeleted = 0";
        var result = await dbConnection.QueryFirstAsync<Ticket>(sql, new { TicketNumber = ticketNumber, Key = projectKey });

        return result;
    }

    public async Task<List<Ticket>> GetByPriorityIdAsync(Guid priorityId)
    {
        var sql = @"SELECT * FROM Tickets 
                    WHERE PriorityId = @PriorityId";
        var result = await dbConnection.QueryAsync<Ticket>(sql, new { PriorityId = priorityId });

        return result.ToList();
    }

    public async Task<List<Ticket>> GetByProjectIdAsync(Guid projectId)
    {
        var sql = @"SELECT * FROM Tickets 
                    WHERE ProjectId = @ProjectId";
        var result = await dbConnection.QueryAsync<Ticket>(sql, new { ProjectId = projectId });

        return result.ToList();
    }

    public async Task<List<Ticket>> GetByReporterIdAsync(Guid reporterId, IDbConnection connection, IDbTransaction transaction)
    {
        var sql = @"SELECT * FROM Tickets 
                    WHERE ReporterId = @ReporterId";
        var result = await connection.QueryAsync<Ticket>(sql, new { ReporterId = reporterId }, transaction);

        return result.ToList();
    }

    public async Task<List<Ticket>> GetByResolutionAsync(Resolution resolution)
    {
        var sql = @"SELECT * FROM Tickets 
                    WHERE Resolution = @Resolution";
        var result = await dbConnection.QueryAsync<Ticket>(sql, new { Resolution = resolution });

        return result.ToList();
    }

    public async Task<List<Ticket>> GetByStatusAsync(Status status)
    {
        var sql = @"SELECT * FROM Tickets 
                    WHERE Status = @Status";
        var result = await dbConnection.QueryAsync<Ticket>(sql, new { Status = status });

        return result.ToList();
    }

    public async Task<List<Ticket>> GetByTicketTypeAsync(TicketType ticketType)
    {
        var sql = @"SELECT * FROM Tickets 
                    WHERE TicketType = @TicketType";
        var result = await dbConnection.QueryAsync<Ticket>(sql, new { TicketType = ticketType });

        return result.ToList();
    }

    public async Task<List<Ticket>> GetByAssigneeIdAsync(Guid? assigneeId)
    {
        var sql = @"SELECT * FROM Tickets 
                    WHERE AssigneeId = @AssigneeId";
        var result = await dbConnection.QueryAsync<Ticket>(sql, new { AssigneeId = assigneeId });

        return result.ToList();
    }

    // ============================= CRUD =============================
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

    public async Task<List<Ticket>> GetAllAsync()
    {
        var sql = @"SELECT * FROM Tickets";
        var result = await dbConnection.QueryAsync<Ticket>(sql);

        return result.ToList();
    }

    public async Task<Ticket> GetByIdAsync(Guid id)
    {
        var sql = @"SELECT * FROM Tickets 
                    WHERE Id = @Id";
        var result = await dbConnection.QueryFirstAsync<Ticket>(sql, new { Id = id });

        return result;
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
