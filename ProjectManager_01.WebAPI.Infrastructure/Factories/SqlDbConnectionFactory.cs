using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.Data.SqlClient;
using ProjectManager_01.Application.Contracts.Factories;

namespace ProjectManager_01.Infrastructure.Factories;
public sealed class SqlDbConnectionFactory : IDbConnectionFactory
{
    private readonly string connectionString;

    public SqlDbConnectionFactory(string connectionString)
    {
        this.connectionString = connectionString;
    }

    public IDbConnection CreateConnection()
    {
        return new SqlConnection(connectionString);
    }
}