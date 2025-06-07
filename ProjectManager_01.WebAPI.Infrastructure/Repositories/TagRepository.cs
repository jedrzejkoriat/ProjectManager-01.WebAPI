using System.Data;
using Dapper;
using ProjectManager_01.Application.Contracts.Repositories;
using ProjectManager_01.Domain.Models;

namespace ProjectManager_01.Infrastructure.Repositories;

internal class TagRepository : ITagRepository
{
    private readonly IDbConnection _dbConnection;

    public TagRepository(IDbConnection dbConnection)
    {
        _dbConnection = dbConnection;
    }

    // ============================= QUERIES =============================
    public async Task<IEnumerable<Tag>> GetAllAsync()
    {
        var sql = @"SELECT * FROM Tags";
        var result = await _dbConnection.QueryAsync<Tag>(sql);

        return result.ToList();
    }

    public async Task<Tag> GetByIdAsync(Guid id)
    {
        var sql = @"SELECT * FROM Tags 
                    WHERE Id = @Id";
        var result = await _dbConnection.QueryFirstAsync<Tag>(sql, new { Id = id });

        return result;
    }
    public async Task<IEnumerable<Tag>> GetAllByProjectIdAsync(Guid projectId)
    {
        var sql = @"SELECT * FROM Tags 
                    WHERE ProjectId = @ProjectId";
        var result = await _dbConnection.QueryAsync<Tag>(sql, new { ProjectId = projectId });

        return result.ToList();
    }

    public async Task<IEnumerable<Tag>> GetAllByTicketIdAsync(Guid ticketId)
    {
        var sql = @"SELECT t.*
                    FROM Tags t
                    INNER JOIN TicketTags tt ON t.Id = tt.TagId
                    WHERE tt.TicketId = @TicketId";
        var result = await _dbConnection.QueryAsync<Tag>(sql, new { TicketId = ticketId });

        return result.ToList();
    }

    // ============================= COMMANDS =============================
    public async Task<Guid> CreateAsync(Tag entity)
    {
        var sql = @"INSERT INTO Tags (Id, Name, ProjectId)
					VALUES (@Id, @Name, @ProjectId)";
        entity.Id = Guid.NewGuid();
        var result = await _dbConnection.ExecuteAsync(sql, entity);

        if (result > 0)
            return entity.Id;
        else
            throw new Exception("Creating tag failed.");
    }

    public async Task<bool> UpdateAsync(Tag entity)
    {
        var sql = @"UPDATE Tags
					SET Name = @Name,
						ProjectId = @ProjectId
					WHERE Id = @Id";
        var result = await _dbConnection.ExecuteAsync(sql, entity);

        return result > 0;
    }

    public async Task<bool> DeleteByIdAsync(Guid id)
    {
        var sql = @"DELETE FROM Tags 
                    WHERE Id = @Id";
        var result = await _dbConnection.ExecuteAsync(sql, new { Id = id });

        return result > 0;
    }
}
