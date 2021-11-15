using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;

namespace CodeGenerator.Data.Structure
{
    public class OracleDatabaseStrategy : DatabaseStrategy
    {
        protected override IEnumerable<string> GetDatabaseNames(DbConnection connection)
        {
            var set = new DataSet();
            var command = connection.CreateCommand();
            command.CommandText = "SELECT DISTINCT USERNAME FROM ALL_USERS";
            command.CommandType = CommandType.Text;
            var adapter = DbProviderFactories.GetFactory(connection).CreateDataAdapter();
            adapter.SelectCommand = command;
            adapter.Fill(set);

            return set.Tables[0].Rows
                .OfType<DataRow>()
                .Select(x => x.Field<string>("USERNAME"))
                .ToList();
        }
    }
}