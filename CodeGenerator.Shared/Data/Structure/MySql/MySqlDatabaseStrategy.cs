using System.Collections.Generic;
using System.Data.Common;
using Extenso.Data.MySql;
using MySql.Data.MySqlClient;

namespace CodeGenerator.Data.Structure
{
    public class MySqlDatabaseStrategy : DatabaseStrategy
    {
        protected override IEnumerable<string> GetDatabaseNames(DbConnection connection)
        {
            return (connection as MySqlConnection).GetDatabaseNames();
        }
    }
}