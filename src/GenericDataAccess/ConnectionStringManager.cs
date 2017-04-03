using System;
using System.Collections;
using System.IO;

namespace iCodeGenerator.GenericDataAccess
{
    public class ConnectionStringManager
    {
        private string _Filename = AppDomain.CurrentDomain.BaseDirectory + "ConnectionStrings.txt";

        private static ConnectionStringManager manager = new ConnectionStringManager();

        public static ConnectionStringManager Instance
        {
            get { return manager; }
        }

        public string Filename
        {
            get { return _Filename; }
            set { _Filename = value; }
        }

        private ConnectionStringManager()
        {
        }

        public string[] GetConnectionStrings()
        {
            ArrayList connections = new ArrayList();

            if (File.Exists(_Filename))
            {
                StreamReader sr = File.OpenText(_Filename);
                while (sr.Peek() != -1)
                    connections.Add(sr.ReadLine());
                sr.Close();
            }
            else
            {
                File.CreateText(_Filename);
            }
            return (string[])connections.ToArray(typeof(string));
        }

        public void Add(string connectionString)
        {
            StreamWriter sw = null;
            string[] strings = GetConnectionStrings();
            if (strings.Length == 0 || strings[0].Trim().Length == 0)
            {
                sw = File.CreateText(_Filename);
            }
            else
            {
                sw = File.AppendText(_Filename);
            }
            sw.WriteLine(connectionString);
            sw.Flush();
            sw.Close();
        }

        public void Clear()
        {
            StreamWriter sw = File.CreateText(_Filename);
            sw.WriteLine();
            sw.Flush();
            sw.Close();
        }

        public string Creator(DataProviderType providerType, string server, string username, string password, string database)
        {
            switch (providerType)
            {
                case DataProviderType.SqlClient:
                    return "SERVER=" + server + ";DATABASE=" + database + ";UID=" + username + ";PWD=" + password + ";";
                case DataProviderType.MySql:
                    return @"Data Source=" + database + ";Password=" + password + ";User ID=" + username + ";Location=" + server + ";";
                default:
                    return "Data Source=" + server + ";Initial Catalog=" + database + ";User Id=" + username + ";Password=" + password + ";";
            }
        }
    }
}