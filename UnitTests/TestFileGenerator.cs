using System;
using System.Linq;
using CodeGenerator.Data;
using CodeGenerator.Data.Structure;
using CodeGenerator.Generator;
using NUnit.Framework;

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
            Server.ProviderType = ProviderType.SqlServer;
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