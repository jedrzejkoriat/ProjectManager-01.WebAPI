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
		throw new NotImplementedException();
	}

	public Task<bool> SoftDeleteAsync(Guid id)
	{
		throw new NotImplementedException();
	}

	public Task<bool> UpdateAsync(Ticket entity)
	{
		throw new NotImplementedException();
	}

	public Task<List<Ticket>> GetAllAsync()
	{
		throw new NotImplementedException();
	}

	public Task<List<Ticket>> GetByAssigneeIdAsync(Guid? assigneeId)
	{
		throw new NotImplementedException();
	}

	public Task<Ticket> GetByIdAsync(Guid id)
	{
		throw new NotImplementedException();
	}

	public Task<Ticket> GetByKeyAndNumberAsync(string key, int ticketNumber)
	{
		throw new NotImplementedException();
	}

	public Task<List<Ticket>> GetByPriorityAsync(Guid priorityId)
	{
		throw new NotImplementedException();
	}

	public Task<List<Ticket>> GetByProjectIdAsync(Guid projectId)
	{
		throw new NotImplementedException();
	}

	public Task<List<Ticket>> GetByReporterIdAsync(Guid reporterId)
	{
		throw new NotImplementedException();
	}

	public Task<List<Ticket>> GetByResolutionAsync(Resolution resolution)
	{
		throw new NotImplementedException();
	}

	public Task<List<Ticket>> GetByStatusAsync(Status status)
	{
		throw new NotImplementedException();
	}

	public Task<List<Ticket>> GetByTicketTypeAsync(TicketType ticketType)
	{
		throw new NotImplementedException();
	}
}
