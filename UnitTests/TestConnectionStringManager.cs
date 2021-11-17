using NUnit.Framework;

namespace CodeGenerator.UnitTests
{
    [TestFixture]
    public class TestConnectionStringManager
    {
        [Test]
        public void TestGetConnectionStrings()
        {
            Assert.IsTrue(ConfigFile.Instance.ConnectionStrings.Count >= 0);
        }

        [Test]
        public void TestAddConnection()
        {
            int numConn = ConfigFile.Instance.ConnectionStrings.Count;
            ConfigFile.Instance.ConnectionStrings.Add("Borrarme");
            ConfigFile.Instance.Save();
            Assert.IsTrue(numConn == (ConfigFile.Instance.ConnectionStrings.Count - 1));
        }
    }
}