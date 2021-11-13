namespace CodeGenerator.Data
{
    /// <summary>
    /// Summary description for DataProviderInfo.
    /// </summary>
    public class ProviderInfo
    {
        public ProviderInfo(ProviderType providerType)
        {
            ProviderType = providerType;
        }

        public string ConnectionStringFormat
        {
            get
            {
                switch (ProviderType)
                {
                    case ProviderType.SqlServer: return "SERVER=<SERVER>;UID=<USERNAME>;PWD=<PASSWORD>;";
                    case ProviderType.MySql: return "SERVER=<SERVER>;UID=<USERNAME>;PWD=<PASSWORD>;";
                    case ProviderType.PostgresSql: return "Server=<SERVER>;Port=<PORT>;User Id=<USERNAME>;Password=<PASSWORD>;";
                    case ProviderType.Oracle: return "Data Source=(DESCRIPTION=(ADDRESS_LIST=(ADDRESS=(PROTOCOL=TCP)(HOST=<IP)(PORT=1521)))(CONNECT_DATA=(SERVICE_NAME = <SERVICENAME>)));User ID=<USERNAME>;Password=<PASSWORD>";
                    //case DataProviderType.Oracle: return "Provider=OraOLEDB.Oracle;Password=<PASSWORD>;Persist Security Info=True;User ID=<USERNAME>;Data Source=<DATASOURCE>";
                    default: return null;
                }
            }
        }

        public ProviderType ProviderType { get; private set; }
    }
}