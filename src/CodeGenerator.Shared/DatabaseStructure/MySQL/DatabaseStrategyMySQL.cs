using System.Data;
using CodeGenerator.GenericDataAccess;

namespace CodeGenerator.DatabaseStructure
{
    public class DatabaseStrategyMySQL : DatabaseStrategy
    {
        protected override Database CreateDatabase(DataRow row, DatabaseCollection databases)
        {
            return new Database
            {
                Name = row["DATABASE"].ToString()
            };
        }

        protected override DataSet DatabaseSchema(DataAccessProviderFactory dataAccessProviderFactory, IDbConnection connection)
        {
            var set = new DataSet();
            var command = dataAccessProviderFactory.CreateCommand("show databases", connection);
            command.CommandType = CommandType.Text;
            var adapter = dataAccessProviderFactory.CreateDataAdapter();
            adapter.SelectCommand = command;
            adapter.Fill(set);
            return set;
        }
    }
}