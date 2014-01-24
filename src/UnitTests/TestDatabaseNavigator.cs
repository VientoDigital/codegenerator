using System;
using iCodeGenerator.GenericDataAccess;
using iCodeGenerator.DatabaseStructure;
using NUnit.Framework;

namespace iCodeGenerator.UnitTests
{
	[TestFixture]
	public class TestDatabaseNavigator
	{
		private Server _server;

		[SetUp]
		public void SetUp()
		{
			Server.ConnectionString = @"SERVER=(local);DATABASE=;UID=sa;PWD=m14m14;";
			Server.ProviderType = DataProviderType.SqlClient;
			_server = new Server();			
		}

		[Test]
		public void TestConstructor()
		{
			Assert.IsNotNull(_server);
		}

		[Test]
		public void TestDatabaseCollection()
		{
			Assert.IsTrue(_server.Databases.Count>0); 
		}

		[Test]
		public void TestDatabaseCollectionRemoveAndReload()
		{
			int count = _server.Databases.Count;
			Console.Out.WriteLine(count);
			_server.Databases.Remove(_server.Databases[0]);
			int CountAfterRemove = count-1;
			Assert.IsTrue(_server.Databases.Count == CountAfterRemove);
			_server.Reload();
			Assert.IsTrue(_server.Databases.Count == count);
		}

		[Test]
		public void TestDatabaseTablesCollection()
		{
			Assert.IsTrue(_server.Databases[0].Tables.Count>0);
		}

		[Test]
		public void TestDatabaseTableColumnsCollection()
		{
			Table table = _server.Databases[0].Tables[0];			
			Assert.IsTrue(table.Columns.Count>0);
		}

		[Test]
		public void TestDatabaseKeys()
		{
			Table table = _server.Databases[0].Tables[0];
			Assert.IsTrue(table.Keys.Count>0);
		}

		[Test]
		public void TestPrimaryKeyExists()
		{
			Table table = _server.Databases[0].Tables[0];
			bool exists = false;
			foreach(Column column in table.Columns)
			{
				if(column.IsPrimaryKey){
					System.Console.Write(column.Name);
					exists = true; break; 
				}
			}
			Assert.IsTrue(exists);
		}
		
	}
}
