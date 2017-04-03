using System;
using System.Collections;
using System.IO;
using System.Xml;
using System.Xml.XPath;
using iCodeGenerator.ConfigurationManager;

namespace iCodeGenerator.DataTypeConverter
{
    public class DataTypeManager
    {
        private XmlDocument _Document = null;
        private static string _Uri = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location) + Path.DirectorySeparatorChar + "DataTypeMapping.xml";

        internal static string Uri
        {
            get { return _Uri; }
        }

        public DataTypeManager() : this(Uri)
        {
        }

        public DataTypeManager(string uri)
        {
            _Uri = uri;
            if (!DataMappingFileExists())
            {
                _Uri = @"C:\temp\" + "DataTypeMapping.xml";
            }
            try
            {
                _Document = new XmlDocument();
                _Document.Load(_Uri);
            }
            catch (Exception ex)
            {
                throw new DataTypeManagerException(string.Format("Error Loading Data Mapping Values. File expected at {0}", _Uri), ex);
            }
        }

        private static bool DataMappingFileExists()
        {
            return File.Exists(_Uri);
        }

        public LanguageCollection Languages
        {
            get
            {
                LanguageCollection collection = new LanguageCollection();
                XmlNode root = _Document.DocumentElement;
                XPathNavigator nav = root.CreateNavigator();
                XPathNodeIterator nodeIterator = nav.Select("/DataTypes/Language");
                while (nodeIterator.MoveNext())
                {
                    collection.Add(new Language(nodeIterator.Current.GetAttribute("name", "")));
                }
                return collection;
            }
        }

        public Language SelectedLanguage
        {
            get
            {
                XmlNode root = _Document.DocumentElement;
                XPathNavigator nav = root.CreateNavigator();
                XPathNodeIterator nodeIterator = nav.Select("/DataTypes/Language[@selected=\"true\"]");
                nodeIterator.MoveNext();
                return new Language(nodeIterator.Current.GetAttribute("name", ""));
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
    }
}