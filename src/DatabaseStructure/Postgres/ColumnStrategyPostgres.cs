using System;
using System.Data;
using iCodeGenerator.GenericDataAccess;

namespace iCodeGenerator.DatabaseStructure
{
    public class ColumnStrategyPostgres : ColumnStrategy
    {
        protected override DataSet ColumnSchema(Table table, DataAccessProviderFactory dataProvider, IDbConnection connection)
        {
            var set = new DataSet();
            int tableId = GetTableId(table.Name, dataProvider, connection);
            var command = dataProvider.CreateCommand("SELECT a.attname,t.typname as atttype, " +
                "(SELECT substring(d.adsrc for 128) FROM pg_catalog.pg_attrdef d " +
                "WHERE d.adrelid = a.attrelid AND d.adnum = a.attnum AND a.atthasdef)as attdef, a.attlen, a.atttypmod,a.attnotnull, a.attnum " +
                "FROM pg_catalog.pg_attribute a, pg_catalog.pg_type t " +
                "WHERE a.attrelid = '" + tableId + "' AND a.attnum > 0 AND NOT a.attisdropped " +
                "AND t.oid = a.atttypid " +
                "ORDER BY a.attnum", connection);
            command.CommandType = CommandType.Text;
            var adapter = dataProvider.CreateDataAdapter();
            adapter.SelectCommand = command;
            adapter.Fill(set);
            return set;
        }

        private static int GetTableId(string tablename, DataAccessProviderFactory dataProvider, IDbConnection connection)
        {
            var command = dataProvider.CreateCommand(@"SELECT c.oid," +
                @"n.nspname, " +
                @"c.relname " +
                @"FROM pg_catalog.pg_class c " +
                @"LEFT JOIN pg_catalog.pg_namespace n ON n.oid = c.relnamespace " +
                @"WHERE pg_catalog.pg_table_is_visible(c.oid) " +
                @"AND c.relname ~ '^" + tablename + "$' " +
                @"ORDER BY 2, 3;", connection);

            return Convert.ToInt32(command.ExecuteScalar());
        }

        protected override Column CreateColumn(DataRow row)
        {
            int length = Convert.ToInt32(row["attlen"]);

            return new Column
            {
                Name = row["attname"].ToString(),
                Type = row["atttype"].ToString(),
                Length = length >= 0 ? length : Convert.ToInt32(row["atttypmod"]) - 4,
                Nullable = !Convert.ToBoolean(row["attnotnull"]),
                Default = row["attdef"].ToString()
            };
        }

        protected override DataSet KeySchema(Table table, DataAccessProviderFactory dataProvider, IDbConnection connection)
        {
            var set = new DataSet();
            var command = dataProvider.CreateCommand("SELECT c2.relname, i.indisprimary, i.indisunique, i.indisclustered, pg_catalog.pg_get_indexdef(i.indexrelid, 0, true)" +
                "FROM pg_catalog.pg_class c, pg_catalog.pg_class c2, pg_catalog.pg_index i " +
                "WHERE c.oid = '" + GetTableId(table.Name, dataProvider, connection) + "' AND c.oid = i.indrelid AND i.indexrelid = c2.oid " +
                "ORDER BY i.indisprimary DESC, i.indisunique DESC, c2.relname", connection);
            command.CommandType = CommandType.Text;
            var adapter = dataProvider.CreateDataAdapter();
            adapter.SelectCommand = command;
            adapter.Fill(set);
            return set;
        }

        protected override Key CreateKey(DataRow row)
        {
            return new Key
            {
                Name = row["relname"].ToString(),
                ColumnName = row["relname"].ToString(),
                IsPrimary = Convert.ToBoolean(row["indisprimary"])
            };
        }
    }
}