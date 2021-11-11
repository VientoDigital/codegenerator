using System.Windows.Forms;

namespace CodeGenerator.CodeGenerator.UI
{
    public partial class ResultForm : UserControl
    {
        public ResultForm()
        {
            InitializeComponent();
        }

        public string ContentText
        {
            get { return rtbResult.Text; }
            set { rtbResult.Text = value; }
        }
    }
}