using System.Collections.Generic;
using System.Data;

namespace CodeGenerator.Data.Structure
{
    public class OracleDatabaseStrategy : DatabaseStrategy
    {
        protected override Database CreateDatabase(DataRow row, ICollection<Database> databases)
        {
            return new Database
            {
                Name = row.Field<string>("USERNAME")
            };
        }

        protected override DataSet DatabaseSchema(ProviderFactory providerFactory, IDbConnection connection)
        {
            var set = new DataSet();
            var command = providerFactory.CreateCommand("SELECT DISTINCT USERNAME FROM ALL_USERS", connection);
            command.CommandType = CommandType.Text;
            var adapter = providerFactory.CreateDataAdapter();
            adapter.SelectCommand = command;
            adapter.Fill(set);
            return set;
        }
    }
}