using System;
using System.Windows.Forms;
using Krypton.Toolkit;

namespace CodeGenerator.UI
{
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
            this.dlgFolderBrowserInput = new System.Windows.Forms.FolderBrowserDialog();
            this.dlgFolderBrowserOutput = new System.Windows.Forms.FolderBrowserDialog();
            this.txtTemplatesDirectory = new Krypton.Toolkit.KryptonTextBox();
            this.txtOutputDirectory = new Krypton.Toolkit.KryptonTextBox();
            this.btnBrowseTemplatesDirectory = new Krypton.Toolkit.KryptonButton();
            this.btnBrowseOutputDirectory = new Krypton.Toolkit.KryptonButton();
            this.lblTemplatesDirectory = new Krypton.Toolkit.KryptonLabel();
            this.lblOutputDirectory = new Krypton.Toolkit.KryptonLabel();
            this.btnOK = new Krypton.Toolkit.KryptonButton();
            this.btnCancel = new Krypton.Toolkit.KryptonButton();
            this.SuspendLayout();
            //
            // txtTemplatesDirectory
            //
            this.txtTemplatesDirectory.Location = new System.Drawing.Point(163, 30);
            this.txtTemplatesDirectory.Name = "txtTemplatesDirectory";
            this.txtTemplatesDirectory.Size = new System.Drawing.Size(336, 23);
            this.txtTemplatesDirectory.TabIndex = 5;
            //
            // txtOutputDirectory
            //
            this.txtOutputDirectory.Location = new System.Drawing.Point(163, 89);
            this.txtOutputDirectory.Name = "txtOutputDirectory";
            this.txtOutputDirectory.Size = new System.Drawing.Size(336, 23);
            this.txtOutputDirectory.TabIndex = 15;
            //
            // btnBrowseTemplatesDirectory
            //
            this.btnBrowseTemplatesDirectory.Location = new System.Drawing.Point(509, 30);
            this.btnBrowseTemplatesDirectory.Name = "btnBrowseTemplatesDirectory";
            this.btnBrowseTemplatesDirectory.Size = new System.Drawing.Size(38, 29);
            this.btnBrowseTemplatesDirectory.TabIndex = 10;
            this.btnBrowseTemplatesDirectory.Values.Text = "…";
            this.btnBrowseTemplatesDirectory.Click += new System.EventHandler(this.btnBrowseTemplatesDirectory_Click);
            //
            // btnBrowseOutputDirectory
            //
            this.btnBrowseOutputDirectory.Location = new System.Drawing.Point(509, 89);
            this.btnBrowseOutputDirectory.Name = "btnBrowseOutputDirectory";
            this.btnBrowseOutputDirectory.Size = new System.Drawing.Size(38, 29);
            this.btnBrowseOutputDirectory.TabIndex = 20;
            this.btnBrowseOutputDirectory.Values.Text = "…";
            this.btnBrowseOutputDirectory.Click += new System.EventHandler(this.btnBrowseOutputDirectory_Click);
            //
            // lblTemplatesDirectory
            //
            this.lblTemplatesDirectory.Location = new System.Drawing.Point(19, 30);
            this.lblTemplatesDirectory.Name = "lblTemplatesDirectory";
            this.lblTemplatesDirectory.Size = new System.Drawing.Size(120, 20);
            this.lblTemplatesDirectory.TabIndex = 4;
            this.lblTemplatesDirectory.Values.Text = "Templates Directory";
            //
            // lblOutputDirectory
            //
            this.lblOutputDirectory.Location = new System.Drawing.Point(19, 89);
            this.lblOutputDirectory.Name = "lblOutputDirectory";
            this.lblOutputDirectory.Size = new System.Drawing.Size(103, 20);
            this.lblOutputDirectory.TabIndex = 5;
            this.lblOutputDirectory.Values.Text = "Output Directory";
            //
            // btnOK
            //
            this.btnOK.Location = new System.Drawing.Point(163, 135);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(108, 31);
            this.btnOK.TabIndex = 25;
            this.btnOK.Values.Text = "OK";
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            //
            // btnCancel
            //
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(391, 135);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(108, 31);
            this.btnCancel.TabIndex = 30;
            this.btnCancel.Values.Text = "Cancel";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            //
            // DirectorySelectionWindow
            //
            this.AcceptButton = this.btnOK;
            this.AutoScaleBaseSize = new System.Drawing.Size(6, 16);
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(559, 176);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.lblOutputDirectory);
            this.Controls.Add(this.lblTemplatesDirectory);
            this.Controls.Add(this.btnBrowseOutputDirectory);
            this.Controls.Add(this.btnBrowseTemplatesDirectory);
            this.Controls.Add(this.txtOutputDirectory);
            this.Controls.Add(this.txtTemplatesDirectory);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.Name = "DirectorySelectionWindow";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "File Generation Configuration";
            this.TopMost = true;
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion Windows Form Designer generated code

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE1006:Naming Styles", Justification = "<Pending>")]
        private void btnBrowseTemplatesDirectory_Click(object sender, EventArgs e)
        {
            if (dlgFolderBrowserInput.ShowDialog() == DialogResult.OK)
            {
                txtTemplatesDirectory.Text = dlgFolderBrowserInput.SelectedPath;
            }
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE1006:Naming Styles", Justification = "<Pending>")]
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

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE1006:Naming Styles", Justification = "<Pending>")]
        private void btnOK_Click(object sender, EventArgs e)
        {
            InputFolderSelected?.Invoke(this, new FolderEventArgs(txtTemplatesDirectory.Text.Trim()));
            OutputFolderSelected?.Invoke(this, new FolderEventArgs(txtOutputDirectory.Text.Trim()));
            Close();
        }

        public delegate void FolderEventHandler(object sender, FolderEventArgs args);

        public event FolderEventHandler InputFolderSelected;

        public event FolderEventHandler OutputFolderSelected;

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE1006:Naming Styles", Justification = "<Pending>")]
        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}