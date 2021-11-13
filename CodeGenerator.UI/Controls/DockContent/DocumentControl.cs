using System.Windows.Forms;

namespace CodeGenerator.UI
{
    public partial class DocumentControl : UserControl
    {
        public DocumentControl()
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