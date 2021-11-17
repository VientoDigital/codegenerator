using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using Extenso.Data;
using Extenso.Data.SqlClient;

namespace CodeGenerator.Data.Structure
{
    public class SqlColumnStrategy : ColumnStrategy
    {
        protected override IEnumerable<Column> ColumnSchema(Table table, ProviderFactory providerFactory, IDbConnection connection)
        {
            var columnData = (connection as SqlConnection).GetColumnData(table.Name, table.Schema);
            return columnData.OfType<ColumnInfo>().Select(x => new Column
            {
                ParentTable = table,
                Name = x.ColumnName,
                DbType = x.DataType,
                NativeType = x.DataTypeNative,
                Length = (int)x.MaximumLength,
                Nullable = x.IsNullable,
                Default = x.DefaultValue,
                IsPrimaryKey = x.KeyType == KeyType.PrimaryKey
            });
        }

        protected override IEnumerable<Key> KeySchema(Table table, ProviderFactory providerFactory, IDbConnection connection)
        {
            // TODO:
            //var foreignKeyInfo = (connection as SqlConnection).GetForeignKeyData(table.Name, table.Schema);
            //return foreignKeyInfo.OfType<ForeignKeyInfo>().Select(x => new Key
            //{
            //    Name = x.PrimaryKeyName,
            //    ColumnName = x.ForeignKeyColumn,
            //    IsPrimary = false
            //});

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

            return resultSet.Tables[0].Rows.OfType<DataRow>().Select(x => new Key
            {
                Name = x.Field<string>("PK_NAME"),
                ColumnName = x.Field<string>("FKCOLUMN_NAME"),
                IsPrimary = false
            });
        }
    }
}