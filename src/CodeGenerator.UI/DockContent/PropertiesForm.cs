using System.Windows.Forms;
using CodeGenerator.Data.Structure;

namespace CodeGenerator.UI
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