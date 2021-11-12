using System.Data;

namespace CodeGenerator.Data.Structure
{
    public class PostgresTableStrategy : TableStrategy
    {
        protected override DataSet TableSchema(DataAccessProviderFactory dataProvider, IDbConnection connection)
        {
            var set = new DataSet();
            var command = dataProvider.CreateCommand("SELECT tablename FROM pg_tables WHERE schemaname = 'public' ORDER BY tablename;", connection);
            command.CommandType = CommandType.Text;
            var adapter = dataProvider.CreateDataAdapter();
            adapter.SelectCommand = command;
            adapter.Fill(set);
            return set;
        }

        protected override DataSet ViewSchema(DataAccessProviderFactory dataAccessProvider, IDbConnection connection)
        {
            return new DataSet();
        }

        protected override Table CreateTable(Database database, DataRow row)
        {
            return new Table
            {
                ParentDatabase = database,
                Name = row.Field<string>("tablename"),
                Schema = string.Empty
            };
        }
    }
}