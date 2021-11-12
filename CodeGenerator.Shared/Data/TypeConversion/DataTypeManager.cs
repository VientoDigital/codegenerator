using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Xml;

namespace CodeGenerator.Data.TypeConversion
{
    public class DataTypeManager
    {
        private static string uri = $"{Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)}{Path.DirectorySeparatorChar}DataTypeMapping.xml";
        private readonly XmlDocument xmlDocument = null;

        public DataTypeManager() : this(Uri)
        {
        }

        public DataTypeManager(string uri)
        {
            DataTypeManager.uri = uri;

            try
            {
                xmlDocument = new XmlDocument();
                xmlDocument.Load(DataTypeManager.uri);
            }
            catch (Exception x)
            {
                throw new DataTypeManagerException(string.Format("Error Loading Data Mapping Values. File expected at {0}", DataTypeManager.uri), x);
            }
        }

        public ICollection<Language> Languages
        {
            get
            {
                var collection = new List<Language>();
                var root = xmlDocument.DocumentElement;
                var nav = root.CreateNavigator();
                var nodeIterator = nav.Select("/DataTypes/Language");

                while (nodeIterator.MoveNext())
                {
                    collection.Add(new Language(nodeIterator.Current.GetAttribute("name", string.Empty)));
                }

                return collection;
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
                return new Language(nodeIterator.Current.GetAttribute("name", string.Empty));
            }
        }

        internal static string Uri => uri;
    }
}