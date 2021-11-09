using System.Collections;
using System.IO;
using System.Xml.Serialization;

namespace iCodeGenerator.ConfigurationManager
{
    /// <summary>
    /// Summary description for Configuration.
    /// </summary>
    public class Configuration
    {
        private const string iCodeConfigFile = @"C:\iCodeConfig.xml";
        private static ConfigFile configFile = new ConfigFile();
        private Hashtable cataTypes = new Hashtable();

        protected Configuration()
        {
        }

        public static Configuration Instance { get; set; } = new Configuration();

        public IDictionary DataTypes
        {
            get { return cataTypes; }
            set { cataTypes = (Hashtable)value; }
        }

        public string EndTag
        {
            get { return configFile.EndTag; }
            set { configFile.EndTag = value; }
        }

        public string StartTag
        {
            get { return configFile.StartTag; }
            set { configFile.StartTag = value; }
        }

        public void Open(string filename)
        {
            var serializer = new XmlSerializer(typeof(ConfigFile));
            TextReader reader = new StreamReader(filename);
            configFile = (ConfigFile)serializer.Deserialize(reader);
            reader.Close();
            LoadDataTypes();
        }

        public void Open()
        {
            Open(iCodeConfigFile);
        }

        public void Save(string filename)
        {
            AssignDataTypes();
            var serializer = new XmlSerializer(typeof(ConfigFile));
            TextWriter writer = new StreamWriter(filename);
            serializer.Serialize(writer, configFile);
            writer.Close();
        }

        public void Save()
        {
            Save(iCodeConfigFile);
        }

        private static DataTypes GetConfigDataTypes()
        {
            DataTypes dataTypes;
            if (configFile.DataTypesCollection.Count == 1)
            {
                dataTypes = configFile.DataTypesCollection[0];
            }
            else
            {
                dataTypes = new DataTypes();
            }
            return dataTypes;
        }

        private DataTypes AssignDataTypes()
        {
            var dataTypes = GetConfigDataTypes();
            dataTypes.Clear();
            foreach (string key in cataTypes.Keys)
            {
                dataTypes.Add(new SqlType
                {
                    name = key,
                    value = cataTypes[key].ToString()
                });
            }
            return dataTypes;
        }

        private void LoadDataTypes()
        {
            var dataTypes = GetConfigDataTypes();
            foreach (SqlType sqlType in dataTypes.SqlTypeCollection)
            {
                if (!cataTypes.ContainsKey(sqlType.name))
                {
                    cataTypes.Add(sqlType.name, sqlType.value);
                }
            }
        }
    }
}