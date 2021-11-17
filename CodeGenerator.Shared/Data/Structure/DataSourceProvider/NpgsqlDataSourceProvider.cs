using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Extenso.Data;
using Extenso.Data.Npgsql;
using Extenso.Data.SqlClient;
using Npgsql;

namespace CodeGenerator.Data.Structure
{
    public class NpgsqlDataSourceProvider : BaseDataSourceProvider<NpgsqlConnection>
    {
        protected override IEnumerable<string> GetDatabaseNames()
        {
            return connection.GetDatabaseNames();
        }

        protected override IEnumerable<Table> GetTableSchema(Database database, ProviderFactory providerFactory)
        {
            var schemas = connection.GetSchemaNames();

            var tables = new List<Table>();
            foreach (string schema in schemas)
            {
                var tableNames = connection.GetTableNames(includeViews: false, schema: schema);
                tables.AddRange(tableNames.Select(x => new Table
                {
                    ParentDatabase = database,
                    Schema = schema,
                    Name = x
                }));
            }

            return tables;
        }

        protected override IEnumerable<Table> GetViewSchema(Database database, ProviderFactory providerFactory)
        {
            var schemas = connection.GetSchemaNames();

            var views = new List<Table>();
            foreach (string schema in schemas)
            {
                var viewNames = connection.GetViewNames(schema: schema);
                views.AddRange(viewNames.Select(x => new Table
                {
                    ParentDatabase = database,
                    Schema = schema,
                    Name = x
                }));
            }

            return views;
        }

        protected override IEnumerable<Column> GetColumnSchema(Table table, ProviderFactory providerFactory)
        {
            var columnData = connection.GetColumnData(table.Name, table.Schema);
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
            using var command = ProviderFactory.CreateCommand(
$@"SELECT
    c2.relname,
    i.indisprimary,
    i.indisunique,
    i.indisclustered,
    pg_catalog.pg_get_indexdef(i.indexrelid, 0, true)
FROM pg_catalog.pg_class c, pg_catalog.pg_class c2, pg_catalog.pg_index i
WHERE c.oid = '{GetTableId(table.Name)}'
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

        private int GetTableId(string tablename)
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
    }
}