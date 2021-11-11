namespace CodeGenerator.GenericDataAccess
{
    /// <summary>
    /// Summary description for DataProviderInfo.
    /// </summary>
    public class DataAccessProviderInfo
    {
        private string[] _ConnectionStringFormat = new string[] {
            @"SERVER=<SERVER>;UID=<USERNAME>;PWD=<PASSWORD>;",
            @"SERVER=<SERVER>;UID=<USERNAME>;PWD=<PASSWORD>;",
            @"",
            @"Server=<SERVER>;Port=<PORT>;User Id=<USERNAME>;Password=<PASSWORD>;",
            @"Provider=OraOLEDB.Oracle;Password=<PASSWORD>;Persist Security Info=True;User ID=<USERNAME>;Data Source=<DATASOURCE>"
        };

        private string[] _Description = new string[] {
            @"SQL Server Connction Type",
            @"MySQL Server Connction Type",
            @"Access Database Connction Type",
            @"Postgres Server Connection Type",
            @"Oracle Server Connection Type"
                                                     };

        private string[] _Names = new string[] {
            @"SQL Server",
            @"MySQL Server",
            @"Access Database",
            @"Postgres Server",
            @"Oracle Server"
                                               };

        public DataAccessProviderInfo(DataProviderType providerType)
        {
            ProviderType = providerType;
        }

        public string ConnectionStringFormat => _ConnectionStringFormat[(int)ProviderType];

        public string Description => _Description[(int)ProviderType];

        public string Name => _Names[(int)ProviderType];

        public DataProviderType ProviderType { get; private set; }
    }
}