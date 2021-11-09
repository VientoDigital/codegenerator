using System.Data;
using iCodeGenerator.GenericDataAccess;

namespace iCodeGenerator.DatabaseStructure
{
    public class DatabaseStrategyOracle : DatabaseStrategy
    {
        protected override Database CreateDatabase(DataRow row, DatabaseCollection databases)
        {
            return new Database
            {
                Name = row["USERNAME"].ToString()
            };
        }

        protected override DataSet DatabaseSchema(DataAccessProviderFactory dataProviderFactory, IDbConnection connection)
        {
            var set = new DataSet();
            var command = dataProviderFactory.CreateCommand("SELECT DISTINCT USERNAME FROM ALL_USERS", connection);
            command.CommandType = CommandType.Text;
            var adapter = dataProviderFactory.CreateDataAdapter();
            adapter.SelectCommand = command;
            adapter.Fill(set);
            return set;
        }
    }
}