using System.IO;
using System.Net;
using Newtonsoft.Json;

namespace iCodeGenerator.Updater
{
    public class Software
    {
        public string Name { get; set; }
        public string Version { get; set; }
    }
     
	/// <summary>
	/// Summary description for UpdateChecker.
	/// </summary>
	public class UpdateChecker
	{				
	    public static string Version = "2.0";
	    private const string Url = "http://icodegenerator.net/version";
     
	    public static bool IsNewUpdate
	    {
	        get
	        {	            	              
                    return CheckUpdates();            
	        }
	    }

	    private static bool CheckUpdates()
	    {
	        try
	        {
                var request = (HttpWebRequest)WebRequest.Create(Url);
                var response = (HttpWebResponse)request.GetResponse();
                var content = new StreamReader(response.GetResponseStream()).ReadToEnd();
                var software = JsonConvert.DeserializeObject<Software>(content);
                return software.Version == Version;
	        }
	        catch
	        {
	            return false;
	        }
	        
	    }
	}
}
