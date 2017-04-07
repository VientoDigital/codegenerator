using System.Windows.Forms;
using iCodeGenerator.DatabaseStructure;

namespace iCodeGenerator.iCodeGeneratorGui
{
    public partial class PropertiesForm : UserControl
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