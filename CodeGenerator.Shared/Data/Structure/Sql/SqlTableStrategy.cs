using System.Data;

namespace CodeGenerator.Data.Structure
{
    public class SqlTableStrategy : TableStrategy
    {
        protected override DataSet TableSchema(ProviderFactory providerFactory, IDbConnection connection)
        {
            var set = new DataSet();

            using var command = ProviderFactory.CreateCommand(
@"SELECT S.[name] AS [Schema], T.[name] AS [Name], T.[type] AS [Type]
FROM sys.tables T
INNER JOIN sys.schemas S ON T.[schema_id] = S.[schema_id]
ORDER BY S.[name], T.[name]", connection);

            command.CommandType = CommandType.Text;
            var adapter = providerFactory.CreateDataAdapter();
            adapter.SelectCommand = command;
            adapter.Fill(set);
            return set;
        }

        protected override DataSet ViewSchema(ProviderFactory providerFactory, IDbConnection connection)
        {
            var set = new DataSet();

            using var command = ProviderFactory.CreateCommand(
@"SELECT S.[name] AS [Schema], V.[name] AS [Name], V.[type] AS [Type]
FROM sys.views V
INNER JOIN sys.schemas S ON V.[schema_id] = S.[schema_id]
ORDER BY S.[name], V.[name]", connection);

            command.CommandType = CommandType.Text;
            var adapter = providerFactory.CreateDataAdapter();
            adapter.SelectCommand = command;
            adapter.Fill(set);
            return set;
        }

        protected override Table CreateTable(Database database, DataRow row)
        {
            return new Table
            {
                ParentDatabase = database,
                Schema = row.Field<string>("Schema"),
                Name = row.Field<string>("Name")
            };
        }
    }
}