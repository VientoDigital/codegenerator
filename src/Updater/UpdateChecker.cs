using System;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Xml;

namespace iCodeGenerator.Updater
{
	/// <summary>
	/// Summary description for UpdateChecker.
	/// </summary>
	public class UpdateChecker
	{				
		private string _AvailableVersion;
		private string _AppFileURL;
		private string _LatestChanges;
		private string _ChangeLogURL;
		private HttpWebRequest _Request = null;
		private HttpWebResponse _Response = null;
		private XmlDocument _XmlDocument = null;


		public string Version
		{
			get { return _AvailableVersion; }			
		}

		public string ApplicationUrl
		{
			get { return _AppFileURL; }			
		}

		public string LatestChanges
		{
			get { return _LatestChanges; }			
		}

		public string ChangeLogUrl
		{
			get { return _ChangeLogURL; }			
		}

		public UpdateChecker()
		{
			
		}

		public bool LoadConfiguration(string url)
		{
			try
			{
				_Request = (HttpWebRequest) HttpWebRequest.Create(url);
				_Response = (HttpWebResponse) _Request.GetResponse();
				Stream stream = _Response.GetResponseStream();
				_XmlDocument = new XmlDocument();
				_XmlDocument.Load(stream);
				_AvailableVersion = _XmlDocument.SelectSingleNode(@"//AvailableVersion").InnerText;			
				_AppFileURL = _XmlDocument.SelectSingleNode(@"//AppFileURL").InnerText;			
				XmlNode LatestChangesNode = _XmlDocument.SelectSingleNode(@"//LatestChanges");
				if(LatestChangesNode != null)
				{
					_LatestChanges = LatestChangesNode.InnerText;
				}				
				else
				{
					_LatestChanges = "";
				}
							
				XmlNode ChangeLogURLNode = _XmlDocument.SelectSingleNode(@"//ChangeLogURL");
				if(ChangeLogURLNode != null)
				{
					_ChangeLogURL = ChangeLogURLNode.InnerText;
				}				
				else
				{
					_ChangeLogURL = "";
				}				
			}
			catch 
			{
				Debug.WriteLine("Failed to read file at " + url);
				return false;
			}
			return true;
		}

	}
}
