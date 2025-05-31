using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using ProjectManager_01.Application.Contracts.Repositories;
using ProjectManager_01.Domain.Models;

namespace ProjectManager_01.Infrastructure.Repositories;
internal class ProjectRoleRepository : IProjectRoleRepository
{
	private readonly IDbConnection dbConnection;

	public ProjectRoleRepository(IDbConnection dbConnection)
    {
		this.dbConnection = dbConnection;
	}
    public async Task<Guid> CreateAsync(ProjectRole entity)
    {
        var sql = @"INSERT INTO ProjectRoles (Id, ProjectId, Name)
                    VALUES (@Id, @ProjectId, @Name)";
        entity.Id = Guid.NewGuid();
        var result = await dbConnection.ExecuteAsync(sql, entity);

        if (result > 0)
            return entity.Id;
        else
            throw new Exception("Creating ProjectRole failed");
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        var sql = @"DELETE FROM ProjectRoles WHERE Id = @Id";
        var result = await dbConnection.ExecuteAsync(sql, new {Id = id});

        return result > 0;
    }

	public async Task<List<ProjectRole>> GetAllAsync()
	{
        var sql = @"SELECT * FROM ProjectRoles";
        var result = await dbConnection.QueryAsync<ProjectRole>(sql);

        return result.ToList();
	}

	public async Task<ProjectRole> GetByIdAsync(Guid id)
    {
        var sql = @"SELECT * FROM ProjectRoles WHERE Id = @Id";
        var result = await dbConnection.QueryFirstAsync<ProjectRole>(sql, new {Id = id});

        return result;
    }

    public async Task<bool> UpdateAsync(ProjectRole entity)
    {
        var sql = @"UPDATE ProjectRoles
                    SET ProjectId = @ProjectId,
                        Name = @Name
                    WHERE Id = @Id";
        var result = await dbConnection.ExecuteAsync(sql, entity);

        return result > 0;
    }
}
