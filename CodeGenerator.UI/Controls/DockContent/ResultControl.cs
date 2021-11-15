using System.Windows.Forms;

namespace CodeGenerator.UI
{
    public partial class ResultControl : UserControl
    {
        public ResultControl()
        {
            InitializeComponent();
        }

        public string ContentText
        {
            get => rtbResult.Text;
            set => rtbResult.Text = value;
        }
    }
}