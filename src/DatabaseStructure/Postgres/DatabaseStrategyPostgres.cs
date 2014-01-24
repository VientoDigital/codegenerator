using System.Data;
using iCodeGenerator.GenericDataAccess;

namespace iCodeGenerator.DatabaseStructure
{
	public class DatabaseStrategyPostgres : DatabaseStrategy
	{
		protected override Database CreateDatabase(DataRow row, DatabaseCollection databases)
		{
			Database db = new iCodeGenerator.DatabaseStructure.Database();
			db.Name = row["datname"].ToString();
			return db;
		}

		protected override DataSet DatabaseSchema(DataAccessProviderFactory dataProviderFactory, IDbConnection connection)
		{
			DataSet ds = new DataSet();
			IDbCommand sqlString = dataProviderFactory.CreateCommand("SELECT datname FROM pg_database",connection);
			sqlString.CommandType = CommandType.Text;
			IDbDataAdapter da = dataProviderFactory.CreateDataAdapter();
			da.SelectCommand = sqlString;
			da.Fill(ds);
			return ds;
		}
	}
}
