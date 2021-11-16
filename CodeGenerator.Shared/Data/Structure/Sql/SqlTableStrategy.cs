using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using Extenso.Data.SqlClient;

namespace CodeGenerator.Data.Structure
{
    public class SqlTableStrategy : TableStrategy
    {
        protected override IEnumerable<Table> TableSchema(Database database, ProviderFactory providerFactory, IDbConnection connection)
        {
            var sqlConnection = connection as SqlConnection;
            var schemas = sqlConnection.GetSchemaNames();

            var tables = new List<Table>();
            foreach (string schema in schemas)
            {
                var tableNames = sqlConnection.GetTableNames(includeViews: false, schema: schema);
                tables.AddRange(tableNames.Select(x => new Table
                {
                    ParentDatabase = database,
                    Schema = schema,
                    Name = x
                }));
            }

            return tables;
        }

        protected override IEnumerable<Table> ViewSchema(Database database, ProviderFactory providerFactory, IDbConnection connection)
        {
            var sqlConnection = connection as SqlConnection;
            var schemas = sqlConnection.GetSchemaNames();

            var views = new List<Table>();
            foreach (string schema in schemas)
            {
                var viewNames = sqlConnection.GetViewNames(schema: schema);
                views.AddRange(viewNames.Select(x => new Table
                {
                    ParentDatabase = database,
                    Schema = schema,
                    Name = x
                }));
            }

            return views;
        }
    }
}