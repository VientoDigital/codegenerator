using CodeGenerator.Data.TypeConversion;
using NUnit.Framework;

namespace CodeGenerator.UnitTests
{
    [TestFixture]
    public class TestDataTypeManager
    {
        private DataTypeManager manager = null;

        [Test]
        public void TestConstructor()
        {
            manager = new DataTypeManager();
            Assert.IsNotNull(manager);
        }

        [Test]
        public void TestLanguages()
        {
            Assert.IsTrue(manager.Languages.Count > 0);
        }

        [Test]
        public void TestMappings()
        {
            Assert.IsTrue(manager.Languages[0].Mappings.Count > 0);
            Assert.IsTrue(manager.SelectedLanguage.Mappings.Count > 0);
        }
    }
}