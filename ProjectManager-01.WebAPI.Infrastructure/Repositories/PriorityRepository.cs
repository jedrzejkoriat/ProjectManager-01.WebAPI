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
internal class PriorityRepository : IPriorityRepository
{
    private readonly IDbConnection dbConnection;

    public PriorityRepository(IDbConnection dbConnection)
    {
        this.dbConnection = dbConnection;
    }

    public async Task<List<Priority>> GetAllAsync()
    {
        var sql = @"SELECT * FROM Priorities";
        var result = await dbConnection.QueryAsync<Priority>(sql);

        return result.ToList();
    }
}
