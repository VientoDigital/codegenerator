using System.IO;
using System.Net;
using Newtonsoft.Json;

namespace CodeGenerator
{
    /// <summary>
    /// Summary description for UpdateChecker.
    /// </summary>
    public static class AppVersion
    {
        public const string Version = "3.0";

        private const string Url = "http://icodegenerator.net/version";
        private static VersionInfo latestVersion;

        public static bool HasNewUpdate => CheckForUpdate();

        public static VersionInfo LatestVersion
        {
            get
            {
                try
                {
                    if (latestVersion != null)
                    {
                        return latestVersion;
                    }

                    var request = (HttpWebRequest)WebRequest.Create(Url);
                    using (var response = (HttpWebResponse)request.GetResponse())
                    using (var streamReader = new StreamReader(response.GetResponseStream()))
                    {
                        string content = streamReader.ReadToEnd();
                        latestVersion = JsonConvert.DeserializeObject<VersionInfo>(content);
                        return latestVersion;
                    }
                }
                catch
                {
                    return new VersionInfo()
                    {
                        Name = "Code Generator",
                        Version = Version,
                    };
                }
            }
        }

        private static bool CheckForUpdate()
        {
            return LatestVersion.Version != Version;
        }
    }

    public class VersionInfo
    {
        public string Name { get; set; }

        public string Version { get; set; }
    }
}