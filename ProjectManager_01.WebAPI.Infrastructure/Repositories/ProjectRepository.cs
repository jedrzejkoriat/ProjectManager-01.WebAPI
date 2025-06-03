using System.Data;
using Dapper;
using ProjectManager_01.Application.Contracts.Repositories;
using ProjectManager_01.Domain.Models;

namespace ProjectManager_01.Infrastructure.Repositories;

internal class ProjectRepository : IProjectRepository
{

    private readonly IDbConnection dbConnection;

    public ProjectRepository(IDbConnection dbConnection)
    {
        this.dbConnection = dbConnection;
    }

    public async Task<bool> SoftDeleteAsync(Guid id)
    {
        var sql = @"UPDATE Projects
                        SET IsDeleted = 1
                        WHERE Id = @Id";
        var result = await dbConnection.ExecuteAsync(sql, new { Id = id });

        return result > 0;
    }

    public async Task<bool> DeleteAsync(Guid id, IDbConnection connection, IDbTransaction transaction)
    {
        var sql = @"DELETE FROM Projects 
                        WHERE Id = @Id";
        var result = await connection.ExecuteAsync(sql, new { Id = id }, transaction);

        return result > 0;
    }

    // ============================= CRUD =============================
    public async Task<Guid> CreateAsync(Project project)
    {
        var sql = @"INSERT INTO Projects(Id, Name, [Key], CreatedAt, IsDeleted)
                    VALUES (@Id, @Name, @Key, @CreatedAt, @IsDeleted)";
        project.Id = Guid.NewGuid();
        project.CreatedAt = DateTimeOffset.UtcNow;
        var result = await dbConnection.ExecuteAsync(sql, project);

        if (result > 0)
            return project.Id;
        else
            throw new Exception("Insert to projects table failed.");
    }

    public async Task<List<Project>> GetAllAsync()
    {
        var sql = @"SELECT * FROM Projects";
        var result = await dbConnection.QueryAsync<Project>(sql);

        return result.ToList();
    }

    public async Task<Project> GetByIdAsync(Guid id)
    {
        var sql = @"SELECT * FROM Projects 
                        WHERE Id = @Id";
        var result = await dbConnection.QueryFirstAsync<Project>(sql, new { Id = id });

        return result;
    }

    public async Task<bool> UpdateAsync(Project project)
    {
        var sql = @"UPDATE Projects
                    SET Name = @Name,
                        Key = @Key
                    WHERE Id = @Id";
        var result = await dbConnection.ExecuteAsync(sql, project);

        return result > 0;
    }
    public async Task<bool> DeleteAsync(Guid id)
    {
        var sql = @"DELETE FROM Projects 
                        WHERE Id = @Id";
        var result = await dbConnection.ExecuteAsync(sql, new { Id = id });

        return result > 0;
    }
}
