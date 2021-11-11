using System;
using System.Collections;
using System.IO;
using System.Reflection;
using System.Xml;
using CodeGenerator.ConfigurationManager;

namespace CodeGenerator.Data.TypeConversion
{
    public class DataTypeManager
    {
        private static string uri = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + Path.DirectorySeparatorChar + "DataTypeMapping.xml";
        private XmlDocument xmlDocument = null;

        public DataTypeManager() : this(Uri)
        {
        }

        public DataTypeManager(string uri)
        {
            DataTypeManager.uri = uri;

            if (!DataMappingFileExists())
            {
                DataTypeManager.uri = @"C:\temp\" + "DataTypeMapping.xml";
            }
            try
            {
                xmlDocument = new XmlDocument();
                xmlDocument.Load(DataTypeManager.uri);
            }
            catch (Exception ex)
            {
                throw new DataTypeManagerException(string.Format("Error Loading Data Mapping Values. File expected at {0}", DataTypeManager.uri), ex);
            }
        }

        public LanguageCollection Languages
        {
            get
            {
                var collection = new LanguageCollection();
                var root = xmlDocument.DocumentElement;
                var nav = root.CreateNavigator();
                var nodeIterator = nav.Select("/DataTypes/Language");

                while (nodeIterator.MoveNext())
                {
                    collection.Add(new Language(nodeIterator.Current.GetAttribute("name", "")));
                }

                return collection;
            }
        }

        public IDictionary Mappings
        {
            get
            {
                if (!DataMappingFileExists())
                {
                    return SelectedLanguage.Mappings;
                }
                else
                {
                    return Configuration.Instance.DataTypes;
                }
            }
        }

        public Language SelectedLanguage
        {
            get
            {
                var root = xmlDocument.DocumentElement;
                var nav = root.CreateNavigator();
                var nodeIterator = nav.Select("/DataTypes/Language[@selected=\"true\"]");
                nodeIterator.MoveNext();
                return new Language(nodeIterator.Current.GetAttribute("name", ""));
            }
        }

        internal static string Uri => uri;

        private static bool DataMappingFileExists()
        {
            return File.Exists(uri);
        }
    }
}