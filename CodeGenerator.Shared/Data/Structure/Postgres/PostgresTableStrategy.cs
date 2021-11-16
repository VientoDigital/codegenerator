using System.Collections.Generic;
using System.Data;
using System.Linq;
using Extenso.Data.Npgsql;
using Npgsql;

namespace CodeGenerator.Data.Structure
{
    public class PostgresTableStrategy : TableStrategy
    {
        protected override IEnumerable<Table> TableSchema(Database database, ProviderFactory providerFactory, IDbConnection connection)
        {
            var npgsqlConnection = connection as NpgsqlConnection;
            var schemas = npgsqlConnection.GetSchemaNames();

            var tables = new List<Table>();
            foreach (string schema in schemas)
            {
                var tableNames = npgsqlConnection.GetTableNames(includeViews: false, schema: schema);
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
            var npgsqlConnection = connection as NpgsqlConnection;
            var schemas = npgsqlConnection.GetSchemaNames();

            var views = new List<Table>();
            foreach (string schema in schemas)
            {
                var viewNames = npgsqlConnection.GetViewNames(schema: schema);
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