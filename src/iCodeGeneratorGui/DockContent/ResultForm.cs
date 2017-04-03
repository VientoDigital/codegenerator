using WeifenLuo.WinFormsUI.Docking;

namespace iCodeGenerator.iCodeGeneratorGui
{
    public partial class ResultForm : DockContent
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