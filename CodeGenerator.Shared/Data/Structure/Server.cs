namespace CodeGenerator.Data.Structure;

public class Server : IDisposable
{
    private ICollection<Database> databases;
    private bool reload;
    private bool disposedValue;

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

    protected virtual void Dispose(bool disposing)
    {
        if (!disposedValue)
        {
            if (disposing)
            {
                // TODO: dispose managed state (managed objects)
                DataSourceProvider?.Dispose();
            }

            // TODO: free unmanaged resources (unmanaged objects) and override finalizer
            // TODO: set large fields to null
            disposedValue = true;
        }
    }

    // // TODO: override finalizer only if 'Dispose(bool disposing)' has code to free unmanaged resources
    // ~Server()
    // {
    //     // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
    //     Dispose(disposing: false);
    // }

    public void Dispose()
    {
        // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
        Dispose(disposing: true);
        GC.SuppressFinalize(this);
    }
}