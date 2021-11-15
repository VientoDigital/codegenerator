using System.Data;

namespace CodeGenerator.Data.Structure
{
    public class PostgresTableStrategy : TableStrategy
    {
        protected override DataSet TableSchema(ProviderFactory providerFactory, IDbConnection connection)
        {
            var set = new DataSet();

            var command = ProviderFactory.CreateCommand(
@"SELECT ""table_schema"" AS ""Schema"", ""table_name"" AS ""Name"", ""table_type"" AS ""Type""
FROM information_schema.""tables""
WHERE ""table_schema"" NOT IN('information_schema', 'pg_catalog')
AND ""table_type"" = 'BASE TABLE'
ORDER BY ""table_name""", connection);

            command.CommandType = CommandType.Text;
            var adapter = providerFactory.CreateDataAdapter();
            adapter.SelectCommand = command;
            adapter.Fill(set);
            return set;
        }

        protected override DataSet ViewSchema(ProviderFactory providerFactory, IDbConnection connection)
        {
            var set = new DataSet();

            using var command = ProviderFactory.CreateCommand(
@"SELECT ""table_schema"" AS ""Schema"", ""table_name"" AS ""Name"", ""table_type"" AS ""Type""
FROM information_schema.""tables""
WHERE ""table_schema"" NOT IN('information_schema', 'pg_catalog')
AND ""table_type"" = 'VIEW'
ORDER BY ""table_name""", connection);

            command.CommandType = CommandType.Text;
            var adapter = providerFactory.CreateDataAdapter();
            adapter.SelectCommand = command;
            adapter.Fill(set);
            return set;
        }

        protected override Table CreateTable(Database database, DataRow row)
        {
            return new Table
            {
                ParentDatabase = database,
                Schema = row.Field<string>("Schema"),
                Name = row.Field<string>("Name")
            };
        }
    }
}