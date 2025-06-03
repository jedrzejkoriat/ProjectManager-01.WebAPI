using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;

namespace ProjectManager_01.Application.Contracts.Factories;

public interface IDbConnectionFactory
{
    IDbConnection CreateConnection();
}
