using System.Data;
using Dapper;
using ProjectManager_01.Application.Contracts.Repositories;
using ProjectManager_01.Domain.Models;

namespace ProjectManager_01.Infrastructure.Repositories;

internal class TicketRelationRepository : ITicketRelationRepository
{

    private readonly IDbConnection dbConnection;

    public TicketRelationRepository(IDbConnection dbConnection)
    {
        this.dbConnection = dbConnection;
    }

    public async Task<List<TicketRelation>> GetBySourceIdAsync(Guid sourceId)
    {
        var sql = @"SELECT * FROM TicketRelations 
                    WHERE SourceId = @SourceId";
        var result = await dbConnection.QueryAsync<TicketRelation>(sql, new { SourceId = sourceId });

        return result.ToList();
    }

    public async Task<List<TicketRelation>> GetByTargetIdAsync(Guid targetId)
    {
        var sql = @"SELECT * FROM TicketRelations 
                    WHERE TargetId = @TargetId";
        var result = await dbConnection.QueryAsync<TicketRelation>(sql, new { TargetId = targetId });

        return result.ToList();
    }

    // ============================= CRUD =============================
    public async Task<Guid> CreateAsync(TicketRelation entity)
    {
        var sql = @"INSERT INTO TicketRelations (Id, SourceId, TargetId, RelationType)
					VALUES (@Id, @SourceId, @TargetId, @RelationType)";
        entity.Id = Guid.NewGuid();
        var result = await dbConnection.ExecuteAsync(sql, entity);

        if (result > 0)
            return entity.Id;
        else
            throw new Exception("Creating TicketRelation failed.");
    }

    public async Task<List<TicketRelation>> GetAllAsync()
    {
        var sql = @"SELECT * FROM TicketRelations";
        var result = await dbConnection.QueryAsync<TicketRelation>(sql);

        return result.ToList();
    }

    public async Task<TicketRelation> GetByIdAsync(Guid id)
    {
        var sql = @"SELECT * FROM TicketRelations 
                    WHERE Id = @Id";
        var result = await dbConnection.QueryFirstAsync<TicketRelation>(sql, new { Id = id });

        return result;
    }

    public async Task<bool> UpdateAsync(TicketRelation entity)
    {
        var sql = @"UPDATE TicketRelations
					SET SourceId = @SourceId,
						TargetId = @TargetId,
						RelationType = @RelationType
					WHERE Id = @Id";
        var result = await dbConnection.ExecuteAsync(sql, entity);

        return result > 0;
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        var sql = @"DELETE FROM TicketRelations 
                    WHERE Id = @Id";
        var result = await dbConnection.ExecuteAsync(sql, new { Id = id });

        return result > 0;
    }
}
