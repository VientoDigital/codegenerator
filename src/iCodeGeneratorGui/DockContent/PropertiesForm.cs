using iCodeGenerator.DatabaseStructure;
using WeifenLuo.WinFormsUI.Docking;

namespace iCodeGenerator.iCodeGeneratorGui
{
    public partial class PropertiesForm : DockContent
    {
        public PropertiesForm()
        {
            InitializeComponent();
        }

        public object SelectedObject
        {
            get { return (Table)propertyGrid.SelectedObject; }
            set { propertyGrid.SelectedObject = value; }
        }
    }
}