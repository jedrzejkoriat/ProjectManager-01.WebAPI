using System.Data;
using Dapper;
using ProjectManager_01.Application.Contracts.Repositories;
using ProjectManager_01.Domain.Models;

namespace ProjectManager_01.Infrastructure.Repositories;

internal class TagRepository : ITagRepository
{

    private readonly IDbConnection dbConnection;

    public TagRepository(IDbConnection dbConnection)
    {
        this.dbConnection = dbConnection;
    }

    public async Task<List<Tag>> GetByProjectIdAsync(Guid projectId)
    {
        var sql = @"SELECT * FROM Tags 
                    WHERE ProjectId = @ProjectId";
        var result = await dbConnection.QueryAsync<Tag>(sql, new { ProjectId = projectId });

        return result.ToList();
    }

    // ============================= CRUD =============================
    public async Task<Guid> CreateAsync(Tag entity)
    {
        var sql = @"INSERT INTO Tags (Id, Name, ProjectId)
					VALUES (@Id, @Name, @ProjectId)";
        entity.Id = Guid.NewGuid();
        var result = await dbConnection.ExecuteAsync(sql, entity);

        if (result > 0)
            return entity.Id;
        else
            throw new Exception("Creating tag failed.");
    }

    public async Task<List<Tag>> GetAllAsync()
    {
        var sql = @"SELECT * FROM Tags";
        var result = await dbConnection.QueryAsync<Tag>(sql);

        return result.ToList();
    }

    public async Task<Tag> GetByIdAsync(Guid id)
    {
        var sql = @"SELECT * FROM Tags 
                    WHERE Id = @Id";
        var result = await dbConnection.QueryFirstAsync<Tag>(sql, new { Id = id });

        return result;
    }

    public async Task<bool> UpdateAsync(Tag entity)
    {
        var sql = @"UPDATE Tags
					SET Name = @Name,
						ProjectId = @ProjectId
					WHERE Id = @Id";
        var result = await dbConnection.ExecuteAsync(sql, entity);

        return result > 0;
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        var sql = @"DELETE FROM Tags 
                    WHERE Id = @Id";
        var result = await dbConnection.ExecuteAsync(sql, new { Id = id });

        return result > 0;
    }
}
