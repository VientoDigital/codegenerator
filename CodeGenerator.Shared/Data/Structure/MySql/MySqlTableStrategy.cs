using System.Data;

namespace CodeGenerator.Data.Structure
{
    public class MySqlTableStrategy : TableStrategy
    {
        protected override DataSet TableSchema(ProviderFactory providerFactory, IDbConnection connection)
        {
            var set = new DataSet();
            var command = providerFactory.CreateCommand("SHOW FULL TABLES WHERE `Table_Type` = 'BASE TABLE'", connection);
            command.CommandType = CommandType.Text;
            var adapter = providerFactory.CreateDataAdapter();
            adapter.SelectCommand = command;
            adapter.Fill(set);
            return set;
        }

        protected override DataSet ViewSchema(ProviderFactory providerFactory, IDbConnection connection)
        {
            var set = new DataSet();
            var command = providerFactory.CreateCommand("SHOW FULL TABLES WHERE `Table_Type` = 'VIEW'", connection);
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
                Schema = string.Empty,
                Name = row.Field<string>(0)
            };
        }
    }
}