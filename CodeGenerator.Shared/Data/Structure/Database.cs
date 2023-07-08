namespace CodeGenerator.Data.Structure;

// TODO: Add schemas collection in here..

public class Database
{
    private bool reload;
    private IEnumerable<Table> tables;
    private IEnumerable<Table> views;

    [Category("Database"), ReadOnly(true)]
    public string Name { get; set; }

    [Browsable(false), DefaultValue(false)]
    public IEnumerable<Table> Tables
    {
        get
        {
            if (reload || tables == null)
            {
                tables = Server.DataSourceProvider.GetTables(this);
                reload = false;
            }
            return tables;
        }
    }

    [Browsable(false), DefaultValue(false)]
    public IEnumerable<Table> Views
    {
        get
        {
            if (reload || views == null)
            {
                views = Server.DataSourceProvider.GetViews(this);
                reload = false;
            }
            return views;
        }
    }

    public void Reload() => reload = true;

    public override string ToString() => Name;
}