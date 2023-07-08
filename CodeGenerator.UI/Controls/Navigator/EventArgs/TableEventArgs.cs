namespace CodeGenerator.UI;

public class TableEventArgs : EventArgs
{
    public Table Table { get; }

    public TableEventArgs(Table table)
    {
        Table = table;
    }
}