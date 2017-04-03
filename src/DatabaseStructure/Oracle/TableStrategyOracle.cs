using System.Data;
using iCodeGenerator.GenericDataAccess;

namespace iCodeGenerator.DatabaseStructure
{
    public class TableStrategyOracle : TableStrategy
    {
        protected override DataSet TableSchema(DataAccessProviderFactory dataAccessProvider, IDbConnection connection)
        {
            return new DataSet();
        }

        protected override DataSet TableSchema(DataAccessProviderFactory dataProvider, IDbConnection connection, Database database)
        {
            var ds = new DataSet();

            var sqlString = dataProvider.CreateCommand("SELECT OWNER, TABLE_NAME FROM all_tables where OWNER = '" + database.Name + "'", connection);
            sqlString.CommandType = CommandType.Text;
            var da = dataProvider.CreateDataAdapter();
            da.SelectCommand = sqlString;
            da.Fill(ds);

            return ds;
        }

        protected override Table CreateTable(Database database, DataRow row)
        {
            var table = new Table();

            table.ParentDatabase = database;
            table.Name = row["table_name"].ToString();
            table.Schema = string.Empty;

            return table;
        }

        protected override DataSet ViewSchema(DataAccessProviderFactory dataAccessProvider, IDbConnection connection)
        {
            return new DataSet();
        }
    }
}