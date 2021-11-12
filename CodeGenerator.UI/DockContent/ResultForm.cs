using System.Windows.Forms;

namespace CodeGenerator.UI
{
    public partial class ResultForm : UserControl
    {
        public ResultForm()
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