using System.Collections.Generic;
using System.Data;

namespace CodeGenerator.Data.Structure
{
    public class PostgresDatabaseStrategy : DatabaseStrategy
    {
        protected override Database CreateDatabase(DataRow row, ICollection<Database> databases)
        {
            return new Database
            {
                Name = row.Field<string>("datname")
            };
        }

        protected override DataSet DatabaseSchema(ProviderFactory providerFactory, IDbConnection connection)
        {
            var set = new DataSet();
            var command = providerFactory.CreateCommand("SELECT datname FROM pg_database ORDER BY datname;", connection);
            command.CommandType = CommandType.Text;
            var adapter = providerFactory.CreateDataAdapter();
            adapter.SelectCommand = command;
            adapter.Fill(set);
            return set;
        }
    }
}