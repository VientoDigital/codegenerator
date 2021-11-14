using System.Collections.Generic;
using System.Data.Common;
using Extenso.Data.Npgsql;
using Npgsql;

namespace CodeGenerator.Data.Structure
{
    public class PostgresDatabaseStrategy : DatabaseStrategy
    {
        protected override IEnumerable<string> GetDatabaseNames(DbConnection connection)
        {
            return (connection as NpgsqlConnection).GetDatabaseNames();
        }
    }
}