using System.Data;
using System.Net.WebSockets;
using Dapper;
using ProjectManager_01.Application.Contracts.Repositories;
using ProjectManager_01.Domain.Models;

namespace ProjectManager_01.Infrastructure.Repositories;
internal class TicketTagRepository : ITicketTagRepository
{
    private readonly IDbConnection dbConnection;

    public TicketTagRepository(IDbConnection dbConnection)
    {
        this.dbConnection = dbConnection;
    }

    public async Task<bool> CreateAsync(TicketTag ticketTag)
    {
        var sql = @"INSERT INTO TicketTags (TicketId, TagId)
                    VALUES (@TicketId, @TagId)";
        var result = await dbConnection.ExecuteAsync(sql, new { TicketTag = ticketTag });

        return result > 0;
    }

    public async Task<bool> DeleteAsync(Guid ticketId, Guid tagId)
    {
        var sql = @"DELETE FROM TicketTags WHERE TicketId = @TicketId AND TagId = @TagId";
        var result = await dbConnection.ExecuteAsync(sql, new {TicketId = ticketId, TagId = tagId});

        return result > 0;
    }

    public async Task<bool> DeleteByTagIdAsync(Guid tagId)
    {
        var sql = @"DELETE FROM TicketTags WHERE TagId = @TagId";
        var result = await dbConnection.ExecuteAsync(sql, new {TagId = tagId});

        return result > 0;
    }

    public async Task<bool> DeleteByTicketIdAsync(Guid ticketId)
    {
        var sql = @"DELETE FROM TicketTags WHERE TicketId = @TicketId";
        var result = await dbConnection.ExecuteAsync(sql, new {TicketId = ticketId});

        return result > 0;
    }

    public async Task<List<TicketTag>> GetByTagIdAsync(Guid tagId)
    {
        var sql = @"SELECT * FROM TicketTags WHERE TagId = @TagId";
        var result = await dbConnection.QueryAsync<TicketTag>(sql, new { TagId = tagId });

        return result.ToList();
    }

    public async Task<List<TicketTag>> GetByTicketIdAsync(Guid ticketId)
    {
        var sql = @"SELECT * FROM TicketTags WHERE TicketId = @TicketId";
        var result = await dbConnection.QueryAsync<TicketTag>(sql , new {TicketId = ticketId});

        return result.ToList();
    }
}
