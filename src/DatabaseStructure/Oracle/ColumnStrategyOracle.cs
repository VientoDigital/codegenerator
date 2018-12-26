using System;
using System.Data;
using iCodeGenerator.GenericDataAccess;

namespace iCodeGenerator.DatabaseStructure
{
    public class ColumnStrategyOracle : ColumnStrategy
    {
        protected override DataSet ColumnSchema(Table table, DataAccessProviderFactory dataProvider, IDbConnection connection)
        {
            DataSet ds = new DataSet();
            String schemaQuery = "SELECT atc.OWNER, " +
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

            IDbCommand sqlCommand = dataProvider.CreateCommand(schemaQuery, connection);

            sqlCommand.CommandType = CommandType.Text;
            IDbDataAdapter da = dataProvider.CreateDataAdapter();
            da.SelectCommand = sqlCommand;
            da.Fill(ds);
            return ds;
        }

        protected override Column CreateColumn(DataRow row)
        {
            Column column = new Column();
            column.Name = row["COLUMN_NAME"].ToString();
            column.Type = row["DATA_TYPE"].ToString();
            column.Length = Convert.ToInt16(row["DATA_LENGTH"].ToString());

            if (row["NULLABLE"].ToString() == "Y")
            {
                column.Nullable = true;
            }
            else
            {
                column.Nullable = false;
            }

            column.Default = "";
            return column;
        }

        protected override DataSet KeySchema(Table table, DataAccessProviderFactory dataProvider, IDbConnection connection)
        {
            DataSet ds = new DataSet();
            String schemaQuery = "SELECT acc.COLUMN_NAME, " +
            "ac.CONSTRAINT_NAME, " +
            "ac.CONSTRAINT_TYPE " +
            "FROM ALL_CONS_COLUMNS acc " +
            "JOIN ALL_CONSTRAINTS ac " +
            "ON ac.OWNER = acc.OWNER " +
            "AND ac.TABLE_NAME = acc.TABLE_NAME " +
            "AND ac.CONSTRAINT_NAME = acc.CONSTRAINT_NAME " +
            "where acc.owner = '" + table.ParentDatabase.Name + "' " +
            "and acc.Table_NAME = '" + table.Name + "'";
            IDbCommand sqlCommand = dataProvider.CreateCommand(schemaQuery, connection);
            sqlCommand.CommandType = CommandType.Text;
            IDbDataAdapter da = dataProvider.CreateDataAdapter();
            da.SelectCommand = sqlCommand;
            da.Fill(ds);
            return ds;
        }

        protected override Key CreateKey(DataRow row)
        {
            Key key = new Key();
            key.Name = row["CONSTRAINT_NAME"].ToString();
            key.ColumnName = row["COLUMN_NAME"].ToString();
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