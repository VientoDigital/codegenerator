using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text.RegularExpressions;

namespace CodeGenerator.Data.Structure
{
    public class MySqlColumnStrategy : ColumnStrategy
    {
        protected override IEnumerable<Column> ColumnSchema(Table table, ProviderFactory providerFactory, IDbConnection connection)
        {
            //var columnData = (connection as MySqlConnection).GetColumnData(table.Name);
            //return columnData.OfType<ColumnInfo>().Select(x => new Column
            //{
            //    Name = x.ColumnName,
            //    Type = x.DataTypeNative,
            //    Length = (int)x.MaximumLength,
            //    Nullable = x.IsNullable,
            //    Default = x.DefaultValue,
            //    IsPrimaryKey = x.KeyType == KeyType.PrimaryKey
            //});

            var set = new DataSet();
            using var command = ProviderFactory.CreateCommand("desc " + table.Name, connection);
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
                Name = row.Field<string>("Field")
            };

            bool isParenthesisFound = row.Field<string>("Type").IndexOf("(") != -1;
            if (isParenthesisFound)
            {
                column.Type = row.Field<string>("Type")[..row.Field<string>("Type").IndexOf("(")];
            }
            else
            {
                column.Type = row.Field<string>("Type").Trim();
            }
            if (isParenthesisFound)
            {
                int start = row.Field<string>("Type").IndexOf("(") + 1;
                int end = row.Field<string>("Type").IndexOf(")");
                string length = row.Field<string>("Type")[start..(end - start)];
                int commaPosition = length.IndexOf(',');

                if (commaPosition != -1)
                {
                    length = length[..commaPosition];
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

            column.Nullable = !string.IsNullOrEmpty(row.Field<string>("Null"));
            column.Default = row.Field<string>("Default");
            return column;
        }

        protected override Key CreateKey(DataRow row)
        {
            return new Key
            {
                Name = row.Field<string>("Key_name"),
                ColumnName = row.Field<string>("Column_name"),
                IsPrimary = row.Field<string>("Key_name") == "PRIMARY"
            };
        }

        protected override DataSet KeySchema(Table table, ProviderFactory providerFactory, IDbConnection connection)
        {
            var set = new DataSet();
            using var command = ProviderFactory.CreateCommand("show index from " + table.Name, connection);
            command.CommandType = CommandType.Text;
            var adapter = providerFactory.CreateDataAdapter();
            adapter.SelectCommand = command;
            adapter.Fill(set);
            return set;
        }
    }
}