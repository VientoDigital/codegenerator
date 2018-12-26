using System.Data;
using iCodeGenerator.GenericDataAccess;

namespace iCodeGenerator.DatabaseStructure
{
    public class DatabaseStrategyPostgres : DatabaseStrategy
    {
        protected override Database CreateDatabase(DataRow row, DatabaseCollection databases)
        {
            var database = new Database
            {
                Name = row["datname"].ToString()
            };
            return database;
        }

        protected override DataSet DatabaseSchema(DataAccessProviderFactory dataProviderFactory, IDbConnection connection)
        {
            var set = new DataSet();
            var command = dataProviderFactory.CreateCommand("SELECT datname FROM pg_database ORDER BY datname;", connection);
            command.CommandType = CommandType.Text;
            var adapter = dataProviderFactory.CreateDataAdapter();
            adapter.SelectCommand = command;
            adapter.Fill(set);
            return set;
        }
    }
}