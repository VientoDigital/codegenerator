using System;
using CodeGenerator.Data.Structure;
using CodeGenerator.Generator;
using CodeGenerator.Data;
using NUnit.Framework;
using System.Linq;

namespace CodeGenerator.UnitTests
{
    /// <summary>
    /// Summary description for Test.
    /// </summary>
    [TestFixture]
    public class TestFileGenerator
    {
        [Test]
        public void TestFilenameGenerator()
        {
            Server.ConnectionString = @"Server=.;Database=master;Integrated Security=SSPI;";
            Server.ProviderType = DataProviderType.SqlClient;
            Server server = new Server();
            var client = new Client();
            _ = client.StartDelimiter;
            _ = client.EndingDelimiter;
            client.StartDelimiter = string.Empty;
            client.EndingDelimiter = string.Empty;
            Console.WriteLine(client.Parse(server.Databases.First().Tables.First(), "DATABASE.NAME_TABLE.NAMEForm.aspx"));
        }
    }
}