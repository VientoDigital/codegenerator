namespace CodeGenerator.UI;

/// <summary>
/// Summary description for DirectorySelectionWindow.
/// </summary>
public class DirectorySelectionWindow : KryptonForm
{
    private FolderBrowserDialog dlgFolderBrowserInput;
    private FolderBrowserDialog dlgFolderBrowserOutput;
    private KryptonTextBox txtTemplatesDirectory;
    private KryptonTextBox txtOutputDirectory;
    private KryptonButton btnBrowseTemplatesDirectory;
    private KryptonButton btnBrowseOutputDirectory;
    private KryptonLabel lblTemplatesDirectory;
    private KryptonLabel lblOutputDirectory;
    private KryptonButton btnOK;
    private KryptonButton btnCancel;

    public DirectorySelectionWindow()
    {
        //
        // Required for Windows Form Designer support
        //
        InitializeComponent();
    }

    #region Windows Form Designer generated code

    /// <summary>
    /// Required method for Designer support - do not modify
    /// the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
        dlgFolderBrowserInput = new FolderBrowserDialog();
        dlgFolderBrowserOutput = new FolderBrowserDialog();
        txtTemplatesDirectory = new KryptonTextBox();
        txtOutputDirectory = new KryptonTextBox();
        btnBrowseTemplatesDirectory = new KryptonButton();
        btnBrowseOutputDirectory = new KryptonButton();
        lblTemplatesDirectory = new KryptonLabel();
        lblOutputDirectory = new KryptonLabel();
        btnOK = new KryptonButton();
        btnCancel = new KryptonButton();
        SuspendLayout();
        // 
        // txtTemplatesDirectory
        // 
        txtTemplatesDirectory.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        txtTemplatesDirectory.Location = new System.Drawing.Point(163, 30);
        txtTemplatesDirectory.Name = "txtTemplatesDirectory";
        txtTemplatesDirectory.Size = new System.Drawing.Size(390, 23);
        txtTemplatesDirectory.TabIndex = 5;
        // 
        // txtOutputDirectory
        // 
        txtOutputDirectory.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        txtOutputDirectory.Location = new System.Drawing.Point(163, 89);
        txtOutputDirectory.Name = "txtOutputDirectory";
        txtOutputDirectory.Size = new System.Drawing.Size(390, 23);
        txtOutputDirectory.TabIndex = 15;
        // 
        // btnBrowseTemplatesDirectory
        // 
        btnBrowseTemplatesDirectory.Anchor = AnchorStyles.Top | AnchorStyles.Right;
        btnBrowseTemplatesDirectory.CornerRoundingRadius = -1F;
        btnBrowseTemplatesDirectory.Location = new System.Drawing.Point(563, 25);
        btnBrowseTemplatesDirectory.Name = "btnBrowseTemplatesDirectory";
        btnBrowseTemplatesDirectory.Size = new System.Drawing.Size(34, 34);
        btnBrowseTemplatesDirectory.TabIndex = 10;
        btnBrowseTemplatesDirectory.Values.Image = Properties.Resources.Browse_32x32;
        btnBrowseTemplatesDirectory.Values.Text = "";
        btnBrowseTemplatesDirectory.Click += btnBrowseTemplatesDirectory_Click;
        // 
        // btnBrowseOutputDirectory
        // 
        btnBrowseOutputDirectory.Anchor = AnchorStyles.Top | AnchorStyles.Right;
        btnBrowseOutputDirectory.CornerRoundingRadius = -1F;
        btnBrowseOutputDirectory.Location = new System.Drawing.Point(563, 84);
        btnBrowseOutputDirectory.Name = "btnBrowseOutputDirectory";
        btnBrowseOutputDirectory.Size = new System.Drawing.Size(34, 34);
        btnBrowseOutputDirectory.TabIndex = 20;
        btnBrowseOutputDirectory.Values.Image = Properties.Resources.Browse_32x32;
        btnBrowseOutputDirectory.Values.Text = "";
        btnBrowseOutputDirectory.Click += btnBrowseOutputDirectory_Click;
        // 
        // lblTemplatesDirectory
        // 
        lblTemplatesDirectory.Location = new System.Drawing.Point(19, 30);
        lblTemplatesDirectory.Name = "lblTemplatesDirectory";
        lblTemplatesDirectory.Size = new System.Drawing.Size(120, 20);
        lblTemplatesDirectory.TabIndex = 4;
        lblTemplatesDirectory.Values.Text = "Templates Directory";
        // 
        // lblOutputDirectory
        // 
        lblOutputDirectory.Location = new System.Drawing.Point(19, 89);
        lblOutputDirectory.Name = "lblOutputDirectory";
        lblOutputDirectory.Size = new System.Drawing.Size(103, 20);
        lblOutputDirectory.TabIndex = 5;
        lblOutputDirectory.Values.Text = "Output Directory";
        // 
        // btnOK
        // 
        btnOK.CornerRoundingRadius = -1F;
        btnOK.Location = new System.Drawing.Point(163, 135);
        btnOK.Name = "btnOK";
        btnOK.Size = new System.Drawing.Size(108, 42);
        btnOK.TabIndex = 25;
        btnOK.Values.Image = Properties.Resources.OK_32x32;
        btnOK.Values.Text = "OK";
        btnOK.Click += btnOK_Click;
        // 
        // btnCancel
        // 
        btnCancel.Anchor = AnchorStyles.Top | AnchorStyles.Right;
        btnCancel.CornerRoundingRadius = -1F;
        btnCancel.DialogResult = DialogResult.Cancel;
        btnCancel.Location = new System.Drawing.Point(445, 135);
        btnCancel.Name = "btnCancel";
        btnCancel.Size = new System.Drawing.Size(108, 42);
        btnCancel.TabIndex = 30;
        btnCancel.Values.Image = Properties.Resources.Cancel_32x32;
        btnCancel.Values.Text = "Cancel";
        btnCancel.Click += btnCancel_Click;
        // 
        // DirectorySelectionWindow
        // 
        AcceptButton = btnOK;
        AutoScaleBaseSize = new System.Drawing.Size(6, 16);
        CancelButton = btnCancel;
        ClientSize = new System.Drawing.Size(613, 186);
        Controls.Add(btnCancel);
        Controls.Add(btnOK);
        Controls.Add(lblOutputDirectory);
        Controls.Add(lblTemplatesDirectory);
        Controls.Add(btnBrowseOutputDirectory);
        Controls.Add(btnBrowseTemplatesDirectory);
        Controls.Add(txtOutputDirectory);
        Controls.Add(txtTemplatesDirectory);
        FormBorderStyle = FormBorderStyle.SizableToolWindow;
        Name = "DirectorySelectionWindow";
        ShowInTaskbar = false;
        StartPosition = FormStartPosition.CenterParent;
        Text = "File Generation Configuration";
        TopMost = true;
        ResumeLayout(false);
        PerformLayout();
    }

    #endregion Windows Form Designer generated code

    [System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE1006:Naming Styles", Justification = "Acceptable for WinForms event handlers")]
    private void btnBrowseTemplatesDirectory_Click(object sender, EventArgs e)
    {
        if (dlgFolderBrowserInput.ShowDialog() == DialogResult.OK)
        {
            txtTemplatesDirectory.Text = dlgFolderBrowserInput.SelectedPath;
        }
    }

    [System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE1006:Naming Styles", Justification = "Acceptable for WinForms event handlers")]
    private void btnBrowseOutputDirectory_Click(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(txtTemplatesDirectory.Text) && !string.IsNullOrEmpty(txtOutputDirectory.Text))
        {
            dlgFolderBrowserOutput.SelectedPath = txtTemplatesDirectory.Text;
        }
        if (dlgFolderBrowserOutput.ShowDialog() == DialogResult.OK)
        {
            txtOutputDirectory.Text = dlgFolderBrowserOutput.SelectedPath;
        }
    }

    [System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE1006:Naming Styles", Justification = "Acceptable for WinForms event handlers")]
    private void btnOK_Click(object sender, EventArgs e)
    {
        InputFolderSelected?.Invoke(this, new FolderEventArgs(txtTemplatesDirectory.Text.Trim()));
        OutputFolderSelected?.Invoke(this, new FolderEventArgs(txtOutputDirectory.Text.Trim()));
        Close();
    }

    public delegate void FolderEventHandler(object sender, FolderEventArgs args);

    public event FolderEventHandler InputFolderSelected;

    public event FolderEventHandler OutputFolderSelected;

    [System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE1006:Naming Styles", Justification = "Acceptable for WinForms event handlers")]
    private void btnCancel_Click(object sender, EventArgs e)
    {
        Close();
    }
}