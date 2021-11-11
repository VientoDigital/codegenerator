using System.Data;
using CodeGenerator.GenericDataAccess;

namespace CodeGenerator.DatabaseStructure
{
    public class DatabaseStrategySQLServer : DatabaseStrategy
    {
        protected override Database CreateDatabase(DataRow row, DatabaseCollection databases)
        {
            return new Database
            {
                Name = row["DATABASE_NAME"].ToString()
            };
        }

        protected override DataSet DatabaseSchema(DataAccessProviderFactory dataAccessProvider, IDbConnection connection)
        {
            var set = new DataSet();
            var command = dataAccessProvider.CreateCommand("SELECT name AS DATABASE_NAME, 0 AS DATABASE_SIZE, NULL AS REMARKS FROM master.dbo.sysdatabases WHERE HAS_DBACCESS(name) = 1  ORDER BY name", connection);
            command.CommandType = CommandType.Text;
            var adapter = dataAccessProvider.CreateDataAdapter();
            adapter.SelectCommand = command;
            adapter.Fill(set);
            return set;
        }
    }
}