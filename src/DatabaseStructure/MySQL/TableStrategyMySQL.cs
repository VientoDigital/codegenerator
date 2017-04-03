using System.Data;
using iCodeGenerator.GenericDataAccess;

namespace iCodeGenerator.DatabaseStructure
{
    public class TableStrategyMySQL : TableStrategy
    {
        protected override DataSet TableSchema(DataAccessProviderFactory dataAccessProvider, IDbConnection connection)
        {
            var ds = new DataSet();
            var sqlString = dataAccessProvider.CreateCommand("show tables", connection);
            sqlString.CommandType = CommandType.Text;
            var da = dataAccessProvider.CreateDataAdapter();
            da.SelectCommand = sqlString;
            da.Fill(ds);
            return ds;
        }

        protected override DataSet ViewSchema(DataAccessProviderFactory dataAccessProvider, IDbConnection connection)
        {
            var ds = new DataSet();
            return ds;
        }

        protected override Table CreateTable(Database database, DataRow row)
        {
            var table = new Table();
            table.ParentDatabase = database;
            table.Name = row[0].ToString();
            table.Schema = string.Empty;
            return table;
        }
    }
}