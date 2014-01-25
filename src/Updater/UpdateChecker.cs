using System.IO;
using System.Net;
using System.Runtime.Remoting.Messaging;
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
	    private static Software _software;
     
	    public static bool IsNewUpdate
	    {
	        get
	        {	            	              
                    return CheckUpdates();            
	        }
	    }

	    public static Software Software
	    {
	        get
	        {
                try
	            { 
                if(_software!=null)
	            return _software;
                var request = (HttpWebRequest)WebRequest.Create(Url);
                var response = (HttpWebResponse)request.GetResponse();
                var content = new StreamReader(response.GetResponseStream()).ReadToEnd();
                _software = JsonConvert.DeserializeObject<Software>(content);
	            return _software;
                }
                catch
                {
                    return new Software() {Name = "iCodegenerator",Version = Version,};
                }
	        }
	        
	    }

	    private static bool CheckUpdates()
	    {
	                       
                return Software.Version != Version;
	        
	        
	    }
	}
}
