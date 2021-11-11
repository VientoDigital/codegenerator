using System;
using CodeGenerator.DatabaseStructure;
using CodeGenerator.GenericDataAccess;
using NUnit.Framework;

namespace CodeGenerator.UnitTests
{
    [TestFixture]
    public class TestDatabaseNavigator
    {
        private Server server;

        [SetUp]
        public void SetUp()
        {
            Server.ConnectionString = @"SERVER=(local);DATABASE=;UID=sa;PWD=m14m14;";
            Server.ProviderType = DataProviderType.SqlClient;
            server = new Server();
        }

        [Test]
        public void TestConstructor()
        {
            Assert.IsNotNull(server);
        }

        [Test]
        public void TestDatabaseCollection()
        {
            Assert.IsTrue(server.Databases.Count > 0);
        }

        [Test]
        public void TestDatabaseCollectionRemoveAndReload()
        {
            int count = server.Databases.Count;
            Console.Out.WriteLine(count);
            server.Databases.Remove(server.Databases[0]);
            int CountAfterRemove = count - 1;
            Assert.IsTrue(server.Databases.Count == CountAfterRemove);
            server.Reload();
            Assert.IsTrue(server.Databases.Count == count);
        }

        [Test]
        public void TestDatabaseTablesCollection()
        {
            Assert.IsTrue(server.Databases[0].Tables.Count > 0);
        }

        [Test]
        public void TestDatabaseTableColumnsCollection()
        {
            Table table = server.Databases[0].Tables[0];
            Assert.IsTrue(table.Columns.Count > 0);
        }

        [Test]
        public void TestDatabaseKeys()
        {
            Table table = server.Databases[0].Tables[0];
            Assert.IsTrue(table.Keys.Count > 0);
        }

        [Test]
        public void TestPrimaryKeyExists()
        {
            Table table = server.Databases[0].Tables[0];
            bool exists = false;
            foreach (Column column in table.Columns)
            {
                if (column.IsPrimaryKey)
                {
                    Console.Write(column.Name);
                    exists = true; break;
                }
            }
            Assert.IsTrue(exists);
        }
    }
}