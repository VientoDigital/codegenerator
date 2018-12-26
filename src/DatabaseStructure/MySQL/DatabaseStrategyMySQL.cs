using System.Data;
using iCodeGenerator.GenericDataAccess;

namespace iCodeGenerator.DatabaseStructure
{
    public class DatabaseStrategyMySQL : DatabaseStrategy
    {
        protected override Database CreateDatabase(DataRow row, DatabaseCollection databases)
        {
            Database db = new Database();
            db.Name = row["DATABASE"].ToString();
            return db;
        }

        protected override DataSet DatabaseSchema(DataAccessProviderFactory dataAccessProviderFactory, IDbConnection connection)
        {
            DataSet ds = new DataSet();
            IDbCommand sqlString = dataAccessProviderFactory.CreateCommand("show databases", connection);
            sqlString.CommandType = CommandType.Text;
            IDbDataAdapter da = dataAccessProviderFactory.CreateDataAdapter();
            da.SelectCommand = sqlString;
            da.Fill(ds);
            return ds;
        }
    }
}