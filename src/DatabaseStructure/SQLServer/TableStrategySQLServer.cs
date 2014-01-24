using System.Data;
using iCodeGenerator.GenericDataAccess;

namespace iCodeGenerator.DatabaseStructure
{
	public class TableStrategySQLServer : TableStrategy
	{
		protected override DataSet TableSchema(DataAccessProviderFactory dataAccessProvider, IDbConnection connection)
		{
			var ds = new DataSet();
		    var sqlQry = dataAccessProvider.CreateCommand("SELECT s.name AS [SCHEMA], t.name AS [NAME], t.type AS type " +
                            "FROM sys.tables t " +
                            "INNER JOIN sys.schemas s ON t.schema_id = s.schema_id " +
                            "ORDER BY s.name, t.name",connection);			
		    sqlQry.CommandType = CommandType.Text;
			var da = dataAccessProvider.CreateDataAdapter();
			da.SelectCommand = sqlQry;
			da.Fill(ds);
			return ds;
		}

		protected override DataSet ViewSchema(DataAccessProviderFactory dataAccessProvider, IDbConnection connection)
		{
			var ds = new DataSet();
		    var sqlQry = dataAccessProvider.CreateCommand("SELECT s.name AS [SCHEMA], v.name AS [NAME], v.type AS type " +
                          "FROM sys.views v  " +
                          "INNER JOIN sys.schemas s ON v.schema_id = s.schema_id " +
                          "ORDER BY s.name, v.name ", connection);
		    sqlQry.CommandType = CommandType.Text;
			var da = dataAccessProvider.CreateDataAdapter();
			da.SelectCommand = sqlQry;
			da.Fill(ds);
			return ds;
		}

		protected override Table CreateTable(Database database, DataRow row)
		{
			var table = new Table {
                ParentDatabase = database, 
                Schema = row["SCHEMA"].ToString(), 
                Name = row["NAME"].ToString()
            };
		    return table;
		}
	}
}