using System.Windows.Forms;

namespace iCodeGenerator.iCodeGeneratorGui
{
    public partial class DocumentForm : UserControl
    {
        public DocumentForm()
        {
            InitializeComponent();
        }

        public int SelectionStart
        {
            get { return rtbDocument.SelectionStart; }
        }

        public string ContentText
        {
            get { return rtbDocument.Text; }
            set { rtbDocument.Text = value; }
        }
    }
}