using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManager_01.Application.Helpers;
internal static class DbTransactionHelper
{
    public static IDbTransaction BeginTransaction(IDbConnection connection)
    {
        if (connection.State != ConnectionState.Open)
            connection.Open();

        return connection.BeginTransaction();
    }
}