using System.Data;
using Dapper;
using ProjectManager_01.Application.Contracts.Repositories;
using ProjectManager_01.Domain.Models;

namespace ProjectManager_01.Infrastructure.Repositories;

internal class ProjectUserRoleRepository : IProjectUserRoleRepository
{
    private readonly IDbConnection _dbConnection;

    public ProjectUserRoleRepository(IDbConnection dbConnection)
    {
        _dbConnection = dbConnection;
    }

    // ============================= QUERIES =============================
    public async Task<IEnumerable<ProjectUserRole>> GetAllAsync()
    {
        var sql = @"SELECT * FROM ProjectUserRoles";
        var result = await _dbConnection.QueryAsync<ProjectUserRole>(sql);

        return result.ToList();
    }

    public async Task<ProjectUserRole> GetByIdAsync(Guid id)
    {
        var sql = @"SELECT * FROM ProjectUserRoles 
                    WHERE Id = @Id";
        var result = await _dbConnection.QueryFirstAsync<ProjectUserRole>(sql);

        return result;
    }

    // ============================= COMMANDS =============================
    public async Task<bool> DeleteByUserIdAsync(Guid userId, IDbTransaction transaction)
    {
        var sql = @"DELETE FROM ProjectUserRoles 
                    WHERE UserId = @UserId";
        var result = await _dbConnection.ExecuteAsync(sql, new { UserId = userId }, transaction);

        return result > 0;
    }

    public async Task<bool> DeleteByProjectIdAsync(Guid projectId, IDbTransaction transaction)
    {
        var sql = @"DELETE FROM ProjectUserRoles 
                    WHERE ProjectId = @ProjectId";
        var result = await _dbConnection.ExecuteAsync(sql, new { ProjectId = projectId }, transaction);

        return result > 0;
    }

    public async Task<bool> DeleteByProjectRoleIdAsync(Guid projectRoleId, IDbTransaction transaction)
    {
        var sql = @"DELETE FROM ProjectUserRoles 
                    WHERE ProjectRoleId = @ProjectRoleId";
        var result = await _dbConnection.ExecuteAsync(sql, new { ProjectRoleId = projectRoleId }, transaction);

        return result > 0;
    }

    public async Task<Guid> CreateAsync(ProjectUserRole entity)
    {
        var sql = @"INSERT INTO ProjectUserRoles (Id, ProjectId, ProjectRoleId, UserId)
					VALUES (@Id, @ProjectId, @ProjectRoleId, @UserId)";
        entity.Id = Guid.NewGuid();
        var result = await _dbConnection.ExecuteAsync(sql, entity);

        if (result > 0)
            return entity.Id;
        else
            throw new Exception("Creating ProjectUserRoles failed.");
    }

    public async Task<bool> UpdateAsync(ProjectUserRole entity)
    {
        var sql = @"UPDATE ProjectUserRoles
					SET ProjectRoleId = @ProjectRoleId,
						ProjectId = @ProjectId,
						UserId = @UserId
					WHERE Id = @Id";
        var result = await _dbConnection.ExecuteAsync(sql, entity);

        return result > 0;
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        var sql = @"DELETE FROM ProjectUserRoles WHERE Id = @Id";
        var result = await _dbConnection.ExecuteAsync(sql, new { Id = id });

        return result > 0;
    }
}