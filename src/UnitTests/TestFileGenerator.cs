using System;
using iCodeGenerator.DatabaseStructure;
using iCodeGenerator.Generator;
using iCodeGenerator.GenericDataAccess;
using NUnit.Framework;

namespace iCodeGenerator.UnitTests
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
            Server.ConnectionString = @"SERVER=(local);DATABASE=;UID=sa;PWD=m14m14;";
            Server.ProviderType = DataProviderType.SqlClient;
            Server server = new Server();
            Client client = new Client();
            _ = client.StartDelimiter;
            _ = client.EndingDelimiter;
            client.StartDelimiter = string.Empty;
            client.EndingDelimiter = string.Empty;
            Console.WriteLine(client.Parse(server.Databases[0].Tables[0], "DATABASE.NAME_TABLE.NAMEForm.aspx"));
        }
    }
}