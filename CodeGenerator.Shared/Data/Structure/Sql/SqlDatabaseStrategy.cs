using System.Collections.Generic;
using System.Data;

namespace CodeGenerator.Data.Structure
{
    public class SqlDatabaseStrategy : DatabaseStrategy
    {
        protected override Database CreateDatabase(DataRow row, ICollection<Database> databases)
        {
            return new Database
            {
                Name = row.Field<string>("DATABASE_NAME")
            };
        }

        protected override DataSet DatabaseSchema(ProviderFactory providerFactory, IDbConnection connection)
        {
            var set = new DataSet();
            var command = providerFactory.CreateCommand("SELECT name AS DATABASE_NAME, 0 AS DATABASE_SIZE, NULL AS REMARKS FROM master.dbo.sysdatabases WHERE HAS_DBACCESS(name) = 1  ORDER BY name", connection);
            command.CommandType = CommandType.Text;
            var adapter = providerFactory.CreateDataAdapter();
            adapter.SelectCommand = command;
            adapter.Fill(set);
            return set;
        }
    }
}