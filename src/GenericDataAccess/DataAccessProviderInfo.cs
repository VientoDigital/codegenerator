namespace iCodeGenerator.GenericDataAccess
{
    /// <summary>
    /// Summary description for DataProviderInfo.
    /// </summary>
    public class DataAccessProviderInfo
    {
        private string[] _Names = new string[] {
            @"SQL Server",
            @"MySQL Server",
            @"Access Database",
            @"Postgres Server",
            @"Oracle Server"
                                               };

        private string[] _Description = new string[] {
            @"SQL Server Connction Type",
            @"MySQL Server Connction Type",
            @"Access Database Connction Type",
            @"Postgres Server Connection Type",
            @"Oracle Server Connection Type"
                                                     };

        private string[] _ConnectionStringFormat = new string[] {
            @"SERVER=<SERVER>;UID=<USERNAME>;PWD=<PASSWORD>;",
            @"SERVER=<SERVER>;UID=<USERNAME>;PWD=<PASSWORD>;",
            @"",
            @"Server=<SERVER>;Port=<PORT>;User Id=<USERNAME>;Password=<PASSWORD>;",
            @"Provider=OraOLEDB.Oracle;Password=<PASSWORD>;Persist Security Info=True;User ID=<USERNAME>;Data Source=<DATASOURCE>"
        };

        private DataProviderType _ProviderType;

        public DataProviderType ProviderType
        {
            get { return _ProviderType; }
        }

        public string Name
        {
            get { return _Names[(int)_ProviderType]; }
        }

        public string Description
        {
            get { return _Description[(int)_ProviderType]; }
        }

        public string ConnectionStringFormat
        {
            get { return _ConnectionStringFormat[(int)_ProviderType]; }
        }

        public DataAccessProviderInfo(DataProviderType providerType)
        {
            _ProviderType = providerType;
        }
    }
}