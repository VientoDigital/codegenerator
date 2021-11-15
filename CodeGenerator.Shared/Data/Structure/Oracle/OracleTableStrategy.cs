using System.Data;

namespace CodeGenerator.Data.Structure
{
    public class OracleTableStrategy : TableStrategy
    {
        protected override Table CreateTable(Database database, DataRow row)
        {
            return new Table
            {
                ParentDatabase = database,
                Name = row.Field<string>("table_name"),
                Schema = string.Empty
            };
        }

        protected override DataSet TableSchema(ProviderFactory providerFactory, IDbConnection connection)
        {
            return new DataSet();
        }

        protected override DataSet TableSchema(ProviderFactory providerFactory, IDbConnection connection, Database database)
        {
            var set = new DataSet();
            using var command = ProviderFactory.CreateCommand($"SELECT OWNER, TABLE_NAME FROM all_tables where OWNER = '{database.Name}' ORDER BY TABLE_NAME", connection);
            command.CommandType = CommandType.Text;
            var adapter = providerFactory.CreateDataAdapter();
            adapter.SelectCommand = command;
            adapter.Fill(set);
            return set;
        }

        protected override DataSet ViewSchema(ProviderFactory providerFactory, IDbConnection connection)
        {
            return new DataSet();
        }
    }
}