using System;
using System.Data;
using System.Text.RegularExpressions;
using iCodeGenerator.GenericDataAccess;

namespace iCodeGenerator.DatabaseStructure
{
    public class ColumnStrategyMySQL : ColumnStrategy
    {
        protected override Column CreateColumn(DataRow row)
        {
            Column column = new Column();
            column.Name = row["Field"].ToString();
            bool IsParenthesisFound = row["Type"].ToString().IndexOf("(") != -1;
            if (IsParenthesisFound)
            {
                column.Type = row["Type"].ToString().Substring(0, row["Type"].ToString().IndexOf("("));
            }
            else
            {
                column.Type = row["Type"].ToString().Trim();
            }
            if (IsParenthesisFound)
            {
                int start = row["Type"].ToString().IndexOf("(") + 1;
                int end = row["Type"].ToString().IndexOf(")");
                string length = row["Type"].ToString().Substring(start, end - start);
                int comaPosition = length.IndexOf(',');
                if (comaPosition != -1)
                {
                    length = length.Substring(0, comaPosition);
                }
                if (!Regex.IsMatch(length, "^[0-9]+$"))
                {
                    length = "0";
                }
                column.Length = Convert.ToInt32(length);
            }
            else
            {
                column.Length = 0;
            }

            column.Nullable = (row["Null"].ToString() != null && row["Null"].ToString().Length == 0);
            column.Default = row["Default"].ToString();
            return column;
        }

        protected override DataSet ColumnSchema(Table table, DataAccessProviderFactory dataAccessProvider, IDbConnection connection)
        {
            DataSet ds = new DataSet();
            IDbCommand sqlString = dataAccessProvider.CreateCommand("desc " + table.Name, connection);
            sqlString.CommandType = CommandType.Text;
            IDbDataAdapter da = dataAccessProvider.CreateDataAdapter();
            da.SelectCommand = sqlString;
            da.Fill(ds);
            return ds;
        }

        protected override Key CreateKey(DataRow row)
        {
            Key key = new Key();
            key.Name = row["Key_name"].ToString();
            key.ColumnName = row["Column_name"].ToString();
            key.IsPrimary = row["Key_name"].ToString() == "PRIMARY" ? true : false;
            return key;
        }

        protected override DataSet KeySchema(Table table, DataAccessProviderFactory dataAccessProvider, IDbConnection connection)
        {
            DataSet ds = new DataSet();
            IDbCommand sqlSp = dataAccessProvider.CreateCommand("show index from " + table.Name, connection);
            sqlSp.CommandType = CommandType.Text;
            IDbDataAdapter da = dataAccessProvider.CreateDataAdapter();
            da.SelectCommand = sqlSp;
            da.Fill(ds);
            return ds;
        }
    }
}