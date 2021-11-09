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
            var column = new Column
            {
                Name = row["Field"].ToString()
            };

            bool isParenthesisFound = row["Type"].ToString().IndexOf("(") != -1;
            if (isParenthesisFound)
            {
                column.Type = row["Type"].ToString().Substring(0, row["Type"].ToString().IndexOf("("));
            }
            else
            {
                column.Type = row["Type"].ToString().Trim();
            }
            if (isParenthesisFound)
            {
                int start = row["Type"].ToString().IndexOf("(") + 1;
                int end = row["Type"].ToString().IndexOf(")");
                string length = row["Type"].ToString().Substring(start, end - start);
                int commaPosition = length.IndexOf(',');

                if (commaPosition != -1)
                {
                    length = length.Substring(0, commaPosition);
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
            var set = new DataSet();
            var command = dataAccessProvider.CreateCommand("desc " + table.Name, connection);
            command.CommandType = CommandType.Text;
            var adapter = dataAccessProvider.CreateDataAdapter();
            adapter.SelectCommand = command;
            adapter.Fill(set);
            return set;
        }

        protected override Key CreateKey(DataRow row)
        {
            return new Key
            {
                Name = row["Key_name"].ToString(),
                ColumnName = row["Column_name"].ToString(),
                IsPrimary = row["Key_name"].ToString() == "PRIMARY"
            };
        }

        protected override DataSet KeySchema(Table table, DataAccessProviderFactory dataAccessProvider, IDbConnection connection)
        {
            var set = new DataSet();
            var command = dataAccessProvider.CreateCommand("show index from " + table.Name, connection);
            command.CommandType = CommandType.Text;
            var adapter = dataAccessProvider.CreateDataAdapter();
            adapter.SelectCommand = command;
            adapter.Fill(set);
            return set;
        }
    }
}