using System;
using System.IO;
using CodeGenerator.ConfigurationManager;
using NUnit.Framework;

namespace CodeGenerator.UnitTests
{
    /// <summary>
    /// Summary description for TestConfiguration.
    /// </summary>
    [TestFixture]
    public class TestConfiguration
    {
        private const string configFile = @"C:\iCode.xml";

        [Test]
        public void TestOpen()
        {
            var config = Configuration.Instance;
            var config2 = Configuration.Instance;
            config.Open(configFile);
            Console.WriteLine(config.StartTag);
            Console.WriteLine(config.EndTag);
            Console.WriteLine(config2.StartTag);
            Console.WriteLine(config2.EndTag);
        }

        [Test]
        public void TestSave()
        {
            var config = Configuration.Instance;
            if (File.Exists(configFile))
            {
                config.Open(configFile);
            }

            config.StartTag = "<";
            config.EndTag = ">";
            foreach (string key in config.DataTypes.Keys)
            {
                Console.WriteLine(config.DataTypes[key]);
            }

            config.Save(configFile);
            Assert.IsTrue(File.Exists(configFile));
        }
    }
}