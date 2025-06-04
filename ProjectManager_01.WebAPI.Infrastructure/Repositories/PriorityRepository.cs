using System.Data;
using Dapper;
using ProjectManager_01.Application.Contracts.Repositories;
using ProjectManager_01.Domain.Models;

namespace ProjectManager_01.Infrastructure.Repositories;

internal class PriorityRepository : IPriorityRepository
{
    private readonly IDbConnection _dbConnection;

    public PriorityRepository(IDbConnection dbConnection)
    {
        _dbConnection = dbConnection;
    }

    // ============================= QUERIES =============================
    public async Task<IEnumerable<Priority>> GetAllAsync()
    {
        var sql = @"SELECT * FROM Priorities";
        var result = await _dbConnection.QueryAsync<Priority>(sql);

        return result.ToList();
    }

    public async Task<Priority> GetByIdAsync(Guid id)
    {
        var sql = @"SELECT * FROM Priorities 
                    WHERE Id = @Id";
        var result = await _dbConnection.QueryFirstAsync(sql, new { Id = id });

        return result;
    }

    // ============================= COMMANDS =============================
    public async Task<Guid> CreateAsync(Priority entity)
    {
        var sql = @"INSERT INTO Priorities (Id, Name) 
                    VALUES (@Id, @Name)";
        entity.Id = Guid.NewGuid();
        var result = await _dbConnection.ExecuteAsync(sql, entity);

        if (result > 0)
            return entity.Id;
        else
            throw new Exception("Creating new Priority failed.");
    }

    public async Task<bool> UpdateAsync(Priority entity)
    {
        var sql = @"UPDATE Priorities 
                    SET Name = @Name 
                    WHERE Id = @Id";
        var result = await _dbConnection.ExecuteAsync(sql, entity);

        return result > 0;
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        var sql = @"DELETE FROM Priorities 
                    WHERE Id = @Id";
        var result = await _dbConnection.ExecuteAsync(sql, new { Id = id });

        return result > 0;
    }

    public async Task<bool> DeleteAsync(Guid id, IDbTransaction transaction)
    {
        var sql = @"DELETE FROM Priorities 
                    WHERE Id = @Id";
        var result = await _dbConnection.ExecuteAsync(sql, new { Id = id }, transaction);

        return result > 0;
    }
}
