using Extenso.Data.MySql;
using Extenso.Data.SqlClient;
using MySql.Data.MySqlClient;

namespace CodeGenerator.Data.Structure;

public class MySqlDataSourceProvider : BaseDataSourceProvider<MySqlConnection>
{
    protected override IEnumerable<string> GetDatabaseNames()
    {
        return connection.GetDatabaseNames();
    }

    protected override IEnumerable<Table> GetTableSchema(Database database, ProviderFactory providerFactory)
    {
        var tableNames = connection.GetTableNames(includeViews: false);
        return tableNames.Select(x => new Table
        {
            ParentDatabase = database,
            Schema = string.Empty,
            Name = x
        });
    }

    protected override IEnumerable<Table> GetViewSchema(Database database, ProviderFactory providerFactory)
    {
        var viewNames = connection.GetViewNames();
        return viewNames.Select(x => new Table
        {
            ParentDatabase = database,
            Schema = string.Empty,
            Name = x
        });
    }

    protected override IEnumerable<Column> GetColumnSchema(Table table, ProviderFactory providerFactory)
    {
        var columnData = connection.GetColumnData(table.Name);
        return columnData.OfType<ColumnInfo>().Select(x => new Column
        {
            ParentTable = table,
            Name = x.ColumnName,
            DbType = x.DataType,
            NativeType = x.DataTypeNative,
            Length = (int)x.MaximumLength,
            Nullable = x.IsNullable,
            Default = x.DefaultValue,
            IsPrimaryKey = x.KeyType == KeyType.PrimaryKey
        });
    }

    protected override IEnumerable<Key> GetKeySchema(Table table, ProviderFactory providerFactory)
    {
        var set = new DataSet();
        using var command = ProviderFactory.CreateCommand("show index from " + table.Name, connection);
        command.CommandType = CommandType.Text;
        var adapter = providerFactory.CreateDataAdapter();
        adapter.SelectCommand = command;
        adapter.Fill(set);

        return set.Tables[0].Rows.OfType<DataRow>().Select(x => new Key
        {
            Name = x.Field<string>("Key_name"),
            ColumnName = x.Field<string>("Column_name"),
            IsPrimary = x.Field<string>("Key_name") == "PRIMARY"
        });
    }
}