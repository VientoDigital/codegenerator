using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Extenso.Data;
using Extenso.Data.Npgsql;
using Npgsql;

namespace CodeGenerator.Data.Structure
{
    public class PostgresColumnStrategy : ColumnStrategy
    {
        protected override IEnumerable<Column> ColumnSchema(Table table, ProviderFactory providerFactory, IDbConnection connection)
        {
            var columnData = (connection as NpgsqlConnection).GetColumnData(table.Name, table.Schema);
            return columnData.OfType<ColumnInfo>().Select(x => new Column
            {
                ParentTable = table,
                Name = x.ColumnName,
                Type = x.DataTypeNative,
                Length = (int)x.MaximumLength,
                Nullable = x.IsNullable,
                Default = x.DefaultValue,
                IsPrimaryKey = x.KeyType == KeyType.PrimaryKey
            });
        }

        private static int GetTableId(string tablename, ProviderFactory providerFactory, IDbConnection connection)
        {
            using var command = ProviderFactory.CreateCommand(
$@"SELECT
    c.oid,
    n.nspname,
    c.relname
FROM pg_catalog.pg_class c
LEFT JOIN pg_catalog.pg_namespace n ON n.oid = c.relnamespace
WHERE pg_catalog.pg_table_is_visible(c.oid)
AND c.relname ~ '^{tablename}$'
ORDER BY 2, 3;", connection);

            return Convert.ToInt32(command.ExecuteScalar());
        }

        protected override IEnumerable<Key> KeySchema(Table table, ProviderFactory providerFactory, IDbConnection connection)
        {
            var set = new DataSet();
            using var command = ProviderFactory.CreateCommand(
$@"SELECT
    c2.relname,
    i.indisprimary,
    i.indisunique,
    i.indisclustered,
    pg_catalog.pg_get_indexdef(i.indexrelid, 0, true)
FROM pg_catalog.pg_class c, pg_catalog.pg_class c2, pg_catalog.pg_index i
WHERE c.oid = '{GetTableId(table.Name, providerFactory, connection)}'
AND c.oid = i.indrelid
AND i.indexrelid = c2.oid
ORDER BY i.indisprimary DESC, i.indisunique DESC, c2.relname", connection);

            command.CommandType = CommandType.Text;
            var adapter = providerFactory.CreateDataAdapter();
            adapter.SelectCommand = command;
            adapter.Fill(set);

            return set.Tables[0].Rows.OfType<DataRow>().Select(x => new Key
            {
                Name = x.Field<string>("relname"),
                ColumnName = x.Field<string>("relname"),
                IsPrimary = Convert.ToBoolean(x.Field<string>("indisprimary"))
            });
        }
    }
}