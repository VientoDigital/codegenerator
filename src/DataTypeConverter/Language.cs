using System.Collections;
using System.Xml;
using System.Xml.XPath;

namespace iCodeGenerator.DataTypeConverter
{
    public class Language
    {
        public string Name { get; set; }

        public Language()
        {
        }

        public Language(string languageName)
        {
            Name = languageName;
        }

        public IDictionary Mappings
        {
            get
            {
                var mappings = new Hashtable();
                var xmlDocument = new XmlDocument();
                xmlDocument.Load(DataTypeManager.Uri);
                var root = xmlDocument.DocumentElement;
                var nav = root.CreateNavigator();
                var nodeIterator = nav.Select("/DataTypes/Language[@name = \"" + Name + "\"]/SqlType");

                while (nodeIterator.MoveNext())
                {
                    mappings.Add(nodeIterator.Current.GetAttribute("name", ""), nodeIterator.Current.Value.Trim());
                }

                return mappings;
            }
        }
    }
}