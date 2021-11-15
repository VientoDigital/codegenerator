using System;
using System.Data;

namespace CodeGenerator.Data.Structure
{
    public class PostgresColumnStrategy : ColumnStrategy
    {
        protected override DataSet ColumnSchema(Table table, ProviderFactory providerFactory, IDbConnection connection)
        {
            var set = new DataSet();
            int tableId = GetTableId(table.Name, providerFactory, connection);
            using var command = ProviderFactory.CreateCommand(
$@"SELECT
    a.attname,
    t.typname as atttype,
    (
        SELECT substring(d.adsrc for 128)
        FROM pg_catalog.pg_attrdef d
        WHERE d.adrelid = a.attrelid
        AND d.adnum = a.attnum
        AND a.atthasdef
    ) as attdef,
    a.attlen,
    a.atttypmod,
    a.attnotnull,
    a.attnum
FROM pg_catalog.pg_attribute a, pg_catalog.pg_type t
WHERE a.attrelid = '{tableId}'
AND a.attnum > 0
AND NOT a.attisdropped
AND t.oid = a.atttypid
ORDER BY a.attnum", connection);

            command.CommandType = CommandType.Text;
            var adapter = providerFactory.CreateDataAdapter();
            adapter.SelectCommand = command;
            adapter.Fill(set);
            return set;
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

        protected override Column CreateColumn(DataRow row)
        {
            int length = row.Field<int>("attlen");

            return new Column
            {
                Name = row.Field<string>("attname"),
                Type = row.Field<string>("atttype"),
                Length = length >= 0 ? length : row.Field<int>("atttypmod") - 4,
                Nullable = !Convert.ToBoolean(row.Field<string>("attnotnull")),
                Default = row.Field<string>("attdef")
            };
        }

        protected override DataSet KeySchema(Table table, ProviderFactory providerFactory, IDbConnection connection)
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
            return set;
        }

        protected override Key CreateKey(DataRow row)
        {
            return new Key
            {
                Name = row.Field<string>("relname"),
                ColumnName = row.Field<string>("relname"),
                IsPrimary = Convert.ToBoolean(row.Field<string>("indisprimary"))
            };
        }
    }
}