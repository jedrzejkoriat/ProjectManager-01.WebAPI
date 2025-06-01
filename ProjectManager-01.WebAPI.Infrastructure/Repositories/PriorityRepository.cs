using System.Data;
using Dapper;
using ProjectManager_01.Application.Contracts.Repositories;
using ProjectManager_01.Domain.Models;

namespace ProjectManager_01.Infrastructure.Repositories;
internal class PriorityRepository : IPriorityRepository
{
    private readonly IDbConnection dbConnection;

    public PriorityRepository(IDbConnection dbConnection)
    {
        this.dbConnection = dbConnection;
    }

    // ============================= CRUD =============================
    public async Task<Guid> CreateAsync(Priority entity)
    {
        var sql = @"INSERT INTO Priorities (Id, Name) VALUES (@Id, @Name)";
        entity.Id = Guid.NewGuid();
        var result = await dbConnection.ExecuteAsync(sql, entity);

        if (result > 0)
            return entity.Id;
        else
            throw new Exception("Creating new Priority failed.");
    }

    public async Task<List<Priority>> GetAllAsync()
    {
        var sql = @"SELECT * FROM Priorities";
        var result = await dbConnection.QueryAsync<Priority>(sql);

        return result.ToList();
    }

    public async Task<Priority> GetByIdAsync(Guid id)
    {
        var sql = @"SELECT * FROM Priorities WHERE Id = @Id";
        var result = await dbConnection.QueryFirstAsync(sql, new { Id = id });

        return result;
    }

    public async Task<bool> UpdateAsync(Priority entity)
    {
        var sql = @"UPDATE Priorities SET Name = @Name WHERE Id = @Id";
        var result = await dbConnection.ExecuteAsync(sql, entity);

        return result > 0;
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        var sql = @"DELETE FROM Priorities WHERE Id = @Id";
        var result = await dbConnection.ExecuteAsync(sql, new { Id = id });

        return result > 0;
    }
}
