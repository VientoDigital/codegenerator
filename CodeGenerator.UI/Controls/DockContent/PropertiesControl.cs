namespace CodeGenerator.UI;

public partial class PropertiesControl : UserControl
{
    public PropertiesControl()
    {
        InitializeComponent();
    }

    public object SelectedObject
    {
        get => (Table)propertyGrid.SelectedObject;
        set => propertyGrid.SelectedObject = value;
    }
}