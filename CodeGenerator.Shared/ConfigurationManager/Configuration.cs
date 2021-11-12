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
        private const string configFilePath = @"C:\iCodeConfig.xml";
        private static ConfigFile configFile = new ConfigFile();
        private Hashtable dataTypes = new Hashtable();

        public static Configuration Instance { get; set; } = new Configuration();

        public IDictionary DataTypes
        {
            get => dataTypes;
            set => dataTypes = (Hashtable)value;
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

        public void Open(string fileName)
        {
            configFile = new FileInfo(fileName).XmlDeserialize<ConfigFile>();
            LoadDataTypes();
        }

        public void Open()
        {
            Open(configFilePath);
        }

        public void Save(string fileName)
        {
            AssignDataTypes();
            configFile.XmlSerialize(fileName);
        }

        public void Save()
        {
            Save(configFilePath);
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
            foreach (string key in this.dataTypes.Keys)
            {
                dataTypes.Add(new SqlType
                {
                    name = key,
                    value = this.dataTypes[key].ToString()
                });
            }
            return dataTypes;
        }

        private void LoadDataTypes()
        {
            var dataTypes = GetConfigDataTypes();
            foreach (SqlType sqlType in dataTypes.SqlTypeCollection)
            {
                if (!this.dataTypes.ContainsKey(sqlType.name))
                {
                    this.dataTypes.Add(sqlType.name, sqlType.value);
                }
            }
        }
    }
}