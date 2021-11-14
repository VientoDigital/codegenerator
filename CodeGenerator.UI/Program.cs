using System;
using System.Data.Common;
using System.Data.SqlClient;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using Npgsql;
using Oracle.ManagedDataAccess.Client;

namespace CodeGenerator.UI
{
    internal class Program
    {
        [STAThread]
        public static void Main()
        {
            DbProviderFactories.RegisterFactory("System.Data.SqlClient", SqlClientFactory.Instance);
            DbProviderFactories.RegisterFactory("MySql.Data.MySqlClient", MySqlClientFactory.Instance);
            DbProviderFactories.RegisterFactory("Npgsql", NpgsqlFactory.Instance);
            DbProviderFactories.RegisterFactory("Oracle.ManagedDataAccess.Client", OracleClientFactory.Instance);

            Application.ApplicationExit += Application_ApplicationExit;
            Application.Run(new Main());
        }

        private static void Application_ApplicationExit(object sender, EventArgs e)
        {
            ConfigFile.Instance.Save();
        }
    }
}
