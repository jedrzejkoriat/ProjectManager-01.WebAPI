using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using ProjectManager_01.Application.Contracts.Repositories;
using ProjectManager_01.Domain.Models;

namespace ProjectManager_01.Infrastructure.Repositories;
internal class PermissionRepository : IPermissionRepository
{
    private readonly IDbConnection dbConnection;

    public PermissionRepository(IDbConnection dbConnection)
    {
        this.dbConnection = dbConnection;
    }
    public async Task<List<Permission>> GetAllAsync()
    {
        var sql = "SELECT * FROM Permissions";
        var result = await dbConnection.QueryAsync<Permission>(sql);

        return result.ToList();
    }
}
