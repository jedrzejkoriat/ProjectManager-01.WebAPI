using System.Data;
using Dapper;
using ProjectManager_01.Application.Contracts.Repositories;
using ProjectManager_01.Domain.Models;

namespace ProjectManager_01.Infrastructure.Repositories;

internal class TicketRelationRepository : ITicketRelationRepository
{
    private readonly IDbConnection _dbConnection;

    public TicketRelationRepository(IDbConnection dbConnection)
    {
        _dbConnection = dbConnection;
    }

    // ============================= QUERIES =============================
    public async Task<IEnumerable<TicketRelation>> GetAllAsync()
    {
        var sql = @"SELECT tr.*, 
                       s.*, sp.*, 
                       t.*, tp.*
                    FROM TicketRelations tr
                    JOIN Tickets s ON tr.SourceId = s.Id
                    JOIN Projects sp ON s.ProjectId = sp.Id
                    JOIN Tickets t ON tr.TargetId = t.Id
                    JOIN Projects tp ON t.ProjectId = tp.Id";

        var relationDict = new Dictionary<Guid, TicketRelation>();

        var result = await _dbConnection.QueryAsync<TicketRelation, Ticket, Project, Ticket, Project, TicketRelation>(
            sql,
            (relation, source, sourceProject, target, targetProject) =>
            {
                if (!relationDict.TryGetValue(relation.Id, out var r))
                {
                    r = relation;
                    r.Source = source;
                    r.Target = target;
                    relationDict[r.Id] = r;
                }

                r.Source.Project = sourceProject;
                r.Target.Project = targetProject;

                return r;
            },
            splitOn: "Id,Id,Id,Id"
        );

        return relationDict.Values.ToList();
    }

    public async Task<TicketRelation> GetByIdAsync(Guid id)
    {
        var sql = @"SELECT tr.*, 
                       s.*, sp.*, 
                       t.*, tp.*
                    FROM TicketRelations tr
                    JOIN Tickets s ON tr.SourceId = s.Id
                    JOIN Projects sp ON s.ProjectId = sp.Id
                    JOIN Tickets t ON tr.TargetId = t.Id
                    JOIN Projects tp ON t.ProjectId = tp.Id
                    WHERE tr.Id = @Id";

        TicketRelation relation = null;

        await _dbConnection.QueryAsync<TicketRelation, Ticket, Project, Ticket, Project, TicketRelation>(
            sql,
            (rel, source, sourceProject, target, targetProject) =>
            {
                source.Project = sourceProject;
                target.Project = targetProject;

                rel.Source = source;
                rel.Target = target;

                relation = rel;
                return rel;
            },
            new { Id = id },
            splitOn: "Id,Id,Id,Id"
        );

        return relation;
    }

    public async Task<IEnumerable<TicketRelation>> GetBySourceIdAsync(Guid ticketId)
    {
        var sql = @"SELECT tr.*, 
                       s.*, sp.*, 
                       t.*, tp.*
                    FROM TicketRelations tr
                    JOIN Tickets s ON tr.SourceId = s.Id
                    JOIN Projects sp ON s.ProjectId = sp.Id
                    JOIN Tickets t ON tr.TargetId = t.Id
                    JOIN Projects tp ON t.ProjectId = tp.Id
                    WHERE tr.SourceId = @TicketId";

        var relationDict = new Dictionary<Guid, TicketRelation>();

        var result = await _dbConnection.QueryAsync<TicketRelation, Ticket, Project, Ticket, Project, TicketRelation>(
            sql,
            (relation, source, sourceProject, target, targetProject) =>
            {
                if (!relationDict.TryGetValue(relation.Id, out var r))
                {
                    r = relation;
                    r.Source = source;
                    r.Target = target;
                    relationDict[r.Id] = r;
                }

                r.Source.Project = sourceProject;
                r.Target.Project = targetProject;

                return r;
            },
            new { TicketId = ticketId },
            splitOn: "Id,Id,Id,Id"
        );

        return relationDict.Values.ToList();
    }

    public async Task<IEnumerable<TicketRelation>> GetByTargetIdAsync(Guid ticketId)
    {
        var sql = @"SELECT tr.*, 
                       s.*, sp.*, 
                       t.*, tp.*
                    FROM TicketRelations tr
                    JOIN Tickets s ON tr.SourceId = s.Id
                    JOIN Projects sp ON s.ProjectId = sp.Id
                    JOIN Tickets t ON tr.TargetId = t.Id
                    JOIN Projects tp ON t.ProjectId = tp.Id
                    WHERE tr.TargetId = @TicketId";

        var relationDict = new Dictionary<Guid, TicketRelation>();

        var result = await _dbConnection.QueryAsync<TicketRelation, Ticket, Project, Ticket, Project, TicketRelation>(
            sql,
            (relation, source, sourceProject, target, targetProject) =>
            {
                if (!relationDict.TryGetValue(relation.Id, out var r))
                {
                    r = relation;
                    r.Source = source;
                    r.Target = target;
                    relationDict[r.Id] = r;
                }

                r.Source.Project = sourceProject;
                r.Target.Project = targetProject;

                return r;
            },
            new { TicketId = ticketId },
            splitOn: "Id,Id,Id,Id"
        );

        return relationDict.Values.ToList();
    }

    // ============================= COMMANDS =============================
    public async Task<bool> DeleteByTicketIdAsync(Guid ticketId, IDbTransaction transaction)
    {
        var sql = @"DELETE FROM TicketRelations 
                    WHERE SourceId = @TicketId OR TargetId = @TicketId";
        var result = await _dbConnection.ExecuteAsync(sql, new { TicketId = ticketId }, transaction);

        return result > 0;
    }

    public async Task<Guid> CreateAsync(TicketRelation entity)
    {
        var sql = @"INSERT INTO TicketRelations (Id, SourceId, TargetId, RelationType)
					VALUES (@Id, @SourceId, @TargetId, @RelationType)";
        entity.Id = Guid.NewGuid();
        var result = await _dbConnection.ExecuteAsync(sql, entity);

        if (result > 0)
            return entity.Id;
        else
            throw new Exception("Creating TicketRelation failed.");
    }

    public async Task<bool> UpdateAsync(TicketRelation entity)
    {
        var sql = @"UPDATE TicketRelations
					SET SourceId = @SourceId,
						TargetId = @TargetId,
						RelationType = @RelationType
					WHERE Id = @Id";
        var result = await _dbConnection.ExecuteAsync(sql, entity);

        return result > 0;
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        var sql = @"DELETE FROM TicketRelations 
                    WHERE Id = @Id";
        var result = await _dbConnection.ExecuteAsync(sql, new { Id = id });

        return result > 0;
    }
}
