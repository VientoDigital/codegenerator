namespace CodeGenerator.Data;

public class ProviderFactory
{
    private readonly DataSource providerType;

    public ProviderFactory(DataSource providerType)
    {
        this.providerType = providerType;
    }

    public string DbProviderInvariantName => providerType switch
    {
        DataSource.SqlServer => "Microsoft.Data.SqlClient",
        DataSource.MySql => "MySql.Data.MySqlClient",
        DataSource.PostgresSql => "Npgsql",
        DataSource.Oracle => "Oracle.ManagedDataAccess.Client",
        _ => throw new ArgumentOutOfRangeException(nameof(providerType)),
    };

    public DbProviderFactory DbProviderFactory => DbProviderFactories.GetFactory(DbProviderInvariantName);

    public static IDbCommand CreateCommand(string cmdText, IDbConnection connection)
    {
        var command = connection.CreateCommand();
        command.CommandText = cmdText;
        return command;
    }

    public DbConnection CreateConnection(string connectionString)
    {
        var connection = DbProviderFactory.CreateConnection();
        connection.ConnectionString = connectionString;
        return connection;
    }

    public IDbDataAdapter CreateDataAdapter()
    {
        return DbProviderFactory.CreateDataAdapter();
    }

    public IDbDataParameter CreateParameter()
    {
        return DbProviderFactory.CreateParameter();
    }
}