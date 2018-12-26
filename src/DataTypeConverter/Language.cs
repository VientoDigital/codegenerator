using System.Collections;
using System.Xml;
using System.Xml.XPath;

namespace iCodeGenerator.DataTypeConverter
{
    public class Language
    {
        private string _Name;

        public string Name
        {
            get { return _Name; }
            set { _Name = value; }
        }

        public Language()
        {
        }

        public Language(string languageName)
        {
            _Name = languageName;
        }

        public IDictionary Mappings
        {
            get
            {
                Hashtable mappings = new Hashtable();
                XmlDocument doc = new XmlDocument();
                doc.Load(DataTypeManager.Uri);
                XmlNode root = doc.DocumentElement;

                XPathNavigator nav = root.CreateNavigator();
                XPathNodeIterator nodeIterator = nav.Select("/DataTypes/Language[@name = \"" + _Name + "\"]/SqlType");
                while (nodeIterator.MoveNext())
                {
                    mappings.Add(nodeIterator.Current.GetAttribute("name", ""), nodeIterator.Current.Value.Trim());
                }
                return mappings;
            }
        }
    }
}