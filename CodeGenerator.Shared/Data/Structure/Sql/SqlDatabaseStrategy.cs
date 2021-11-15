using System.Collections.Generic;
using System.Data.Common;
using System.Data.SqlClient;
using Extenso.Data.SqlClient;

namespace CodeGenerator.Data.Structure
{
    public class SqlDatabaseStrategy : DatabaseStrategy
    {
        protected override IEnumerable<string> GetDatabaseNames(DbConnection connection)
        {
            return (connection as SqlConnection).GetDatabaseNames();
        }
    }
}