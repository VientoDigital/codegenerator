using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Extenso;
using Newtonsoft.Json;

namespace CodeGenerator
{
    public class ConfigFile
    {
        private static readonly string filePath = $"{AppDomain.CurrentDomain.BaseDirectory}Config.js";

        private static ConfigFile instance = null;

        public static ConfigFile Instance
        {
            get
            {
                if (instance == null)
                {
                    if (!File.Exists(filePath))
                    {
                        instance = new ConfigFile();
                        return instance;
                    }

                    instance = File.ReadAllText(filePath).JsonDeserialize<ConfigFile>();
                }
                return instance;
            }
        }

        public ICollection<string> ConnectionStrings { get; set; } = new List<string>();

        public IDictionary<string, string> CustomValues { get; set; } = new Dictionary<string, string>();

        public ICollection<Language> Languages { get; set; } = new List<Language>();

        public string SelectedLanguageName { get; set; } = "C#";

        [JsonIgnore]
        public Language SelectedLanguage
        {
            get
            {
                if (SelectedLanguageName.In(".NET", "C#", "VB"))
                {
                    return new Language { Name = SelectedLanguageName };
                }
                return Languages.FirstOrDefault(x => x.Name == SelectedLanguageName) ?? Languages.First();
            }
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Performance", "CA1822:Mark members as static", Justification = "<Pending>")]
        public void Save() => Instance.JsonSerialize(new JsonSerializerSettings { Formatting = Formatting.Indented }).ToFile(filePath);
    }

    public class Language
    {
        public string Name { get; set; }

        public IDictionary<string, string> Mappings { get; set; }
    }
}