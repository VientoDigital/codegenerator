using System.Data;
using iCodeGenerator.GenericDataAccess;

namespace iCodeGenerator.DatabaseStructure
{
    public class DatabaseStrategySQLServer : DatabaseStrategy
    {
        protected override Database CreateDatabase(DataRow row, DatabaseCollection databases)
        {
            Database db = new Database();
            db.Name = row["DATABASE_NAME"].ToString();
            return db;
        }

        protected override DataSet DatabaseSchema(DataAccessProviderFactory dataAccessProvider, IDbConnection connection)
        {
            DataSet ds = new DataSet();
            IDbCommand sqlSp = dataAccessProvider.CreateCommand("SELECT name AS DATABASE_NAME, 0 AS DATABASE_SIZE, NULL AS REMARKS FROM master.dbo.sysdatabases WHERE HAS_DBACCESS(name) = 1  ORDER BY name", connection);
            sqlSp.CommandType = CommandType.Text;

            //			IDbCommand sqlSp = dataAccessProvider.CreateCommand("sp_databases", connection);
            //			sqlSp.CommandType = CommandType.StoredProcedure;
            IDbDataAdapter da = dataAccessProvider.CreateDataAdapter();
            da.SelectCommand = sqlSp;
            da.Fill(ds);
            return ds;
        }
    }
}