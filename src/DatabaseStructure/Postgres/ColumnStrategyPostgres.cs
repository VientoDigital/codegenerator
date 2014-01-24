using System;
using System.Data;
using iCodeGenerator.GenericDataAccess;
using iCodeGenerator.DatabaseStructure;

namespace iCodeGenerator.DatabaseStructure
{
	
	public class ColumnStrategyPostgres : ColumnStrategy
	{
		protected override DataSet ColumnSchema(Table table, DataAccessProviderFactory dataProvider, IDbConnection connection)
		{
			DataSet ds = new DataSet();
			int tableId = GetTableId(table.Name,dataProvider, connection);
			IDbCommand sqlCommand = dataProvider.CreateCommand("SELECT a.attname,t.typname as atttype, " +
				"(SELECT substring(d.adsrc for 128) FROM pg_catalog.pg_attrdef d " + 
				"WHERE d.adrelid = a.attrelid AND d.adnum = a.attnum AND a.atthasdef)as attdef, a.attlen, a.atttypmod,a.attnotnull, a.attnum " +
				"FROM pg_catalog.pg_attribute a, pg_catalog.pg_type t " +
				"WHERE a.attrelid = '" + tableId + "' AND a.attnum > 0 AND NOT a.attisdropped " +
				"AND t.oid = a.atttypid " +
				"ORDER BY a.attnum",connection);
			sqlCommand.CommandType = CommandType.Text;			
			IDbDataAdapter da = dataProvider.CreateDataAdapter();
			da.SelectCommand = sqlCommand;
			da.Fill(ds);
			return ds;
		}

		private static int GetTableId(string tablename, DataAccessProviderFactory dataProvider, IDbConnection connection)
		{
			IDbCommand sqlCommand = dataProvider.CreateCommand(@"SELECT c.oid," +
				@"n.nspname, " + 
				@"c.relname " +
				@"FROM pg_catalog.pg_class c " +
				@"LEFT JOIN pg_catalog.pg_namespace n ON n.oid = c.relnamespace " +
				@"WHERE pg_catalog.pg_table_is_visible(c.oid) " +
				@"AND c.relname ~ '^" + tablename + "$' " +
				@"ORDER BY 2, 3;", connection);
			
			return Convert.ToInt32(sqlCommand.ExecuteScalar());			
		}

		protected override Column CreateColumn(DataRow row)
		{
			Column column = new Column();
			column.Name = row["attname"].ToString();
			column.Type = row["atttype"].ToString();
			int length = Convert.ToInt32( row["attlen"] );
			column.Length = length >= 0? length : Convert.ToInt32(row["atttypmod"]) - 4;
			column.Nullable = !Convert.ToBoolean( row["attnotnull"] );
			column.Default = row["attdef"].ToString();
			return column;
		}

		protected override DataSet KeySchema(Table table, DataAccessProviderFactory dataProvider, IDbConnection connection)
		{
			DataSet ds = new DataSet();
			IDbCommand sqlCommand = dataProvider.CreateCommand("SELECT c2.relname, i.indisprimary, i.indisunique, i.indisclustered, pg_catalog.pg_get_indexdef(i.indexrelid, 0, true)" +
				"FROM pg_catalog.pg_class c, pg_catalog.pg_class c2, pg_catalog.pg_index i " +
				"WHERE c.oid = '" + GetTableId(table.Name, dataProvider, connection) + "' AND c.oid = i.indrelid AND i.indexrelid = c2.oid " +
				"ORDER BY i.indisprimary DESC, i.indisunique DESC, c2.relname",connection); 
			sqlCommand.CommandType = CommandType.Text;			
			IDbDataAdapter da = dataProvider.CreateDataAdapter();
			da.SelectCommand = sqlCommand;
			da.Fill(ds);
			return ds;
		}

		protected override Key CreateKey(DataRow row)
		{
			Key key = new Key();
			key.Name = row["relname"].ToString();
			key.ColumnName = row["relname"].ToString();
			key.IsPrimary = Convert.ToBoolean(row["indisprimary"]);
			return key;
		}
	}
}
