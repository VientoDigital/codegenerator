using System.Data;

namespace CodeGenerator.Data.Structure
{
    public class MySqlTableStrategy : TableStrategy
    {
        protected override DataSet TableSchema(DataAccessProviderFactory dataAccessProvider, IDbConnection connection)
        {
            var set = new DataSet();
            var command = dataAccessProvider.CreateCommand("show tables", connection);
            command.CommandType = CommandType.Text;
            var adapter = dataAccessProvider.CreateDataAdapter();
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
                Name = row[0].ToString(),
                Schema = string.Empty
            };
        }
    }
}