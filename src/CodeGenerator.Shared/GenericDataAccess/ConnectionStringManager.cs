using System;
using System.Collections;
using System.IO;

namespace CodeGenerator.GenericDataAccess
{
    public class ConnectionStringManager
    {
        private static readonly ConnectionStringManager manager = new ConnectionStringManager();

        private ConnectionStringManager()
        {
        }

        public static ConnectionStringManager Instance => manager;

        public string FileName { get; set; } = AppDomain.CurrentDomain.BaseDirectory + "ConnectionStrings.txt";

        public void Add(string connectionString)
        {
            string[] strings = GetConnectionStrings();

            StreamWriter streamWriter;
            if (strings.Length == 0 || strings[0].Trim().Length == 0)
            {
                streamWriter = File.CreateText(FileName);
            }
            else
            {
                streamWriter = File.AppendText(FileName);
            }

            streamWriter.WriteLine(connectionString);
            streamWriter.Flush();
            streamWriter.Close();
        }

        public void Clear()
        {
            var streamWriter = File.CreateText(FileName);
            streamWriter.WriteLine();
            streamWriter.Flush();
            streamWriter.Close();
        }

        public string Creator(DataProviderType providerType, string server, string username, string password, string database)
        {
            switch (providerType)
            {
                case DataProviderType.SqlClient: return "SERVER=" + server + ";DATABASE=" + database + ";UID=" + username + ";PWD=" + password + ";";
                case DataProviderType.MySql: return @"Data Source=" + database + ";Password=" + password + ";User ID=" + username + ";Location=" + server + ";";
                default: return "Data Source=" + server + ";Initial Catalog=" + database + ";User Id=" + username + ";Password=" + password + ";";
            }
        }

        public string[] GetConnectionStrings()
        {
            var connections = new ArrayList();

            if (File.Exists(FileName))
            {
                var streamReader = File.OpenText(FileName);
                while (streamReader.Peek() != -1)
                {
                    connections.Add(streamReader.ReadLine());
                }
                streamReader.Close();
            }
            else
            {
                File.CreateText(FileName);
            }
            return (string[])connections.ToArray(typeof(string));
        }
    }
}