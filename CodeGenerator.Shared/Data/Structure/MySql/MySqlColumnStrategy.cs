using System.Collections.Generic;
using System.Data;
using System.Linq;
using Extenso.Data;
using Extenso.Data.MySql;
using MySql.Data.MySqlClient;

namespace CodeGenerator.Data.Structure
{
    public class MySqlColumnStrategy : ColumnStrategy
    {
        protected override IEnumerable<Column> ColumnSchema(Table table, ProviderFactory providerFactory, IDbConnection connection)
        {
            var columnData = (connection as MySqlConnection).GetColumnData(table.Name);
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

        protected override IEnumerable<Key> KeySchema(Table table, ProviderFactory providerFactory, IDbConnection connection)
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
}