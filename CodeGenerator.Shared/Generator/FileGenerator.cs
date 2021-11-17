using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using CodeGenerator.Data.Structure;

namespace CodeGenerator.Generator
{
    /// <summary>
    /// Summary description for FileGenerator.
    /// </summary>
    public class FileGenerator
    {
        public event EventHandler OnComplete;

        public IDictionary<string, string> CustomValues { get; set; }

        public void Generate(Table table, string inputDir, string outputDir)
        {
            var client = new Client();

            if (CustomValues != null)
            {
                client.CustomValues = CustomValues;
            }

            var directoryInfo = new DirectoryInfo(inputDir);
            foreach (var fileInfo in directoryInfo.GetFiles())
            {
                string fileContent;
                using (var streamReader = File.OpenText(fileInfo.FullName))
                {
                    fileContent = streamReader.ReadToEnd();
                    streamReader.Close();
                }

                string generatedCode = client.Parse(table, fileContent);
                string fileName = client.Parse(table, fileInfo.Name);

                try
                {
                    using var streamWriter = new StreamWriter($"{outputDir}{Path.DirectorySeparatorChar}{fileName}");
                    streamWriter.Write(generatedCode);
                }
                catch (Exception x)
                {
                    Debug.WriteLine(x);
                }
            }

            OnComplete?.Invoke(this, new EventArgs());
        }
    }
}