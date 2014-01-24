using System.Data;
using iCodeGenerator.GenericDataAccess;

namespace iCodeGenerator.DatabaseStructure
{
	public abstract class ColumnStrategy
	{

		private IDbConnection _connection;
		private DataAccessProviderFactory _dataAccessProvider;
		private ColumnCollection _columns;
		private KeyCollection _keys;

		protected ColumnStrategy()
		{
			_dataAccessProvider = new DataAccessProviderFactory(Server.ProviderType);
			_connection = _dataAccessProvider.CreateConnection(Server.ConnectionString);
			_columns = new ColumnCollection();
			_keys = new KeyCollection();
		}

		protected ColumnCollection _Columns
		{
			get { return _columns; }
		}

		protected KeyCollection _Keys
		{
			get { return _keys; }
		}

		public ColumnCollection GetColumns(Table table)
		{
			if(_connection.State == ConnectionState.Closed)
			{
				_connection.Open();
			}
			
			if (Server.ProviderType != DataProviderType.Oracle)
			{
			_connection.ChangeDatabase(table.ParentDatabase.Name);
			}

			DataSet ds = ColumnSchema(table,_dataAccessProvider,_connection);
			foreach(DataRow row in ds.Tables[0].Rows)
			{
				Column column = CreateColumn(row);
				column.SetParentTable(table);
				foreach(Key key in table.Keys)
				{
					if(key.IsPrimary)
					{
						if(key.ColumnName == column.Name)
						{
							column.IsPrimaryKey = true;
							continue;
						}
					}
				}
				_Columns.Add(column);
				
			}
			_connection.Close();
			return _Columns;
		}

		protected abstract DataSet ColumnSchema(Table table, DataAccessProviderFactory dataAccessProvider, IDbConnection connection);
		protected abstract Column CreateColumn(DataRow row);

		public KeyCollection GetKeys(Table table)
		{
			if(_connection.State == ConnectionState.Closed)
			{
				_connection.Open();
			}
			if (Server.ProviderType != DataProviderType.Oracle)
			{
			_connection.ChangeDatabase(table.ParentDatabase.Name);
			}
			DataSet ds = KeySchema(table,_dataAccessProvider,_connection);
			foreach(DataRow row in ds.Tables[0].Rows)
			{
				Key key = CreateKey(row);
				_Keys.Add(key);				
			}
			_connection.Close();
			return _Keys;	
		}

		protected abstract DataSet KeySchema(Table table, DataAccessProviderFactory dataAccessProvider, IDbConnection connection);
		protected abstract Key CreateKey(DataRow row);

	}
}
