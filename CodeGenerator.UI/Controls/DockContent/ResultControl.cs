namespace CodeGenerator.UI;

public partial class ResultControl : UserControl
{
    public ResultControl()
    {
        InitializeComponent();
        ConfigFile.SelectedLanguageChanged += ConfigFile_SelectedLanguageChanged;
    }

    private void ConfigFile_SelectedLanguageChanged(string lang)
    {
        switch (lang)
        {
            case ".NET":
            case "C#": Language = FastColoredTextBoxNS.Language.CSharp; break;
            case "VB": Language = FastColoredTextBoxNS.Language.VB; break;
            default:
                {
                    if (Enum.TryParse(lang, out FastColoredTextBoxNS.Language result))
                    {
                        Language = result;
                    }
                    else
                    {
                        Language = FastColoredTextBoxNS.Language.Custom;
                    }
                }
                break;
        }
    }

    public FastColoredTextBoxNS.Language Language
    {
        get => fctResult.Language;
        set => fctResult.Language = value;
    }

    public string ContentText
    {
        get => fctResult.Text;
        set => fctResult.Text = value;
    }
}