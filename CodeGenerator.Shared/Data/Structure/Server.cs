using System.Collections.Generic;

namespace CodeGenerator.Data.Structure
{
    public class Server
    {
        private ICollection<Database> databases;
        private bool reload;

        public Server()
        {
            switch (ProviderType)
            {
                case DataSource.SqlServer: DataSourceProvider = new SqlDataSourceProvider(); break;
                case DataSource.MySql: DataSourceProvider = new MySqlDataSourceProvider(); break;
                case DataSource.PostgresSql: DataSourceProvider = new NpgsqlDataSourceProvider(); break;
                case DataSource.Oracle: DataSourceProvider = new OracleDataSourceProvider(); break;
            }
        }

        public static string ConnectionString { get; set; }

        public static IDataSourceProvider DataSourceProvider { get; private set; }

        public static DataSource ProviderType { get; set; }

        public static Database SelectedDatabase => DataSourceProvider.SelectedDatabase;

        public ICollection<Database> Databases
        {
            get
            {
                if (reload || databases == null)
                {
                    databases = DataSourceProvider.GetDatabases();
                }
                return databases;
            }
        }

        public void Reload() => reload = true;

        public override string ToString() => $"{ProviderType}:{ConnectionString}";
    }
}