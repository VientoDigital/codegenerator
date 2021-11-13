using System;
using System.Collections;
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

        public IDictionary CustomValues { get; set; }

        public void Generate(Table table, string inputDir, string outputDir)
        {
            var directoryInfo = new DirectoryInfo(inputDir);
            var client = new Client();
            var originalSd = client.StartDelimiter;
            var originalEd = client.EndingDelimiter;

            if (CustomValues != null)
            {
                client.CustomValues = CustomValues;
            }

            foreach (var fileInfo in directoryInfo.GetFiles())
            {
                client.StartDelimiter = "{";//originalSd;
                client.EndingDelimiter = "}";//originalEd;

                string fileContent;
                using (var streamReader = File.OpenText(fileInfo.FullName))
                {
                    fileContent = streamReader.ReadToEnd();
                    streamReader.Close();
                }

                string codeGenerated = client.Parse(table, fileContent);
                client.StartDelimiter = string.Empty;
                client.EndingDelimiter = string.Empty;
                string fileName = client.Parse(table, fileInfo.Name);

                try
                {
                    using (var streamWriter = new StreamWriter(outputDir + Path.DirectorySeparatorChar + fileName))
                    {
                        streamWriter.Write(codeGenerated);
                    }
                }
                catch (Exception x)
                {
                    Debug.WriteLine(x);
                }
            }

            client.StartDelimiter = originalSd;
            client.EndingDelimiter = originalEd;
            CompleteNotifier(new EventArgs());
        }

        protected void CompleteNotifier(EventArgs e)
        {
            OnComplete?.Invoke(this, e);
        }
    }
}