using System;
using System.Collections;
using System.Diagnostics;
using System.IO;
using iCodeGenerator.DatabaseStructure;

namespace iCodeGenerator.Generator
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
            //var originalSd = client.StartDelimiter;
            //var originalEd = client.EndingDelimiter;
            if (CustomValues != null)
            {
                client.CustomValues = CustomValues;
            }
            foreach (var fileInfo in directoryInfo.GetFiles())
            {
                client.StartDelimiter = "{";//originalSd;
                client.EndingDelimiter = "}";//originalEd;
                var streamReader = File.OpenText(fileInfo.FullName);
                var fileContent = streamReader.ReadToEnd();
                streamReader.Close();
                string codeGenerated = client.Parse(table, fileContent);
                client.StartDelimiter = string.Empty;
                client.EndingDelimiter = string.Empty;
                var fileName = client.Parse(table, fileInfo.Name);
                try
                {
                    var streamWriter = new StreamWriter(outputDir + Path.DirectorySeparatorChar + fileName);
                    streamWriter.Write(codeGenerated);
                    streamWriter.Flush();
                    streamWriter.Close();
                }
                catch (Exception e)
                {
                    Debug.WriteLine(e);
                }
            }
            CompleteNotifier(new EventArgs());
        }

        protected void CompleteNotifier(EventArgs e)
        {
            if (OnComplete != null)
            {
                OnComplete(this, e);
            }
        }
    }
}