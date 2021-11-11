using System;
using System.ComponentModel;
using System.Windows.Forms;
using ComponentFactory.Krypton.Toolkit;

namespace CodeGenerator.CodeGenerator.UI
{
    /// <summary>
    /// Summary description for DirectorySelectionWindow.
    /// </summary>
    public class DirectorySelectionWindow : KryptonForm
    {
        private FolderBrowserDialog uiInputFolderDialog;
        private FolderBrowserDialog uiOutputFolderDialog;
        private KryptonTextBox uiInputFolderSelectedTextBox;
        private KryptonTextBox uiOutputFolderSelectedTextBox;
        private KryptonButton uiInputFolderButton;
        private KryptonButton uiOutputFolderButton;
        private KryptonLabel label1;
        private KryptonLabel label2;
        private KryptonButton uiOkButton;
        private KryptonButton uiCancelButton;

        /// <summary>
        /// Required designer variable.
        /// </summary>
        private Container components = null;

        public DirectorySelectionWindow()
        {
            //
            // Required for Windows Form Designer support
            //
            InitializeComponent();
        }

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (components != null)
                {
                    components.Dispose();
                }
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.uiInputFolderDialog = new System.Windows.Forms.FolderBrowserDialog();
            this.uiOutputFolderDialog = new System.Windows.Forms.FolderBrowserDialog();
            this.uiInputFolderSelectedTextBox = new ComponentFactory.Krypton.Toolkit.KryptonTextBox();
            this.uiOutputFolderSelectedTextBox = new ComponentFactory.Krypton.Toolkit.KryptonTextBox();
            this.uiInputFolderButton = new ComponentFactory.Krypton.Toolkit.KryptonButton();
            this.uiOutputFolderButton = new ComponentFactory.Krypton.Toolkit.KryptonButton();
            this.label1 = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.label2 = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.uiOkButton = new ComponentFactory.Krypton.Toolkit.KryptonButton();
            this.uiCancelButton = new ComponentFactory.Krypton.Toolkit.KryptonButton();
            this.SuspendLayout();
            // 
            // uiInputFolderSelectedTextBox
            // 
            this.uiInputFolderSelectedTextBox.Location = new System.Drawing.Point(136, 24);
            this.uiInputFolderSelectedTextBox.Name = "uiInputFolderSelectedTextBox";
            this.uiInputFolderSelectedTextBox.Size = new System.Drawing.Size(280, 23);
            this.uiInputFolderSelectedTextBox.TabIndex = 5;
            // 
            // uiOutputFolderSelectedTextBox
            // 
            this.uiOutputFolderSelectedTextBox.Location = new System.Drawing.Point(136, 72);
            this.uiOutputFolderSelectedTextBox.Name = "uiOutputFolderSelectedTextBox";
            this.uiOutputFolderSelectedTextBox.Size = new System.Drawing.Size(280, 23);
            this.uiOutputFolderSelectedTextBox.TabIndex = 15;
            // 
            // uiInputFolderButton
            // 
            this.uiInputFolderButton.Location = new System.Drawing.Point(424, 24);
            this.uiInputFolderButton.Name = "uiInputFolderButton";
            this.uiInputFolderButton.Size = new System.Drawing.Size(32, 24);
            this.uiInputFolderButton.TabIndex = 10;
            this.uiInputFolderButton.Values.Text = "...";
            this.uiInputFolderButton.Click += new System.EventHandler(this.uiInputFolderButton_Click);
            // 
            // uiOutputFolderButton
            // 
            this.uiOutputFolderButton.Location = new System.Drawing.Point(424, 72);
            this.uiOutputFolderButton.Name = "uiOutputFolderButton";
            this.uiOutputFolderButton.Size = new System.Drawing.Size(32, 24);
            this.uiOutputFolderButton.TabIndex = 20;
            this.uiOutputFolderButton.Values.Text = "...";
            this.uiOutputFolderButton.Click += new System.EventHandler(this.uiOutputFolderButton_Click);
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(16, 24);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(120, 20);
            this.label1.TabIndex = 4;
            this.label1.Values.Text = "Templates Directory";
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(16, 72);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(103, 20);
            this.label2.TabIndex = 5;
            this.label2.Values.Text = "Output Directory";
            // 
            // uiOkButton
            // 
            this.uiOkButton.Location = new System.Drawing.Point(136, 110);
            this.uiOkButton.Name = "uiOkButton";
            this.uiOkButton.Size = new System.Drawing.Size(90, 25);
            this.uiOkButton.TabIndex = 25;
            this.uiOkButton.Values.Text = "Ok";
            this.uiOkButton.Click += new System.EventHandler(this.uiOkButton_Click);
            // 
            // uiCancelButton
            // 
            this.uiCancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.uiCancelButton.Location = new System.Drawing.Point(326, 110);
            this.uiCancelButton.Name = "uiCancelButton";
            this.uiCancelButton.Size = new System.Drawing.Size(90, 25);
            this.uiCancelButton.TabIndex = 30;
            this.uiCancelButton.Values.Text = "Cancel";
            this.uiCancelButton.Click += new System.EventHandler(this.uiCancelButton_Click);
            // 
            // DirectorySelectionWindow
            // 
            this.AcceptButton = this.uiOkButton;
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.CancelButton = this.uiCancelButton;
            this.ClientSize = new System.Drawing.Size(476, 147);
            this.Controls.Add(this.uiCancelButton);
            this.Controls.Add(this.uiOkButton);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.uiOutputFolderButton);
            this.Controls.Add(this.uiInputFolderButton);
            this.Controls.Add(this.uiOutputFolderSelectedTextBox);
            this.Controls.Add(this.uiInputFolderSelectedTextBox);
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

        private void uiInputFolderButton_Click(object sender, EventArgs e)
        {
            if (uiInputFolderDialog.ShowDialog() == DialogResult.OK)
            {
                uiInputFolderSelectedTextBox.Text = uiInputFolderDialog.SelectedPath;
            }
        }

        private void uiOutputFolderButton_Click(object sender, EventArgs e)
        {
            if (uiInputFolderSelectedTextBox.Text.Length > 0 && uiOutputFolderSelectedTextBox.Text.Length == 0)
            {
                uiOutputFolderDialog.SelectedPath = uiInputFolderSelectedTextBox.Text;
            }
            if (uiOutputFolderDialog.ShowDialog() == DialogResult.OK)
            {
                uiOutputFolderSelectedTextBox.Text = uiOutputFolderDialog.SelectedPath;
            }
        }

        private void uiOkButton_Click(object sender, EventArgs e)
        {
            OnInputFolderSelected(new FolderEventArgs(uiInputFolderSelectedTextBox.Text.Trim()));
            OnOutputFolderSelected(new FolderEventArgs(uiOutputFolderSelectedTextBox.Text.Trim()));
            Close();
        }

        public delegate void FolderEventHandler(object sender, FolderEventArgs args);

        public event FolderEventHandler InputFolderSelected;

        public event FolderEventHandler OutputFolderSelected;

        protected virtual void OnInputFolderSelected(FolderEventArgs args)
        {
            if (InputFolderSelected != null)
            {
                InputFolderSelected(this, args);
            }
        }

        protected virtual void OnOutputFolderSelected(FolderEventArgs args)
        {
            if (OutputFolderSelected != null)
            {
                OutputFolderSelected(this, args);
            }
        }

        private void uiCancelButton_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}