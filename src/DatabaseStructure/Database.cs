using System.ComponentModel;
using iCodeGenerator.GenericDataAccess;

namespace iCodeGenerator.DatabaseStructure
{	
	public class Database
	{
		
		private string _name;		
		private TableStrategy _strategy;
		private bool _reload;
		private TableCollection _tables;
		
		private TableCollection _views;

		public Database()
		{
			if(Server.ProviderType == DataProviderType.SqlClient)
			{
				_strategy = new TableStrategySQLServer();
			}
			else if(Server.ProviderType == DataProviderType.MySql)
			{
				_strategy = new TableStrategyMySQL();
			}
			else if(Server.ProviderType == DataProviderType.PostgresSql)
			{
				_strategy = new TableStrategyPostgres();
			}
			else if (Server.ProviderType == DataProviderType.Oracle)
			{
				_strategy = new TableStrategyOracle();
			}
		}

		public void Reload()
		{
			_reload = true;
		}

		[CategoryAttribute("Database"),
		ReadOnlyAttribute(true)]
		public string Name
		{
			get { return _name; }
			set { _name = value; }
		}

		[BrowsableAttribute(false),
		DefaultValueAttribute(false)]
		public TableCollection Tables
		{
			get
			{
				if(_reload || _tables == null)
				{
					_tables = _strategy.GetTables(this);
					_reload = false;
				}
				return _tables;
			}
		}

		[BrowsableAttribute(false),
		DefaultValueAttribute(false)]
		public TableCollection Views
		{
			get
			{
				if (_reload || _views == null)
				{
					_views = _strategy.GetViews(this);
					_reload = false;
				}
				return _views;
			}
		}
	}
}
