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
        public static string Version = "2.1";
        private const string Url = "http://icodegenerator.net/version";
        private static Software software;

        public static bool IsNewUpdate => CheckUpdates();

        public static Software Software
        {
            get
            {
                try
                {
                    if (software != null)
                    {
                        return software;
                    }
                    var request = (HttpWebRequest)WebRequest.Create(Url);
                    var response = (HttpWebResponse)request.GetResponse();
                    var content = new StreamReader(response.GetResponseStream()).ReadToEnd();
                    software = JsonConvert.DeserializeObject<Software>(content);
                    return software;
                }
                catch
                {
                    return new Software() { Name = "iCodegenerator", Version = Version, };
                }
            }
        }

        private static bool CheckUpdates()
        {
            return Software.Version != Version;
        }
    }
}