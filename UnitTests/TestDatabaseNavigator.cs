using System;
using CodeGenerator.Data.Structure;
using CodeGenerator.Data;
using NUnit.Framework;
using System.Linq;

namespace CodeGenerator.UnitTests
{
    [TestFixture]
    public class TestDatabaseNavigator
    {
        private Server server;

        [SetUp]
        public void SetUp()
        {
            Server.ConnectionString = @"Server=.;Database=master;Integrated Security=SSPI;";
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
            server.Databases.Remove(server.Databases.First());

            int countAfterRemove = count - 1;
            Assert.IsTrue(server.Databases.Count == countAfterRemove);
            server.Reload();
            Assert.IsTrue(server.Databases.Count == count);
        }

        [Test]
        public void TestDatabaseTablesCollection()
        {
            Assert.IsTrue(server.Databases.First().Tables.Count > 0);
        }

        [Test]
        public void TestDatabaseTableColumnsCollection()
        {
            var table = server.Databases.First().Tables.First();
            Assert.IsTrue(table.Columns.Count > 0);
        }

        [Test]
        public void TestDatabaseKeys()
        {
            var table = server.Databases.First().Tables.First();
            Assert.IsTrue(table.Keys.Count > 0);
        }

        [Test]
        public void TestPrimaryKeyExists()
        {
            var table = server.Databases.First().Tables.First();
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