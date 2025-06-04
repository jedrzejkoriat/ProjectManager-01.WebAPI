using System.Data;
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

    // ============================= QUERIES =============================
    public async Task<IEnumerable<TicketTag>> GetAllAsync()
    {
        var sql = @"SELECT * FROM TicketTags";
        var result = await dbConnection.QueryAsync<TicketTag>(sql);

        return result.ToList();
    }

    public async Task<TicketTag> GetByIdAsync(Guid ticketId, Guid tagId)
    {
        var sql = @"SELECT * FROM TicketTags 
                    WHERE TicketId = @TicketId 
                    AND TagId = @TagId";
        var result = await dbConnection.QueryFirstAsync<TicketTag>(sql, new { TicketId = ticketId, TagId = tagId });

        return result;
    }

    // ============================= COMMANDS =============================
    public async Task<bool> CreateAsync(TicketTag ticketTag, IDbTransaction dbTransaction)
    {
        var sql = @"INSERT INTO TicketTags (TicketId, TagId)
                    VALUES (@TicketId, @TagId)";
        var result = await dbConnection.ExecuteAsync(sql, ticketTag, dbTransaction);

        return result > 0;
    }

    public async Task<bool> CreateAsync(TicketTag ticketTag)
    {
        var sql = @"INSERT INTO TicketTags (TicketId, TagId)
                    VALUES (@TicketId, @TagId)";
        var result = await dbConnection.ExecuteAsync(sql, ticketTag);

        return result > 0;
    }

    public async Task<bool> DeleteAsync(Guid ticketId, Guid tagId)
    {
        var sql = @"DELETE FROM TicketTags 
                    WHERE TicketId = @TicketId 
                    AND TagId = @TagId";
        var result = await dbConnection.ExecuteAsync(sql, new { TicketId = ticketId, TagId = tagId });

        return result > 0;
    }
}
