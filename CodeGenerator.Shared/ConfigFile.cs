using System;
using System.Collections.Generic;
using System.IO;
using CodeGenerator.Shared.Extensions;
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

        public void Save() => Instance.JsonSerialize(new JsonSerializerSettings { Formatting = Formatting.Indented }).ToFile(filePath);
    }
}