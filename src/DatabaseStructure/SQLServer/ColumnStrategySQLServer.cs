using System;
using System.Data;
using iCodeGenerator.GenericDataAccess;

namespace iCodeGenerator.DatabaseStructure
{
    public class ColumnStrategySQLServer : ColumnStrategy
    {
        protected override DataSet ColumnSchema(Table table, DataAccessProviderFactory dataAccessProvider, IDbConnection connection)
        {
            DataSet ds = new DataSet();
            IDbCommand sqlSp = dataAccessProvider.CreateCommand("sp_columns", connection);
            sqlSp.CommandType = CommandType.StoredProcedure;
            IDbDataParameter param = dataAccessProvider.CreateParameter();
            param.Direction = ParameterDirection.Input;
            param.DbType = DbType.String;
            param.ParameterName = "@table_name";
            param.Value = table.Name;
            sqlSp.Parameters.Add(param);
            IDbDataParameter schemaParameter = dataAccessProvider.CreateParameter();
            schemaParameter.Direction = ParameterDirection.Input;
            schemaParameter.DbType = DbType.String;
            schemaParameter.ParameterName = "@table_owner";
            schemaParameter.Value = table.Schema;
            sqlSp.Parameters.Add(schemaParameter);
            IDbDataAdapter da = dataAccessProvider.CreateDataAdapter();
            da.SelectCommand = sqlSp;
            da.Fill(ds);
            return ds;
        }

        protected override Column CreateColumn(DataRow row)
        {
            Column column = new Column();
            column.Name = row["COLUMN_NAME"].ToString();

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
            DataSet dsPkeys = new DataSet();
            IDbCommand sqlSp = dataAccessProvider.CreateCommand("sp_pkeys", connection);
            sqlSp.CommandType = CommandType.StoredProcedure;
            IDbDataParameter param = dataAccessProvider.CreateParameter();
            param.Direction = ParameterDirection.Input;
            param.DbType = DbType.String;
            param.ParameterName = "@table_name";
            param.Value = table.Name;
            sqlSp.Parameters.Add(param);
            IDbDataParameter schemaParameter = dataAccessProvider.CreateParameter();
            schemaParameter.Direction = ParameterDirection.Input;
            schemaParameter.DbType = DbType.String;
            schemaParameter.ParameterName = "@table_owner";
            schemaParameter.Value = table.Schema;
            sqlSp.Parameters.Add(schemaParameter);
            IDbDataAdapter da = dataAccessProvider.CreateDataAdapter();
            da.SelectCommand = sqlSp;
            da.Fill(dsPkeys);
            foreach (DataRow row in dsPkeys.Tables[0].Rows)
            {
                Key key = new Key();
                key.Name = row["PK_NAME"].ToString();
                key.ColumnName = row["COLUMN_NAME"].ToString();
                key.IsPrimary = true;
                _Keys.Add(key);
            }

            DataSet ds = new DataSet();
            sqlSp = dataAccessProvider.CreateCommand("sp_fkeys", connection);
            sqlSp.CommandType = CommandType.StoredProcedure;
            param = dataAccessProvider.CreateParameter();
            param.Direction = ParameterDirection.Input;
            param.DbType = DbType.String;
            param.ParameterName = "@pktable_name";
            param.Value = table.Name;
            sqlSp.Parameters.Add(param);
            da = dataAccessProvider.CreateDataAdapter();
            da.SelectCommand = sqlSp;
            da.Fill(ds);
            ds.Merge(dsPkeys);
            return ds;
        }

        protected override Key CreateKey(DataRow row)
        {
            Key key = new Key();
            key.Name = row["PK_NAME"].ToString();
            key.ColumnName = row["FKCOLUMN_NAME"].ToString();
            key.IsPrimary = false;
            return key;
        }
    }
}