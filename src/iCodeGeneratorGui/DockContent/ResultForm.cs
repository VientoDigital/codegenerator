using System.Windows.Forms;

namespace iCodeGenerator.iCodeGeneratorGui
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