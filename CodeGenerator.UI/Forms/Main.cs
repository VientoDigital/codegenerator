using System.Diagnostics;
using System.Drawing;
using System.IO;
using CodeGenerator.Generator;
using CodeGenerator.UI.Properties;

namespace CodeGenerator.UI;

public partial class Main : KryptonForm
{
    private static string inputTemplateFolder = string.Empty;
    private static string outputTemplateFolder = string.Empty;
    private static Table selectedTable;
    private static string templateFile;

    #region Forms

    private CustomValuesControl customValuesForm;
    private DatabaseNavigationControl databaseNavigationForm;
    private DirectorySelectionWindow directorySelectionWindow;
    private DocumentControl templateForm;
    private KryptonDockingWorkspace workspace;
    private KryptonPage resultPage;
    private KryptonPage templatePage;
    private PropertiesControl propertiesForm;
    private ResultControl resultForm;
    private SnippetsControl snippetsForm;

    #endregion Forms

    public Main()
    {
        InitializeComponent();
        InitializeControls();
        //CheckForUpdates(); // Commented out, as no longer working
    }

    private static bool EnsureTableSelected()
    {
        if (selectedTable == null)
        {
            MessageBox.Show("Please selected a table first.", "Nothing Selected", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            return false;
        }

        return true;
    }

    private static Bitmap IconToBitMap(Icon icon)
    {
        using var bmp = icon.ToBitmap();
        return new Bitmap(bmp, new Size(16, 16));
    }

    private static bool IsValidFolder(string folderPath) => !string.IsNullOrEmpty(folderPath) && Directory.Exists(folderPath);

    private static KryptonPage NewPage(string name, Control content, Bitmap icon = null)
    {
        // Create new page with title and image
        var page = new KryptonPage
        {
            Text = name,
            TextTitle = name,
            TextDescription = name
        };

        if (icon != null)
        {
            page.ImageSmall = icon;
        }

        page.ClearFlags(KryptonPageFlags.DockingAllowClose);

        // Add the control for display inside the page
        content.Dock = DockStyle.Fill;
        page.Controls.Add(content);

        return page;
    }

    private static string SaveFile(string fileName, string contentText)
    {
        using (var streamWriter = new StreamWriter(fileName))
        {
            streamWriter.Write(contentText);
        }
        return fileName;
    }

    private static void ShowAboutVientoDigital()
    {
        var processStartInfo = new ProcessStartInfo("http://www.vientodigital.com/") { UseShellExecute = true };
        _ = Process.Start(processStartInfo);
    }

    private static void ShowAboutWindow()
    {
        using var form = new AboutWindow();
        form.ShowDialog();
    }

    private static void ShowDocoumentation()
    {
        using var form = new DocumentationWindow();
        form.ShowDialog();
    }

    private static void ShowSettings()
    {
        using var settingsForm = new SettingsForm();
        settingsForm.ShowDialog();
    }

    private void EnsureResultsFormExists()
    {
        if (resultPage.IsDisposed)
        {
            if (resultForm.IsDisposed)
            {
                resultForm = new ResultControl { Text = "Results" };
            }
            resultPage = NewPage("Results", resultForm, icon: Resources.Code_32x32);
            _ = kryptonDockingManager.AddToWorkspace("Workspace", new KryptonPage[] { resultPage });
            kryptonDockingManager.HidePage(resultPage);
            kryptonDockingManager.ShowPage(resultPage);
        }
    }

    private void GenerateCode()
    {
        try
        {
            if (!EnsureTableSelected())
            {
                return;
            }

            EnsureResultsFormExists();
            var client = new Client { CustomValues = CustomValuesControl.CustomValues };
            resultForm.ContentText = client.Parse(selectedTable, templateForm.ContentText);
            workspace.SelectPage(resultPage.UniqueName);
        }
        catch (Exception x)
        {
            MessageBox.Show(this, x.Message, x.Message, MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }
    }

    private void GenerateFiles()
    {
        if (!EnsureTableSelected())
        {
            return;
        }

        if (IsValidFolder(inputTemplateFolder) && IsValidFolder(outputTemplateFolder))
        {
            try
            {
                var fileGenerator = new FileGenerator();
                fileGenerator.OnComplete += fileGenerator_OnComplete;
                fileGenerator.CustomValues = CustomValuesControl.CustomValues;
                fileGenerator.Generate(selectedTable, inputTemplateFolder, outputTemplateFolder);
            }
            catch (Exception x)
            {
                MessageBox.Show(x.Message);
            }
        }
        else
        {
            SelectTemplatesDirectory();
        }
    }

    private void InitializeControls()
    {
        // Setup docking functionality
        workspace = kryptonDockingManager.ManageWorkspace(kryptonDockableWorkspace);
        _ = kryptonDockingManager.ManageControl(kryptonPanel, workspace);
        _ = kryptonDockingManager.ManageFloating(this);

        // Add initial docking pages
        databaseNavigationForm = new DatabaseNavigationControl { Text = "Database Navigation" };
        databaseNavigationForm.TableSelected += databaseNavigationForm_TableSelected;
        databaseNavigationForm.DatabaseSelected += databaseNavigationForm_DatabaseSelected;
        databaseNavigationForm.ColumnSelected += databaseNavigationForm_ColumnSelected;

        snippetsForm = new SnippetsControl { Text = "Snippets" };
        snippetsForm.SnippetSelected += snippetsForm_SnippetSelected;

        templateForm = new DocumentControl { Text = "Template" };
        resultForm = new ResultControl { Text = "Results" };
        propertiesForm = new PropertiesControl { Text = "Properties" };
        customValuesForm = new CustomValuesControl { Text = "Custom Values" };

        templatePage = NewPage("Template", templateForm, icon: Resources.Design_32x32);
        resultPage = NewPage("Results", resultForm, icon: Resources.Code_32x32);

        _ = kryptonDockingManager.AddToWorkspace("Workspace", new KryptonPage[] { templatePage, resultPage });
        _ = kryptonDockingManager.AddAutoHiddenGroup("Control", DockingEdge.Left, new KryptonPage[]
        {
            NewPage("Snippets", snippetsForm, icon: Resources.Snip_32x32)
        });

        var leftSideDockspace = _ = kryptonDockingManager.AddDockspace("Control", DockingEdge.Left, new KryptonPage[]
        {
            NewPage("Database Navigation", databaseNavigationForm, icon: Resources.Database_32x32)
        });
        leftSideDockspace.DockspaceControl.Width = 250;

        var rightSideDockspace = kryptonDockingManager.AddDockspace("Control", DockingEdge.Right, new KryptonPage[]
        {
            NewPage("Properties", propertiesForm, icon: Resources.Details_32x32),
            NewPage("Custom Values", customValuesForm, icon: Resources.Braces_32x32)
        });
        rightSideDockspace.DockspaceControl.Width = 250;
    }

    private void NewTemplate()
    {
        templateFile = null;
        templateForm.ContentText = string.Empty;
        workspace.SelectPage(templatePage.UniqueName);
    }

    private void OpenTemplate()
    {
        if (dlgOpenFile.ShowDialog(this) == DialogResult.OK)
        {
            try
            {
                templateFile = dlgOpenFile.FileName;
                using (var stream = dlgOpenFile.OpenFile())
                using (var streamReader = new StreamReader(stream))
                {
                    templateForm.ContentText = streamReader.ReadToEnd();
                }
                workspace.SelectPage(templatePage.UniqueName);
            }
            catch (Exception x)
            {
                MessageBox.Show(@"Error: Could not read file from disk. Original error: " + x.Message);
            }
        }
    }

    private string SaveAsFile(string fileName, string contentText)
    {
        if (dlgSaveFile.ShowDialog() == DialogResult.OK)
        {
            fileName = dlgSaveFile.FileName;
            SaveFile(fileName, contentText);
        }
        return fileName;
    }

    private void SaveTemplate()
    {
        if (templateFile != null)
        {
            SaveFile(templateFile, templateForm.ContentText);
        }
        else
        {
            templateFile = SaveAsFile(templateFile, templateForm.ContentText);
        }
    }

    private void SaveTemplateAs() => templateFile = SaveAsFile(null, templateForm.ContentText);

    private void SelectTemplatesDirectory()
    {
        if (directorySelectionWindow == null)
        {
            directorySelectionWindow = new DirectorySelectionWindow();
            directorySelectionWindow.InputFolderSelected += directorySelectionWindow_InputFolderSelected;
            directorySelectionWindow.OutputFolderSelected += directorySelectionWindow_OutputFolderSelected;
        }
        directorySelectionWindow.ShowDialog(this);
    }

    #region Event Handlers

    [System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE1006:Naming Styles", Justification = "Acceptable for WinForms event handlers")]
    private static void directorySelectionWindow_InputFolderSelected(object sender, FolderEventArgs args) => inputTemplateFolder = args.FolderName;

    [System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE1006:Naming Styles", Justification = "Acceptable for WinForms event handlers")]
    private static void directorySelectionWindow_OutputFolderSelected(object sender, FolderEventArgs args) => outputTemplateFolder = args.FolderName;

    [System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE1006:Naming Styles", Justification = "Acceptable for WinForms event handlers")]
    private void databaseNavigationForm_ColumnSelected(object sender, ColumnEventArgs args)
    {
        selectedTable = args.Column.ParentTable;
        propertiesForm.SelectedObject = args.Column;
    }

    [System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE1006:Naming Styles", Justification = "Acceptable for WinForms event handlers")]
    private void databaseNavigationForm_DatabaseSelected(object sender, DatabaseEventArgs args) => propertiesForm.SelectedObject = args.Database;

    [System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE1006:Naming Styles", Justification = "Acceptable for WinForms event handlers")]
    private void databaseNavigationForm_TableSelected(object sender, TableEventArgs args)
    {
        selectedTable = args.Table;
        propertiesForm.SelectedObject = args.Table;
    }

    [System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE1006:Naming Styles", Justification = "Acceptable for WinForms event handlers")]
    private void fileGenerator_OnComplete(object sender, EventArgs e)
    {
        //MessageBox.Show("File Generation Completed");
        if (IsValidFolder(outputTemplateFolder))
        {
            _ = Process.Start(outputTemplateFolder);
        }
    }

    [System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE1006:Naming Styles", Justification = "Acceptable for WinForms event handlers")]
    private void mnuFileDatabaseConnect_Click(object sender, EventArgs e) => databaseNavigationForm.Connect();

    [System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE1006:Naming Styles", Justification = "Acceptable for WinForms event handlers")]
    private void mnuFileDatabaseDisconnect_Click(object sender, EventArgs e) => databaseNavigationForm.Disconnect();

    [System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE1006:Naming Styles", Justification = "Acceptable for WinForms event handlers")]
    private void mnuFileEditConfiguration_Click(object sender, EventArgs e) => databaseNavigationForm.ShowEditConnectionString();

    [System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE1006:Naming Styles", Justification = "Acceptable for WinForms event handlers")]
    private void mnuFileNewTemplate_Click(object sender, EventArgs e) => NewTemplate();

    [System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE1006:Naming Styles", Justification = "Acceptable for WinForms event handlers")]
    private void mnuFileOpenTemplate_Click(object sender, EventArgs e) => OpenTemplate();

    [System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE1006:Naming Styles", Justification = "Acceptable for WinForms event handlers")]
    private void mnuFileSaveAsResult_Click(object sender, EventArgs e) => SaveAsFile(null, resultForm.ContentText);

    [System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE1006:Naming Styles", Justification = "Acceptable for WinForms event handlers")]
    private void mnuFileSaveAsTemplate_Click(object sender, EventArgs e) => SaveTemplateAs();

    [System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE1006:Naming Styles", Justification = "Acceptable for WinForms event handlers")]
    private void mnuFileSaveTemplate_Click(object sender, EventArgs e) => SaveTemplate();

    [System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE1006:Naming Styles", Justification = "Acceptable for WinForms event handlers")]
    private void mnuFileSettings_Click(object sender, EventArgs e) => ShowSettings();

    [System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE1006:Naming Styles", Justification = "Acceptable for WinForms event handlers")]
    private void mnuFileTemplateExit_Click(object sender, EventArgs e) => Close();

    [System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE1006:Naming Styles", Justification = "Acceptable for WinForms event handlers")]
    private void mnuGenerateFromTemplateFiles_Click(object sender, EventArgs e) => GenerateFiles();

    [System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE1006:Naming Styles", Justification = "Acceptable for WinForms event handlers")]
    private void mnuGenerateFromTemplateTab_Click(object sender, EventArgs e) => GenerateCode();

    [System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE1006:Naming Styles", Justification = "Acceptable for WinForms event handlers")]
    private void mnuHelpAbout_Click(object sender, EventArgs e) => ShowAboutWindow();

    [System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE1006:Naming Styles", Justification = "Acceptable for WinForms event handlers")]
    private void mnuHelpAboutVientoDigital_Click(object sender, EventArgs e) => ShowAboutVientoDigital();

    [System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE1006:Naming Styles", Justification = "Acceptable for WinForms event handlers")]
    private void mnuHelpDocumentation_Click(object sender, EventArgs e) => ShowDocoumentation();

    [System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE1006:Naming Styles", Justification = "Acceptable for WinForms event handlers")]
    private void mnuViewResults_Click(object sender, EventArgs e)
    {
        EnsureResultsFormExists();

        if (!kryptonDockingManager.IsPageShowing(resultPage))
        {
            kryptonDockingManager.ShowPage(resultPage);
        }
        else
        {
            kryptonDockingManager.HidePage(resultPage);
        }
    }

    [System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE1006:Naming Styles", Justification = "Acceptable for WinForms event handlers")]
    private void mnuViewTemplate_Click(object sender, EventArgs e)
    {
        if (templatePage.IsDisposed)
        {
            if (templateForm.IsDisposed)
            {
                templateForm = new DocumentControl { Text = "Template" };
            }
            templatePage = NewPage("Template", templateForm, icon: IconToBitMap(Resources.itemplate));
            _ = kryptonDockingManager.AddToWorkspace("Workspace", new KryptonPage[] { templatePage });
            kryptonDockingManager.HidePage(templatePage);
            kryptonDockingManager.ShowPage(templatePage);
        }
        else
        {
            if (!kryptonDockingManager.IsPageShowing(templatePage))
            {
                kryptonDockingManager.ShowPage(templatePage);
            }
            else
            {
                kryptonDockingManager.HidePage(templatePage);
            }
        }
    }

    [System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE1006:Naming Styles", Justification = "Acceptable for WinForms event handlers")]
    private void snippetsForm_SnippetSelected(object sender, SnippetEventArgs args)
    {
        templateForm.ContentText = templateForm.ContentText.Insert(templateForm.SelectionStart, SnippetsHelper.Snippets[args.Snippet].ToString());
    }

    [System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE1006:Naming Styles", Justification = "Acceptable for WinForms event handlers")]
    private void tsBtnAbout_Click(object sender, EventArgs e) => ShowAboutWindow();

    [System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE1006:Naming Styles", Justification = "Acceptable for WinForms event handlers")]
    private void tsBtnAboutViento_Click(object sender, EventArgs e) => ShowAboutVientoDigital();

    [System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE1006:Naming Styles", Justification = "Acceptable for WinForms event handlers")]
    private void tsBtnDatabaseConfig_Click(object sender, EventArgs e) => databaseNavigationForm.ShowEditConnectionString();

    [System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE1006:Naming Styles", Justification = "Acceptable for WinForms event handlers")]
    private void tsBtnDatabaseConnect_Click(object sender, EventArgs e) => databaseNavigationForm.Connect();

    [System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE1006:Naming Styles", Justification = "Acceptable for WinForms event handlers")]
    private void tsBtnDatabaseDisconnect_Click(object sender, EventArgs e) => databaseNavigationForm.Disconnect();

    [System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE1006:Naming Styles", Justification = "Acceptable for WinForms event handlers")]
    private void tsBtnGenerate_Click(object sender, EventArgs e) => GenerateCode();

    [System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE1006:Naming Styles", Justification = "Acceptable for WinForms event handlers")]
    private void tsBtnGenerateFiles_Click(object sender, EventArgs e) => GenerateFiles();

    [System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE1006:Naming Styles", Justification = "Acceptable for WinForms event handlers")]
    private void tsBtnHelp_Click(object sender, EventArgs e) => ShowDocoumentation();

    [System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE1006:Naming Styles", Justification = "Acceptable for WinForms event handlers")]
    private void tsBtnNewTemplate_Click(object sender, EventArgs e) => NewTemplate();

    [System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE1006:Naming Styles", Justification = "Acceptable for WinForms event handlers")]
    private void tsBtnOpenTemplate_Click(object sender, EventArgs e) => OpenTemplate();

    [System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE1006:Naming Styles", Justification = "Acceptable for WinForms event handlers")]
    private void tsBtnSaveResultAs_Click(object sender, EventArgs e) => SaveAsFile(null, resultForm.ContentText);

    [System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE1006:Naming Styles", Justification = "Acceptable for WinForms event handlers")]
    private void tsBtnSaveTemplate_Click(object sender, EventArgs e) => SaveTemplate();

    [System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE1006:Naming Styles", Justification = "Acceptable for WinForms event handlers")]
    private void tsBtnSaveTemplateAs_Click(object sender, EventArgs e) => SaveTemplateAs();

    [System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE1006:Naming Styles", Justification = "Acceptable for WinForms event handlers")]
    private void tsBtnSettings_Click(object sender, EventArgs e) => ShowSettings();

    #endregion Event Handlers
}