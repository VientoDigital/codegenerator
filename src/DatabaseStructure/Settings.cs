using System;
using System.IO;
using System.Reflection;
using System.Xml.Serialization;
using iCodeGenerator.GenericDataAccess;

namespace iCodeGenerator.DatabaseStructure
{
    [Serializable]
    public class Settings
    {
        private static readonly string location = Path.GetFullPath(Assembly.GetExecutingAssembly().Location) + Path.DirectorySeparatorChar + "Settings.xml";

        private static Settings instance = new Settings();
        private string connectionString = string.Empty;

        static Settings()
        {
        }

        public static Settings Instance => instance;
        public static string Location => location;

        public string ConnectionString
        {
            get
            {
                //_connectionString = @"SERVER=(local)\NetSDK;DATABASE=;UID=sa;PWD=;";
                //_connectionString = @"Data Source=vdominguez;Password=victor;User ID=vdominguez;Location=32.76.173.49;";
                if (connectionString.Length == 0)
                {
                    //_connectionString = @"Data Source=vdominguez;Password=victor;User ID=vdominguez;Location=32.76.173.49;";
                    connectionString = @"Data Source=test;Password=;User ID=root;Location=localhost;";
                }
                return connectionString;
            }
            set { connectionString = value; }
        }

        public string DataTypeMappingFile => Path.GetFullPath(Assembly.GetExecutingAssembly().Location) + Path.DirectorySeparatorChar + "DataTypeMapping.xml";

        public DataProviderType ProviderType { get; set; } = DataProviderType.MySql;

        public static void Deserialize()
        {
            var xmlSerializer = new XmlSerializer(typeof(Settings));

            if (!IsNew())
            {
                FileStream fileStream = null;

                try
                {
                    fileStream = File.Open(Location, FileMode.Open, FileAccess.Read);
                    instance = (Settings)xmlSerializer.Deserialize(fileStream);
                }
                catch
                {
                    Serialize();
                }
                finally
                {
                    fileStream.Close();
                }
            }
        }

        public static bool IsNew() => !File.Exists(Location);

        public static void Serialize()
        {
            var xmlSerializer = new XmlSerializer(instance.GetType());
            var fileStream = File.Open(Location, FileMode.Create, FileAccess.Write, FileShare.None);
            try
            {
                xmlSerializer.Serialize(fileStream, instance);
            }
            catch
            {
            }
            finally
            {
                fileStream.Close();
            }
        }
    }
}