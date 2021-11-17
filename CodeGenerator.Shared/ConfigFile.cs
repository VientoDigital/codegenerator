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
                        instance = new ConfigFile
                        {
                            Languages = new List<Language>
                            {
                                new Language
                                {
                                    Name = ".NET",
                                    Selected = true,
                                    Mappings = new Dictionary<string, string>
                                    {
                                        //{ "bigint", "long" },
                                        //{ "bit", "bool" },
                                        //{ "char", "string" },
                                        //{ "date", "DateTime" },
                                        //{ "datetime", "DateTime" },
                                        //{ "decimal", "decimal" },
                                        //{ "double", "double" },
                                        //{ "float", "double" },
                                        //{ "image", "Image" },
                                        //{ "int", "int" },
                                        //{ "integer", "int" },
                                        //{ "nchar", "string" },
                                        //{ "ntext", "string" },
                                        //{ "number", "int" },
                                        //{ "numeric", "int" },
                                        //{ "nvarchar", "string" },
                                        //{ "real", "double" },
                                        //{ "smallint", "short" },
                                        //{ "text", "string" },
                                        //{ "time", "DateTime" },
                                        //{ "timestamp", "DateTime" },
                                        //{ "tinyint", "short" },
                                        //{ "uniqueidentifier", "Guid" },
                                        //{ "varchar", "string" },
                                        //{ "varchar2", "string" },
                                    }
                                }
                            }
                        };
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

        [JsonIgnore]
        public Language SelectedLanguage => Languages.FirstOrDefault(x => x.Selected) ?? Languages.First();

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Performance", "CA1822:Mark members as static", Justification = "<Pending>")]
        public void Save() => Instance.JsonSerialize(new JsonSerializerSettings { Formatting = Formatting.Indented }).ToFile(filePath);
    }

    public class Language
    {
        public string Name { get; set; }

        public bool Selected { get; set; }

        public IDictionary<string, string> Mappings { get; set; }
    }
}