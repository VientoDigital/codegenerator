using System;
using System.Xml.Serialization;
using System.IO;
using iCodeGenerator.GenericDataAccess;

namespace iCodeGenerator.DatabaseStructure
{
	[Serializable]
	public class Settings
	{
		private readonly static string _location = Path.GetFullPath(System.Reflection.Assembly.GetExecutingAssembly().Location) + Path.DirectorySeparatorChar + "Settings.xml";
        public static string Location
        {
            get
            {
                return _location;
            }
        }


		public static bool IsNew()
		{
			return !System.IO.File.Exists(Location);
		}
		public static void Deserialize()
		{
			XmlSerializer xs = new XmlSerializer(typeof(Settings));			
			
			if(!IsNew())
			{
				FileStream fs = null;

				try
				{	
					fs = System.IO.File.Open(Location, FileMode.Open, FileAccess.Read);

					instance = (Settings)xs.Deserialize(fs);
				}
				catch
				{
					Settings.Serialize();
				}
				finally
				{
					fs.Close();
				}
			}
		}

		public static void Serialize()
		{
			FileStream fs = null;
			XmlSerializer xs = new XmlSerializer(instance.GetType());
			fs = System.IO.File.Open(Location, FileMode.Create, FileAccess.Write, FileShare.None);
			try
			{
				xs.Serialize(fs, instance);
			}
			catch
			{
			}
			finally
			{
				fs.Close();
			}
		}
		static Settings()
		{
		}

		private static Settings instance = new Settings();
		private string _connectionString = "";
		private DataProviderType _providerType = DataProviderType.MySql;

		public static Settings Instance
		{
			get { return instance; }
		}

		public string DataTypeMappingFile
		{
			get
			{
				return Path.GetFullPath(System.Reflection.Assembly.GetExecutingAssembly().Location) + Path.DirectorySeparatorChar + "DataTypeMapping.xml";
			}
		}

		public string ConnectionString
		{
			get
			{
				//_connectionString = @"SERVER=(local)\NetSDK;DATABASE=;UID=sa;PWD=;";
				//_connectionString = @"Data Source=vdominguez;Password=victor;User ID=vdominguez;Location=32.76.173.49;";
				if(_connectionString.Length==0)
				{
					//_connectionString = @"Data Source=vdominguez;Password=victor;User ID=vdominguez;Location=32.76.173.49;";
					_connectionString = @"Data Source=test;Password=;User ID=root;Location=localhost;";
				}				
				return _connectionString;
			}
			set{ _connectionString = value; }
		}

		public DataProviderType ProviderType
		{			
			get { return _providerType; }
			set { _providerType = value;}
		}
	}
}
