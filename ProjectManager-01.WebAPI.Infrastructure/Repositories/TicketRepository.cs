using Dapper;
using ProjectManager_01.Application.Contracts.Repositories;
using ProjectManager_01.Domain.Enums;
using ProjectManager_01.Domain.Models;
using System.Data;

namespace ProjectManager_01.Infrastructure.Repositories;
internal class TicketRepository : ITicketRepository
{
	private readonly IDbConnection dbConnection;

	public TicketRepository(IDbConnection dbConnection)
	{
		this.dbConnection = dbConnection;
	}

	public async Task<Guid> CreateAsync(Ticket entity)
	{
		var sql = @"INSERT INTO Tickets 
					(Id, ProjectId, PriorityId, ReporterId, Status, Resolution, TicketType, TicketNumber, Title, Description, Version, CreatedAt)
					VALUES
					(@Id, @ProjectId, @PriorityId, @ReportedId, @Status, @Reolustion, @TicketType, @TicketNumber, @Title, @Description, @Version, @CreatedAt)";
		entity.Id = Guid.NewGuid();
		entity.CreatedAt = DateTime.Now;
		var result = await dbConnection.ExecuteAsync(sql, entity);

		if (result > 0)
			return entity.Id;
		else
			throw new Exception("Creating ticket failed");
	}

	public async Task<bool> DeleteAsync(Guid id)
	{
		var sql = @"DELETE FROM Tickets WHERE Id = @Id";
		var result = await dbConnection.ExecuteAsync(sql, new {Id = id});

		return result > 0;
	}

	public async Task<bool> SoftDeleteAsync(Guid id)
	{
		var sql = @"UPDATE Tickets
					SET IsDeleted = 1
					WHERE Id = @Id";
		var result = await dbConnection.ExecuteAsync(sql, new {Id = id});

		return result > 0;
	}

	public async Task<bool> UpdateAsync(Ticket entity)
	{
		var sql = @"UPDATE Tickets
					SET PriorityId = @PriorityId,
						AssigneeId = @AssigneeId,
						Status = @Status,
						Resolution = @Resolution,
						TicketType = @TicketType,
						Title = @Title,
						Description = @Description,
						Version = @Version
					WHERE Id = @Id";
		var result = await dbConnection.ExecuteAsync(sql, entity);

		return result > 0;
	}

	public async Task<List<Ticket>> GetAllAsync()
	{
		var sql = @"SELECT * FROM Tickets";
		var result = await dbConnection.QueryAsync<Ticket>(sql);

		return result.ToList();
	}

	public async Task<List<Ticket>> GetByAssigneeIdAsync(Guid? assigneeId)
	{
		var sql = @"SELECT * FROM Tickets WHERE AssigneeId = @AssigneeId";
		var result = await dbConnection.QueryAsync<Ticket>(sql, new {AssigneeId = assigneeId});

		return result.ToList();
	}

	public async Task<Ticket> GetByIdAsync(Guid id)
	{
		var sql = @"SELECT * FROM Tickets WHERE Id = @Id";
		var result = await dbConnection.QueryFirstAsync<Ticket>(sql, new {Id = id});

		return result;
	}

	public async Task<Ticket> GetByKeyAndNumberAsync(string projectKey, int ticketNumber)
	{
		var sql = @"SELECT t.* FROM Tickets t
					JOIN Projects p ON t.ProjectId = p.Id
					WHERE t.TicketNumber = @TicketNumber
						AND p.Key = @ProjectKey
						AND p.IsDeleted = 0
						AND t.IsDeleted = 0";
		var result = await dbConnection.QueryFirstAsync<Ticket>(sql, new {TicketNumber = ticketNumber, Key = projectKey});

		return result;
	}

	public async Task<List<Ticket>> GetByPriorityAsync(Guid priorityId)
	{
		var sql = @"SELECT * FROM Tickets WHERE PriorityId = @PriorityId";
		var result = await dbConnection.QueryAsync<Ticket>(sql, new {PriorityId = priorityId});

		return result.ToList();
	}

	public async Task<List<Ticket>> GetByProjectIdAsync(Guid projectId)
	{
		var sql = @"SELECT * FROM Tickets WHERE ProjectId = @ProjectId";
		var result = await dbConnection.QueryAsync<Ticket>(sql, new {ProjectId = projectId});

		return result.ToList();
	}

	public async Task<List<Ticket>> GetByReporterIdAsync(Guid reporterId)
	{
		var sql = @"SELECT * FROM Tickets WHERE ReporterId = @ReporterId";
		var result = await dbConnection.QueryAsync<Ticket>(sql, new {ReporterId = reporterId});

		return result.ToList();
	}

	public async Task<List<Ticket>> GetByResolutionAsync(Resolution resolution)
	{
		var sql = @"SELECT * FROM Tickets WHERE Resolution = @Resolution";
		var result = await dbConnection.QueryAsync<Ticket>(sql, new {Resolution = resolution});

		return result.ToList();
	}

	public async Task<List<Ticket>> GetByStatusAsync(Status status)
	{
		var sql = @"SELECT * FROM Tickets WHERE Status = @Status";
		var result = await dbConnection.QueryAsync<Ticket>(sql, new {Status = status});

		return result.ToList();
	}

	public async Task<List<Ticket>> GetByTicketTypeAsync(TicketType ticketType)
	{
		var sql = @"SELECT * FROM Tickets WHERE TicketType = @TicketType";
		var result = await dbConnection.QueryAsync<Ticket>(sql, new {TicketType = ticketType});

		return result.ToList();
	}
}
