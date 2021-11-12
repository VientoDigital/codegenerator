using System.Linq;
using NUnit.Framework;

namespace CodeGenerator.UnitTests
{
    [TestFixture]
    public class TestDataTypeManager
    {
        [Test]
        public void TestLanguages()
        {
            Assert.IsTrue(ConfigFile.Instance.Languages.Count > 0);
        }

        [Test]
        public void TestMappings()
        {
            Assert.IsTrue(ConfigFile.Instance.Languages.First().Mappings.Count > 0);
            Assert.IsTrue(ConfigFile.Instance.SelectedLanguage.Mappings.Count > 0);
        }
    }
}