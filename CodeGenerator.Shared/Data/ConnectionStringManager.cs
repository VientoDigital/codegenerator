using System;
using System.Collections;
using System.IO;

namespace CodeGenerator.Data
{
    public class ConnectionStringManager
    {
        private static readonly ConnectionStringManager manager = new ConnectionStringManager();

        public static ConnectionStringManager Instance => manager;

        public string FileName { get; set; } = $"{AppDomain.CurrentDomain.BaseDirectory}ConnectionStrings.txt";

        public void Add(string connectionString)
        {
            string[] connectionStrings = GetConnectionStrings();
            bool isNew = (connectionStrings.Length == 0 || connectionStrings[0].Trim().Length == 0);

            using (var streamWriter = isNew ? File.CreateText(FileName) : File.AppendText(FileName))
            {
                streamWriter.WriteLine(connectionString);
            }
        }

        //public void Clear()
        //{
        //    using (var streamWriter = File.CreateText(FileName))
        //    {
        //        streamWriter.WriteLine();
        //    }
        //}

        //public string Creator(DataProviderType providerType, string server, string username, string password, string database)
        //{
        //    switch (providerType)
        //    {
        //        case DataProviderType.SqlClient: return $"SERVER={server};DATABASE={database};UID={username};PWD={password};";
        //        case DataProviderType.MySql: return $"Data Source={database};Password={password};User ID={username};Location={server};";
        //        default: return $"Data Source={server};Initial Catalog={database};User Id={username};Password={password};";
        //    }
        //}

        public string[] GetConnectionStrings()
        {
            var connections = new ArrayList();

            if (File.Exists(FileName))
            {
                using (var streamReader = File.OpenText(FileName))
                {
                    while (streamReader.Peek() != -1)
                    {
                        connections.Add(streamReader.ReadLine());
                    }
                }
            }
            else
            {
                File.CreateText(FileName);
            }
            return (string[])connections.ToArray(typeof(string));
        }
    }
}