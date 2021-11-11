using System;
using System.Data;

namespace CodeGenerator.Data.Structure
{
    public class OracleColumnStrategy : ColumnStrategy
    {
        protected override DataSet ColumnSchema(Table table, DataAccessProviderFactory dataProvider, IDbConnection connection)
        {
            var set = new DataSet();
            string schemaQuery = "SELECT atc.OWNER, " +
                "atc.TABLE_NAME, " +
                "atc.COLUMN_NAME, " +
                "atc.DATA_TYPE, " +
                "atc.DATA_LENGTH, " +
                "atc.DATA_PRECISION, " +
                "atc.DATA_SCALE, " +
                "atc.NULLABLE, " +
                "atc.COLUMN_ID, " +
                "acc.CONSTRAINT_NAME, " +
                "ac.CONSTRAINT_TYPE, " +
                "ac.R_CONSTRAINT_NAME, " +
                "ac.INDEX_NAME " +
                "FROM ALL_TAB_COLUMNS atc " +
                "LEFT OUTER JOIN ALL_CONS_COLUMNS acc " +
                "ON acc.OWNER = atc.OWNER " +
                "AND acc.TABLE_NAME = atc.TABLE_NAME " +
                "AND acc.COLUMN_NAME = atc.COLUMN_NAME " +
                "LEFT OUTER JOIN ALL_CONSTRAINTS ac " +
                "ON ac.OWNER = acc.OWNER " +
                "AND ac.CONSTRAINT_NAME = acc.CONSTRAINT_NAME " +
                "WHERE atc.OWNER = '" + table.ParentDatabase.Name + "' " +
                "AND atc.TABLE_NAME = '" + table.Name + "' " +
                "ORDER BY TABLE_NAME asc";

            var command = dataProvider.CreateCommand(schemaQuery, connection);

            command.CommandType = CommandType.Text;
            var adapter = dataProvider.CreateDataAdapter();
            adapter.SelectCommand = command;
            adapter.Fill(set);
            return set;
        }

        protected override Column CreateColumn(DataRow row)
        {
            var column = new Column
            {
                Name = row["COLUMN_NAME"].ToString(),
                Type = row["DATA_TYPE"].ToString(),
                Length = Convert.ToInt16(row["DATA_LENGTH"].ToString())
            };

            if (row["NULLABLE"].ToString() == "Y")
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

        protected override DataSet KeySchema(Table table, DataAccessProviderFactory dataProvider, IDbConnection connection)
        {
            var set = new DataSet();
            string schemaQuery = "SELECT acc.COLUMN_NAME, " +
            "ac.CONSTRAINT_NAME, " +
            "ac.CONSTRAINT_TYPE " +
            "FROM ALL_CONS_COLUMNS acc " +
            "JOIN ALL_CONSTRAINTS ac " +
            "ON ac.OWNER = acc.OWNER " +
            "AND ac.TABLE_NAME = acc.TABLE_NAME " +
            "AND ac.CONSTRAINT_NAME = acc.CONSTRAINT_NAME " +
            "where acc.owner = '" + table.ParentDatabase.Name + "' " +
            "and acc.Table_NAME = '" + table.Name + "'";
            var command = dataProvider.CreateCommand(schemaQuery, connection);
            command.CommandType = CommandType.Text;
            var adapter = dataProvider.CreateDataAdapter();
            adapter.SelectCommand = command;
            adapter.Fill(set);
            return set;
        }

        protected override Key CreateKey(DataRow row)
        {
            var key = new Key
            {
                Name = row["CONSTRAINT_NAME"].ToString(),
                ColumnName = row["COLUMN_NAME"].ToString()
            };

            if (row["CONSTRAINT_TYPE"].ToString() == "P")
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