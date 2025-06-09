using System.Data;
using Dapper;
using ProjectManager_01.Application.Contracts.Repositories;
using ProjectManager_01.Domain.Models;

namespace ProjectManager_01.Infrastructure.Repositories;

internal class ProjectRepository : IProjectRepository
{
    private readonly IDbConnection _dbConnection;

    public ProjectRepository(IDbConnection dbConnection)
    {
        _dbConnection = dbConnection;
    }

    // ============================= QUERIES =============================
    public async Task<IEnumerable<Project>> GetAllAsync()
    {
        var sql = @"SELECT * FROM Projects";
        var result = await _dbConnection.QueryAsync<Project>(sql);

        return result.ToList();
    }

    public async Task<Project> GetByIdAsync(Guid id)
    {
        var sql = @"SELECT * FROM Projects 
                        WHERE Id = @Id";
        var result = await _dbConnection.QueryFirstOrDefaultAsync<Project>(sql, new { Id = id });

        return result;
    }

    public async Task<IEnumerable<Project>> GetAllByUserIdAsync(Guid userId)
    {
        var sql = @"SELECT DISTINCT p.*
                    FROM Projects p
                    JOIN ProjectUserRole pur ON pur.ProjectId = p.Id
                    WHERE pur.UserId = @UserId";
        var result = await _dbConnection.QueryAsync<Project>(sql, new { UserId = userId });

        return result.ToList();
    }

    // ============================= COMMANDS =============================
    public async Task<bool> SoftDeleteByIdAsync(Guid id)
    {
        var sql = @"UPDATE Projects
                        SET IsDeleted = 1
                        WHERE Id = @Id";
        var result = await _dbConnection.ExecuteAsync(sql, new { Id = id });

        return result > 0;
    }

    public async Task<bool> DeleteByIdAsync(Guid id, IDbTransaction transaction)
    {
        var sql = @"DELETE FROM Projects 
                        WHERE Id = @Id";
        var result = await _dbConnection.ExecuteAsync(sql, new { Id = id }, transaction);

        return result > 0;
    }

    public async Task<bool> CreateAsync(Project project)
    {
        var sql = @"INSERT INTO Projects(Id, Name, [Key], CreatedAt, IsDeleted)
                    VALUES (@Id, @Name, @Key, @CreatedAt, @IsDeleted)";
        var result = await _dbConnection.ExecuteAsync(sql, project);

        return result > 0;
    }

    public async Task<bool> UpdateAsync(Project project)
    {
        var sql = @"UPDATE Projects
                    SET Name = @Name,
                        [Key] = @Key
                    WHERE Id = @Id";
        var result = await _dbConnection.ExecuteAsync(sql, project);

        return result > 0;
    }
    public async Task<bool> DeleteByIdAsync(Guid id)
    {
        var sql = @"DELETE FROM Projects 
                        WHERE Id = @Id";
        var result = await _dbConnection.ExecuteAsync(sql, new { Id = id });

        return result > 0;
    }
}
