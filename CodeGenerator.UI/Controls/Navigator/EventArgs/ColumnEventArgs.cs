namespace CodeGenerator.UI;

public class ColumnEventArgs : EventArgs
{
    public Column Column { get; }

    public ColumnEventArgs(Column column)
    {
        Column = column;
    }
}