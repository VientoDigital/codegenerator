using System.Data;
using CodeGenerator.GenericDataAccess;

namespace CodeGenerator.DatabaseStructure
{
    public class TableStrategySQLServer : TableStrategy
    {
        protected override DataSet TableSchema(DataAccessProviderFactory dataAccessProvider, IDbConnection connection)
        {
            var set = new DataSet();
            var command = dataAccessProvider.CreateCommand("SELECT s.name AS [SCHEMA], t.name AS [NAME], t.type AS type " +
                            "FROM sys.tables t " +
                            "INNER JOIN sys.schemas s ON t.schema_id = s.schema_id " +
                            "ORDER BY s.name, t.name", connection);
            command.CommandType = CommandType.Text;
            var adapter = dataAccessProvider.CreateDataAdapter();
            adapter.SelectCommand = command;
            adapter.Fill(set);
            return set;
        }

        protected override DataSet ViewSchema(DataAccessProviderFactory dataAccessProvider, IDbConnection connection)
        {
            var set = new DataSet();
            var command = dataAccessProvider.CreateCommand("SELECT s.name AS [SCHEMA], v.name AS [NAME], v.type AS type " +
                          "FROM sys.views v  " +
                          "INNER JOIN sys.schemas s ON v.schema_id = s.schema_id " +
                          "ORDER BY s.name, v.name ", connection);
            command.CommandType = CommandType.Text;
            var adapter = dataAccessProvider.CreateDataAdapter();
            adapter.SelectCommand = command;
            adapter.Fill(set);
            return set;
        }

        protected override Table CreateTable(Database database, DataRow row)
        {
            return new Table
            {
                ParentDatabase = database,
                Schema = row["SCHEMA"].ToString(),
                Name = row["NAME"].ToString()
            };
        }
    }
}