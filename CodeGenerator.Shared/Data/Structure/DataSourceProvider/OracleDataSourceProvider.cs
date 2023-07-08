using Oracle.ManagedDataAccess.Client;

namespace CodeGenerator.Data.Structure;

public class OracleDataSourceProvider : BaseDataSourceProvider<OracleConnection>
{
    protected override IEnumerable<string> GetDatabaseNames()
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

    protected override IEnumerable<Table> GetTableSchema(Database database, ProviderFactory providerFactory)
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

    protected override IEnumerable<Table> GetViewSchema(Database database, ProviderFactory providerFactory)
    {
        return Enumerable.Empty<Table>();
    }

    protected override IEnumerable<Column> GetColumnSchema(Table table, ProviderFactory providerFactory)
    {
        var set = new DataSet();
        string schemaQuery =
$@"SELECT
    atc.OWNER,
    atc.TABLE_NAME,
    atc.COLUMN_NAME,
    atc.DATA_TYPE,
    atc.DATA_LENGTH,
    atc.DATA_PRECISION,
    atc.DATA_SCALE,
    atc.NULLABLE,
    atc.COLUMN_ID,
    acc.CONSTRAINT_NAME,
    ac.CONSTRAINT_TYPE,
    ac.R_CONSTRAINT_NAME,
    ac.INDEX_NAME
FROM ALL_TAB_COLUMNS atc
LEFT OUTER JOIN ALL_CONS_COLUMNS acc
    ON acc.OWNER = atc.OWNER
    AND acc.TABLE_NAME = atc.TABLE_NAME
    AND acc.COLUMN_NAME = atc.COLUMN_NAME
LEFT OUTER JOIN ALL_CONSTRAINTS ac
    ON ac.OWNER = acc.OWNER
    AND ac.CONSTRAINT_NAME = acc.CONSTRAINT_NAME
WHERE atc.OWNER = '{table.ParentDatabase.Name}'
AND atc.TABLE_NAME = '{table.Name}'
ORDER BY TABLE_NAME asc";

        using var command = ProviderFactory.CreateCommand(schemaQuery, connection);
        command.CommandType = CommandType.Text;
        var adapter = providerFactory.CreateDataAdapter();
        adapter.SelectCommand = command;
        adapter.Fill(set);

        return set.Tables[0].Rows.OfType<DataRow>().Select(x => new Column
        {
            ParentTable = table,
            Name = x.Field<string>("COLUMN_NAME"),
            NativeType = x.Field<string>("DATA_TYPE"),
            Length = x.Field<int>("DATA_LENGTH"),
            Nullable = x.Field<string>("NULLABLE") == "Y",
            Default = string.Empty
        });
    }

    protected override IEnumerable<Key> GetKeySchema(Table table, ProviderFactory providerFactory)
    {
        var set = new DataSet();
        string schemaQuery =
$@"SELECT
    acc.COLUMN_NAME,
    ac.CONSTRAINT_NAME,
    ac.CONSTRAINT_TYPE
FROM ALL_CONS_COLUMNS acc
JOIN ALL_CONSTRAINTS ac
    ON ac.OWNER = acc.OWNER
    AND ac.TABLE_NAME = acc.TABLE_NAME
    AND ac.CONSTRAINT_NAME = acc.CONSTRAINT_NAME
where acc.owner = '{table.ParentDatabase.Name}'
and acc.Table_NAME = '{table.Name}'";

        using var command = ProviderFactory.CreateCommand(schemaQuery, connection);
        command.CommandType = CommandType.Text;
        var adapter = providerFactory.CreateDataAdapter();
        adapter.SelectCommand = command;
        adapter.Fill(set);

        return set.Tables[0].Rows.OfType<DataRow>().Select(x => new Key
        {
            Name = x.Field<string>("CONSTRAINT_NAME"),
            ColumnName = x.Field<string>("COLUMN_NAME"),
            IsPrimary = x.Field<string>("CONSTRAINT_TYPE") == "P"
        });
    }
}