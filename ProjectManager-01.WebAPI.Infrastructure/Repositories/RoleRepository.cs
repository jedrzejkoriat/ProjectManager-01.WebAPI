using Dapper;
using ProjectManager_01.Application.Contracts.Repositories;
using ProjectManager_01.Domain.Models;
using System.Data;

namespace ProjectManager_01.Infrastructure.Repositories;
internal class RoleRepository : IRoleRepository
{
	private readonly IDbConnection dbConnection;

	public RoleRepository(IDbConnection dbConnection)
	{
		this.dbConnection = dbConnection;
	}

	public async Task<Guid> CreateAsync(Role entity)
	{
		var sql = @"INSERT INTO Roles (Id, Name)
					VALUES (@Id, @Name)";
		entity.Id = Guid.NewGuid();
		var result = await dbConnection.ExecuteAsync(sql, entity);

		if (result > 0)
			return entity.Id;
		else
			throw new Exception("Creating new role failed.");
	}

	public async Task<bool> DeleteAsync(Guid id)
	{
		var sql = @"DELETE FROM Roles WHERE Id = @Id";
		var result = await dbConnection.ExecuteAsync(sql, new {Id = id});

		return result > 0;
	}

	public async Task<List<Role>> GetAllAsync()
	{
		var sql = @"SELECT * FROM Roles";
		var result = await dbConnection.QueryAsync<Role>(sql);

		return result.ToList();
	}

	public async Task<Role> GetByIdAsync(Guid id)
	{
		var sql = @"SELECT * FROM Roles WHERE Id = @Id";
		var result = await dbConnection.QueryFirstAsync<Role>(sql);

		return result;
	}

	public async Task<bool> UpdateAsync(Role entity)
	{
		var sql = @"UPDATE Roles
					SET Name = @Name
					WHERE Id = @Id";
		var result = await dbConnection.ExecuteAsync(sql, entity);

		return result > 0;
	}
}
