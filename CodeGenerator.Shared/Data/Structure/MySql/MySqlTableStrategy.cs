using System.Collections.Generic;
using System.Data;
using System.Linq;
using Extenso.Data.MySql;
using MySql.Data.MySqlClient;

namespace CodeGenerator.Data.Structure
{
    public class MySqlTableStrategy : TableStrategy
    {
        protected override IEnumerable<Table> TableSchema(Database database, ProviderFactory providerFactory, IDbConnection connection)
        {
            var mySqlConnection = connection as MySqlConnection;
            var tableNames = mySqlConnection.GetTableNames(includeViews: false);
            return tableNames.Select(x => new Table
            {
                ParentDatabase = database,
                Schema = string.Empty,
                Name = x
            });
        }

        protected override IEnumerable<Table> ViewSchema(Database database, ProviderFactory providerFactory, IDbConnection connection)
        {
            var mySqlConnection = connection as MySqlConnection;
            var viewNames = mySqlConnection.GetViewNames();
            return viewNames.Select(x => new Table
            {
                ParentDatabase = database,
                Schema = string.Empty,
                Name = x
            });
        }
    }
}