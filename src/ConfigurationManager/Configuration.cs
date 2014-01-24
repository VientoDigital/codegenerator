using System;
using System.Collections;
using System.Data;
using System.IO;
using System.Xml.Serialization;

namespace iCodeGenerator.ConfigurationManager
{
	/// <summary>
	/// Summary description for Configuration.
	/// </summary>
	public class Configuration
	{

		const string iCodeConfigFile = @"C:\iCodeConfig.xml";
		private static Configuration _Instance = new Configuration();
		private static ConfigFile _ConfigFile = new ConfigFile();
		private Hashtable _DataTypes = new Hashtable();

		public static Configuration Instance
		{
			get{ return _Instance; }
			set{ _Instance = value; }
		}

		protected Configuration() { }

		public void Save(string filename)
		{
			AssignDataTypes();
			XmlSerializer serializer = new XmlSerializer(typeof(ConfigFile));
			TextWriter writer = new StreamWriter(filename);
			serializer.Serialize(writer, _ConfigFile);
			writer.Close();
		}

		public void Open(string filename)
		{
			XmlSerializer serializer = new XmlSerializer(typeof(ConfigFile));
			TextReader reader = new StreamReader(filename);
			_ConfigFile = (ConfigFile) serializer.Deserialize(reader);
			reader.Close();
			LoadDataTypes();
		}

		private DataTypes AssignDataTypes()
		{
			DataTypes dts = GetConfigDataTypes();
			dts.Clear();
			foreach(string key in _DataTypes.Keys)
			{
				SqlType type = new SqlType();
				type.name = key;
				type.value = _DataTypes[key].ToString();
				dts.Add(type);	
			}
			return dts;
		}

		private void LoadDataTypes()
		{
			DataTypes dts = GetConfigDataTypes();
			foreach(SqlType sqlt in dts.SqlTypeCollection)
			{
				if(!_DataTypes.ContainsKey(sqlt.name))
					_DataTypes.Add(sqlt.name,sqlt.value);	
			}
		}

		private static DataTypes GetConfigDataTypes()
		{
			DataTypes dts;
			if(_ConfigFile.DataTypesCollection.Count==1)
				dts = _ConfigFile.DataTypesCollection[0];
			else
				dts = new DataTypes();
			return dts;
		}

		public void Open()
		{
			Open(iCodeConfigFile);
		}

		public void Save()
		{
			Save(iCodeConfigFile);
		}
		public string StartTag
		{
			get{ return _ConfigFile.StartTag; }
			set{ _ConfigFile.StartTag = value; }
		}

		public string EndTag
		{
			get{ return _ConfigFile.EndTag; }
			set{ _ConfigFile.EndTag = value; }
		}

		public IDictionary DataTypes
		{
			get{ return _DataTypes; }	
			set{ _DataTypes = (Hashtable) value; }
		}

	}
}
