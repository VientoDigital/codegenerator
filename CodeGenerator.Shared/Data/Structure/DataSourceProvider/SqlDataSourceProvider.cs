using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using Extenso.Data;
using Extenso.Data.SqlClient;

namespace CodeGenerator.Data.Structure
{
    public class SqlDataSourceProvider : BaseDataSourceProvider<SqlConnection>
    {
        protected override IEnumerable<string> GetDatabaseNames()
        {
            return connection.GetDatabaseNames();
        }

        protected override IEnumerable<Table> GetTableSchema(Database database, ProviderFactory providerFactory)
        {
            var schemas = connection.GetSchemaNames();

            var tables = new List<Table>();
            foreach (string schema in schemas)
            {
                var tableNames = connection.GetTableNames(includeViews: false, schema: schema);
                tables.AddRange(tableNames.Select(x => new Table
                {
                    ParentDatabase = database,
                    Schema = schema,
                    Name = x
                }));
            }

            return tables;
        }

        protected override IEnumerable<Table> GetViewSchema(Database database, ProviderFactory providerFactory)
        {
            var schemas = connection.GetSchemaNames();

            var views = new List<Table>();
            foreach (string schema in schemas)
            {
                var viewNames = connection.GetViewNames(schema: schema);
                views.AddRange(viewNames.Select(x => new Table
                {
                    ParentDatabase = database,
                    Schema = schema,
                    Name = x
                }));
            }

            return views;
        }

        protected override IEnumerable<Column> GetColumnSchema(Table table, ProviderFactory providerFactory)
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

        protected override IEnumerable<Key> GetKeySchema(Table table, ProviderFactory providerFactory)
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