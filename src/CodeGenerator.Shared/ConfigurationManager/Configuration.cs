using System.Collections;
using System.IO;
using CodeGenerator.Shared.Extensions;

namespace CodeGenerator.ConfigurationManager
{
    /// <summary>
    /// Summary description for Configuration.
    /// </summary>
    public class Configuration
    {
        private const string iCodeConfigFile = @"C:\iCodeConfig.xml";
        private static ConfigFile configFile = new ConfigFile();
        private Hashtable cataTypes = new Hashtable();

        public static Configuration Instance { get; set; } = new Configuration();

        public IDictionary DataTypes
        {
            get => cataTypes;
            set => cataTypes = (Hashtable)value;
        }

        public string EndTag
        {
            get => configFile.EndTag;
            set => configFile.EndTag = value;
        }

        public string StartTag
        {
            get => configFile.StartTag;
            set => configFile.StartTag = value;
        }

        public void Open(string filename)
        {
            configFile = new FileInfo(filename).XmlDeserialize<ConfigFile>();
            LoadDataTypes();
        }

        public void Open()
        {
            Open(iCodeConfigFile);
        }

        public void Save(string filename)
        {
            AssignDataTypes();
            configFile.XmlSerialize(filename);
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