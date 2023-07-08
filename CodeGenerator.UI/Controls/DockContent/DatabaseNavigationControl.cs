namespace CodeGenerator.UI;

public partial class DatabaseNavigationControl : UserControl
{
    public DatabaseNavigationControl()
    {
        InitializeComponent();
    }

    public event NavigatorControl.ColumnEventHandler ColumnSelected;

    public event NavigatorControl.DatabaseEventHandler DatabaseSelected;

    public event NavigatorControl.TableEventHandler TableSelected;

    public void Connect()
    {
        navigatorControl.Connect();
    }

    public void Disconnect()
    {
        navigatorControl.Disconnect();
    }

    public void ShowEditConnectionString()
    {
        navigatorControl.ShowConnectionForm();
    }

    [System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE1006:Naming Styles", Justification = "Acceptable for WinForms event handlers")]
    private void navigatorControl_ColumnSelect(object sender, ColumnEventArgs args)
    {
        ColumnSelected?.Invoke(this, args);
    }

    [System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE1006:Naming Styles", Justification = "Acceptable for WinForms event handlers")]
    private void navigatorControl_DatabaseSelect(object sender, DatabaseEventArgs args)
    {
        DatabaseSelected?.Invoke(this, args);
    }

    [System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE1006:Naming Styles", Justification = "Acceptable for WinForms event handlers")]
    private void navigatorControl_TableSelect(object sender, TableEventArgs args)
    {
        TableSelected?.Invoke(this, args);
    }
}