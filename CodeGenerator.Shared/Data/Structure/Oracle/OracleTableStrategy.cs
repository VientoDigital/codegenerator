using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace CodeGenerator.Data.Structure
{
    public class OracleTableStrategy : TableStrategy
    {
        protected override IEnumerable<Table> TableSchema(Database database, ProviderFactory providerFactory, IDbConnection connection)
        {
            var set = new DataSet();
            using var command = ProviderFactory.CreateCommand($"SELECT OWNER, TABLE_NAME FROM all_tables where OWNER = '{database.Name}' ORDER BY TABLE_NAME", connection);
            command.CommandType = CommandType.Text;
            var adapter = providerFactory.CreateDataAdapter();
            adapter.SelectCommand = command;
            adapter.Fill(set);

            return set.Tables[0].Rows.OfType<DataRow>().Select(x => new Table
            {
                ParentDatabase = database,
                Schema = string.Empty,
                Name = x.Field<string>("table_name")
            });
        }

        protected override IEnumerable<Table> ViewSchema(Database database, ProviderFactory providerFactory, IDbConnection connection)
        {
            return Enumerable.Empty<Table>();
        }
    }
}