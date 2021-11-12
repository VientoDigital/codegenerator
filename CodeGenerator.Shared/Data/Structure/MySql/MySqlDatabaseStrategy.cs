using System.Collections.Generic;
using System.Data;

namespace CodeGenerator.Data.Structure
{
    public class MySqlDatabaseStrategy : DatabaseStrategy
    {
        protected override Database CreateDatabase(DataRow row, ICollection<Database> databases)
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