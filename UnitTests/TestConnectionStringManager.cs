using CodeGenerator.Data;
using NUnit.Framework;

namespace CodeGenerator.UnitTests
{
    [TestFixture]
    public class TestConnectionStringManager
    {
        [Test]
        public void TestGetConnectionStrings()
        {
            Assert.IsTrue(ConnectionStringManager.Instance.GetConnectionStrings().Length >= 0);
        }

        [Test]
        public void TestAddConnection()
        {
            int numConn = ConnectionStringManager.Instance.GetConnectionStrings().Length;
            ConnectionStringManager.Instance.Add("Borrarme");
            Assert.IsTrue(numConn == (ConnectionStringManager.Instance.GetConnectionStrings().Length - 1));
        }
    }
}