using System.Windows.Forms;

namespace CodeGenerator.UI
{
    public partial class DocumentForm : UserControl
    {
        public DocumentForm()
        {
            InitializeComponent();
        }

        public int SelectionStart => rtbDocument.SelectionStart;

        public string ContentText
        {
            get => rtbDocument.Text;
            set => rtbDocument.Text = value;
        }
    }
}