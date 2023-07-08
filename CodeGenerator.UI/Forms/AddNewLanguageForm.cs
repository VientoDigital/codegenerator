namespace CodeGenerator.UI;

public partial class AddNewLanguageForm : KryptonForm
{
    public AddNewLanguageForm()
    {
        InitializeComponent();
    }

    public string LanguageName => txtName.Text;

    [System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE1006:Naming Styles", Justification = "Acceptable for WinForms event handlers")]
    private void btnCancel_Click(object sender, EventArgs e)
    {
        Close();
        DialogResult = DialogResult.Cancel;
    }

    [System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE1006:Naming Styles", Justification = "Acceptable for WinForms event handlers")]
    private void btnOK_Click(object sender, EventArgs e)
    {
        Close();
        DialogResult = DialogResult.OK;
    }
}