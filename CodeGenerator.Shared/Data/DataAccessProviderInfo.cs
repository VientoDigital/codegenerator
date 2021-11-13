namespace CodeGenerator.Data
{
    /// <summary>
    /// Summary description for DataProviderInfo.
    /// </summary>
    public class DataAccessProviderInfo
    {
        public DataAccessProviderInfo(DataProviderType providerType)
        {
            ProviderType = providerType;
        }

        public string ConnectionStringFormat
        {
            get
            {
                switch (ProviderType)
                {
                    case DataProviderType.SqlClient: return "SERVER=<SERVER>;UID=<USERNAME>;PWD=<PASSWORD>;";
                    case DataProviderType.MySql: return "SERVER=<SERVER>;UID=<USERNAME>;PWD=<PASSWORD>;";
                    case DataProviderType.Access: return string.Empty;
                    case DataProviderType.PostgresSql: return "Server=<SERVER>;Port=<PORT>;User Id=<USERNAME>;Password=<PASSWORD>;";
                    case DataProviderType.Oracle: return "Data Source=(DESCRIPTION=(ADDRESS_LIST=(ADDRESS=(PROTOCOL=TCP)(HOST=<IP)(PORT=1521)))(CONNECT_DATA=(SERVICE_NAME = <SERVICENAME>)));User ID=<USERNAME>;Password=<PASSWORD>";
                    //case DataProviderType.Oracle: return "Provider=OraOLEDB.Oracle;Password=<PASSWORD>;Persist Security Info=True;User ID=<USERNAME>;Data Source=<DATASOURCE>";
                    default: return null;
                }
            }
        }

        public string Description
        {
            get
            {
                switch (ProviderType)
                {
                    case DataProviderType.SqlClient: return "SQL Server Connction Type";
                    case DataProviderType.MySql: return "MySQL Server Connction Type";
                    case DataProviderType.Access: return "Access Database Connction Type";
                    case DataProviderType.PostgresSql: return "Postgres Server Connction Type";
                    case DataProviderType.Oracle: return "Oracle Server Connction Type";
                    default: return null;
                }
            }
        }

        public string Name
        {
            get
            {
                switch (ProviderType)
                {
                    case DataProviderType.SqlClient: return "SQL Server";
                    case DataProviderType.MySql: return "MySQL Server";
                    case DataProviderType.Access: return "Access Database";
                    case DataProviderType.PostgresSql: return "Postgres Server";
                    case DataProviderType.Oracle: return "Oracle Server";
                    default: return null;
                }
            }
        }

        public DataProviderType ProviderType { get; private set; }
    }
}