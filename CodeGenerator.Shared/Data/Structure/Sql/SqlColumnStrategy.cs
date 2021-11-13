using System;
using System.Data;

namespace CodeGenerator.Data.Structure
{
    public class SqlColumnStrategy : ColumnStrategy
    {
        protected override DataSet ColumnSchema(Table table, ProviderFactory providerFactory, IDbConnection connection)
        {
            var set = new DataSet();
            var command = providerFactory.CreateCommand("sp_columns", connection);
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
            return set;
        }

        protected override Column CreateColumn(DataRow row)
        {
            var column = new Column
            {
                Name = row.Field<string>("COLUMN_NAME")
            };

            string type = row.Field<string>("TYPE_NAME");
            if (type.IndexOf("identity") != -1)
            {
                column.Type = type.Substring(0, type.IndexOf("identity")).Trim();
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
            var keysSet = new DataSet();
            var command = providerFactory.CreateCommand("sp_pkeys", connection);
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

            var set = new DataSet();
            command = providerFactory.CreateCommand("sp_fkeys", connection);
            command.CommandType = CommandType.StoredProcedure;

            parameter = providerFactory.CreateParameter();
            parameter.Direction = ParameterDirection.Input;
            parameter.DbType = DbType.String;
            parameter.ParameterName = "@pktable_name";
            parameter.Value = table.Name;
            command.Parameters.Add(parameter);

            adapter = providerFactory.CreateDataAdapter();
            adapter.SelectCommand = command;
            adapter.Fill(set);
            set.Merge(keysSet);
            return set;
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