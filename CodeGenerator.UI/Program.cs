using System.Data.Common;
using Microsoft.Data.SqlClient;
using MySql.Data.MySqlClient;
using Npgsql;
using Oracle.ManagedDataAccess.Client;

namespace CodeGenerator.UI;

internal class Program
{
    [STAThread]
    public static void Main()
    {
        DbProviderFactories.RegisterFactory("Microsoft.Data.SqlClient", SqlClientFactory.Instance);
        DbProviderFactories.RegisterFactory("MySql.Data.MySqlClient", MySqlClientFactory.Instance);
        DbProviderFactories.RegisterFactory("Npgsql", NpgsqlFactory.Instance);
        DbProviderFactories.RegisterFactory("Oracle.ManagedDataAccess.Client", OracleClientFactory.Instance);

        Application.ApplicationExit += Application_ApplicationExit;
        using var form = new Main();
        Application.Run(form);
    }

    private static void Application_ApplicationExit(object sender, EventArgs e)
    {
        ConfigFile.Instance.Save();
    }
}