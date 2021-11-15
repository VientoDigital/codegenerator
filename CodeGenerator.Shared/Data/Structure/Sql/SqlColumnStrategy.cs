using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace CodeGenerator.Data.Structure
{
    public class SqlColumnStrategy : ColumnStrategy
    {
        protected override IEnumerable<Column> ColumnSchema(Table table, ProviderFactory providerFactory, IDbConnection connection)
        {
            //var columnData = (connection as SqlConnection).GetColumnData(table.Name);

            var set = new DataSet();
            using var command = ProviderFactory.CreateCommand("sp_columns", connection);
            command.CommandType = CommandType.StoredProcedure;

            var parameter = providerFactory.CreateParameter();
            parameter.Direction = ParameterDirection.Input;
            parameter.DbType = DbType.String;
            parameter.ParameterName = "@table_name";
            parameter.Value = table.Name;
            command.Parameters.Add(parameter);

            var schemaParameter = providerFactory.CreateParameter();
            schemaParameter.Direction = ParameterDirection.Input;
            schemaParameter.DbType = DbType.String;
            schemaParameter.ParameterName = "@table_owner";
            schemaParameter.Value = table.Schema;
            command.Parameters.Add(schemaParameter);

            var adapter = providerFactory.CreateDataAdapter();
            adapter.SelectCommand = command;
            adapter.Fill(set);

            return set.Tables[0].Rows.OfType<DataRow>().Select(x => CreateColumn(x));
        }

        protected Column CreateColumn(DataRow row)
        {
            var column = new Column
            {
                Name = row.Field<string>("COLUMN_NAME")
            };

            string type = row.Field<string>("TYPE_NAME");
            if (type.IndexOf("identity") != -1)
            {
                column.Type = type[..type.IndexOf("identity")].Trim();
            }
            else
            {
                column.Type = type;
            }

            column.Length = row.Field<int>("LENGTH");
            column.Nullable = Convert.ToBoolean(row.Field<short>("Nullable"));
            column.Default = row.Field<string>("COLUMN_DEF");
            return column;
        }

        protected override DataSet KeySchema(Table table, ProviderFactory providerFactory, IDbConnection connection)
        {
            var resultSet = new DataSet();
            var keysSet = new DataSet();

            using (var command = ProviderFactory.CreateCommand("sp_pkeys", connection))
            {
                command.CommandType = CommandType.StoredProcedure;

                var parameter = providerFactory.CreateParameter();
                parameter.Direction = ParameterDirection.Input;
                parameter.DbType = DbType.String;
                parameter.ParameterName = "@table_name";
                parameter.Value = table.Name;
                command.Parameters.Add(parameter);

                var schemaParameter = providerFactory.CreateParameter();
                schemaParameter.Direction = ParameterDirection.Input;
                schemaParameter.DbType = DbType.String;
                schemaParameter.ParameterName = "@table_owner";
                schemaParameter.Value = table.Schema;
                command.Parameters.Add(schemaParameter);

                var adapter = providerFactory.CreateDataAdapter();
                adapter.SelectCommand = command;
                adapter.Fill(keysSet);

                foreach (DataRow row in keysSet.Tables[0].Rows)
                {
                    Keys.Add(new Key
                    {
                        Name = row.Field<string>("PK_NAME"),
                        ColumnName = row.Field<string>("COLUMN_NAME"),
                        IsPrimary = true
                    });
                }
            }

            using (var command = ProviderFactory.CreateCommand("sp_fkeys", connection))
            {
                command.CommandType = CommandType.StoredProcedure;

                var parameter = providerFactory.CreateParameter();
                parameter.Direction = ParameterDirection.Input;
                parameter.DbType = DbType.String;
                parameter.ParameterName = "@pktable_name";
                parameter.Value = table.Name;
                command.Parameters.Add(parameter);

                var adapter = providerFactory.CreateDataAdapter();
                adapter.SelectCommand = command;
                adapter.Fill(resultSet);
                resultSet.Merge(keysSet);
            }

            return resultSet;
        }

        protected override Key CreateKey(DataRow row)
        {
            return new Key
            {
                Name = row.Field<string>("PK_NAME"),
                ColumnName = row.Field<string>("FKCOLUMN_NAME"),
                IsPrimary = false
            };
        }
    }
}