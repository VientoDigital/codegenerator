using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace CodeGenerator.Data.Structure
{
    public class OracleColumnStrategy : ColumnStrategy
    {
        protected override IEnumerable<Column> ColumnSchema(Table table, ProviderFactory providerFactory, IDbConnection connection)
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

            return set.Tables[0].Rows.OfType<DataRow>().Select(x => CreateColumn(x));
        }

        protected Column CreateColumn(DataRow row)
        {
            var column = new Column
            {
                Name = row.Field<string>("COLUMN_NAME"),
                Type = row.Field<string>("DATA_TYPE"),
                Length = row.Field<int>("DATA_LENGTH")
            };

            if (row.Field<string>("NULLABLE") == "Y")
            {
                column.Nullable = true;
            }
            else
            {
                column.Nullable = false;
            }

            column.Default = string.Empty;
            return column;
        }

        protected override DataSet KeySchema(Table table, ProviderFactory providerFactory, IDbConnection connection)
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
            return set;
        }

        protected override Key CreateKey(DataRow row)
        {
            var key = new Key
            {
                Name = row.Field<string>("CONSTRAINT_NAME"),
                ColumnName = row.Field<string>("COLUMN_NAME")
            };

            if (row.Field<string>("CONSTRAINT_TYPE") == "P")
            {
                key.IsPrimary = true;
            }
            else
            {
                key.IsPrimary = false;
            }
            return key;
        }
    }
}