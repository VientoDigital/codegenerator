using System;
using System.Data;
using CodeGenerator.GenericDataAccess;

namespace CodeGenerator.DatabaseStructure
{
    public class ColumnStrategySQLServer : ColumnStrategy
    {
        protected override DataSet ColumnSchema(Table table, DataAccessProviderFactory dataAccessProvider, IDbConnection connection)
        {
            var set = new DataSet();
            var command = dataAccessProvider.CreateCommand("sp_columns", connection);
            command.CommandType = CommandType.StoredProcedure;

            var parameter = dataAccessProvider.CreateParameter();
            parameter.Direction = ParameterDirection.Input;
            parameter.DbType = DbType.String;
            parameter.ParameterName = "@table_name";
            parameter.Value = table.Name;
            command.Parameters.Add(parameter);

            var schemaParameter = dataAccessProvider.CreateParameter();
            schemaParameter.Direction = ParameterDirection.Input;
            schemaParameter.DbType = DbType.String;
            schemaParameter.ParameterName = "@table_owner";
            schemaParameter.Value = table.Schema;
            command.Parameters.Add(schemaParameter);

            var adapter = dataAccessProvider.CreateDataAdapter();
            adapter.SelectCommand = command;
            adapter.Fill(set);
            return set;
        }

        protected override Column CreateColumn(DataRow row)
        {
            var column = new Column
            {
                Name = row["COLUMN_NAME"].ToString()
            };

            string type = row["TYPE_NAME"].ToString();
            if (type.IndexOf("identity") != -1)
            {
                column.Type = type.Substring(0, type.IndexOf("identity")).Trim();
            }
            else
            {
                column.Type = type;
            }

            column.Length = Convert.ToInt32(row["LENGTH"]);
            column.Nullable = Convert.ToBoolean(row["Nullable"]);
            column.Default = row["COLUMN_DEF"].ToString();
            return column;
        }

        protected override DataSet KeySchema(Table table, DataAccessProviderFactory dataAccessProvider, IDbConnection connection)
        {
            var keysSet = new DataSet();
            var command = dataAccessProvider.CreateCommand("sp_pkeys", connection);
            command.CommandType = CommandType.StoredProcedure;

            var parameter = dataAccessProvider.CreateParameter();
            parameter.Direction = ParameterDirection.Input;
            parameter.DbType = DbType.String;
            parameter.ParameterName = "@table_name";
            parameter.Value = table.Name;
            command.Parameters.Add(parameter);

            var schemaParameter = dataAccessProvider.CreateParameter();
            schemaParameter.Direction = ParameterDirection.Input;
            schemaParameter.DbType = DbType.String;
            schemaParameter.ParameterName = "@table_owner";
            schemaParameter.Value = table.Schema;
            command.Parameters.Add(schemaParameter);

            var adapter = dataAccessProvider.CreateDataAdapter();
            adapter.SelectCommand = command;
            adapter.Fill(keysSet);

            foreach (DataRow row in keysSet.Tables[0].Rows)
            {
                Keys.Add(new Key
                {
                    Name = row["PK_NAME"].ToString(),
                    ColumnName = row["COLUMN_NAME"].ToString(),
                    IsPrimary = true
                });
            }

            var set = new DataSet();
            command = dataAccessProvider.CreateCommand("sp_fkeys", connection);
            command.CommandType = CommandType.StoredProcedure;

            parameter = dataAccessProvider.CreateParameter();
            parameter.Direction = ParameterDirection.Input;
            parameter.DbType = DbType.String;
            parameter.ParameterName = "@pktable_name";
            parameter.Value = table.Name;
            command.Parameters.Add(parameter);

            adapter = dataAccessProvider.CreateDataAdapter();
            adapter.SelectCommand = command;
            adapter.Fill(set);
            set.Merge(keysSet);
            return set;
        }

        protected override Key CreateKey(DataRow row)
        {
            return new Key
            {
                Name = row["PK_NAME"].ToString(),
                ColumnName = row["FKCOLUMN_NAME"].ToString(),
                IsPrimary = false
            };
        }
    }
}