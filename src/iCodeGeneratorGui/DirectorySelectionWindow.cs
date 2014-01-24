using System;
using System.ComponentModel;
using System.Windows.Forms;

namespace iCodeGenerator.iCodeGeneratorGui
{
	/// <summary>
	/// Summary description for DirectorySelectionWindow.
	/// </summary>
	public class DirectorySelectionWindow : Form
	{
		private FolderBrowserDialog uiInputFolderDialog;
		private FolderBrowserDialog uiOutputFolderDialog;
		private TextBox uiInputFolderSelectedTextBox;
		private TextBox uiOutputFolderSelectedTextBox;
		private Button uiInputFolderButton;
		private Button uiOutputFolderButton;
		private Label label1;
		private Label label2;
		private Button uiOkButton;
		private Button uiCancelButton;
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
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if(components != null)
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
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
			this.uiInputFolderSelectedTextBox = new System.Windows.Forms.TextBox();
			this.uiOutputFolderSelectedTextBox = new System.Windows.Forms.TextBox();
			this.uiInputFolderButton = new System.Windows.Forms.Button();
			this.uiOutputFolderButton = new System.Windows.Forms.Button();
			this.label1 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.uiOkButton = new System.Windows.Forms.Button();
			this.uiCancelButton = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// uiInputFolderSelectedTextBox
			// 
			this.uiInputFolderSelectedTextBox.Location = new System.Drawing.Point(136, 24);
			this.uiInputFolderSelectedTextBox.Name = "uiInputFolderSelectedTextBox";
			this.uiInputFolderSelectedTextBox.Size = new System.Drawing.Size(280, 20);
			this.uiInputFolderSelectedTextBox.TabIndex = 5;
			this.uiInputFolderSelectedTextBox.Text = "";
			// 
			// uiOutputFolderSelectedTextBox
			// 
			this.uiOutputFolderSelectedTextBox.Location = new System.Drawing.Point(136, 72);
			this.uiOutputFolderSelectedTextBox.Name = "uiOutputFolderSelectedTextBox";
			this.uiOutputFolderSelectedTextBox.Size = new System.Drawing.Size(280, 20);
			this.uiOutputFolderSelectedTextBox.TabIndex = 15;
			this.uiOutputFolderSelectedTextBox.Text = "";
			// 
			// uiInputFolderButton
			// 
			this.uiInputFolderButton.Location = new System.Drawing.Point(424, 24);
			this.uiInputFolderButton.Name = "uiInputFolderButton";
			this.uiInputFolderButton.Size = new System.Drawing.Size(32, 24);
			this.uiInputFolderButton.TabIndex = 10;
			this.uiInputFolderButton.Text = "...";
			this.uiInputFolderButton.Click += new System.EventHandler(this.uiInputFolderButton_Click);
			// 
			// uiOutputFolderButton
			// 
			this.uiOutputFolderButton.Location = new System.Drawing.Point(424, 72);
			this.uiOutputFolderButton.Name = "uiOutputFolderButton";
			this.uiOutputFolderButton.Size = new System.Drawing.Size(32, 23);
			this.uiOutputFolderButton.TabIndex = 20;
			this.uiOutputFolderButton.Text = "...";
			this.uiOutputFolderButton.Click += new System.EventHandler(this.uiOutputFolderButton_Click);
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(16, 24);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(112, 23);
			this.label1.TabIndex = 4;
			this.label1.Text = "Templates Directory";
			this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(16, 72);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(112, 23);
			this.label2.TabIndex = 5;
			this.label2.Text = "Output Directory";
			this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// uiOkButton
			// 
			this.uiOkButton.Location = new System.Drawing.Point(120, 120);
			this.uiOkButton.Name = "uiOkButton";
			this.uiOkButton.TabIndex = 25;
			this.uiOkButton.Text = "Ok";
			this.uiOkButton.Click += new System.EventHandler(this.uiOkButton_Click);
			// 
			// uiCancelButton
			// 
			this.uiCancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.uiCancelButton.Location = new System.Drawing.Point(272, 120);
			this.uiCancelButton.Name = "uiCancelButton";
			this.uiCancelButton.TabIndex = 30;
			this.uiCancelButton.Text = "Cancel";
			this.uiCancelButton.Click += new System.EventHandler(this.uiCancelButton_Click);
			// 
			// DirectorySelectionWindow
			// 
			this.AcceptButton = this.uiOkButton;
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.CancelButton = this.uiCancelButton;
			this.ClientSize = new System.Drawing.Size(472, 166);
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

		}
		#endregion

		private void uiInputFolderButton_Click(object sender, EventArgs e)
		{
			if(uiInputFolderDialog.ShowDialog() == DialogResult.OK)
			{
				uiInputFolderSelectedTextBox.Text = uiInputFolderDialog.SelectedPath;
			}			
		}

		private void uiOutputFolderButton_Click(object sender, EventArgs e)
		{
			if(uiInputFolderSelectedTextBox.Text.Length > 0 && uiOutputFolderSelectedTextBox.Text.Length == 0)
			{
				uiOutputFolderDialog.SelectedPath = uiInputFolderSelectedTextBox.Text;
			}
			if(uiOutputFolderDialog.ShowDialog() == DialogResult.OK)
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
			if(InputFolderSelected != null)
			{
				InputFolderSelected(this,args);
			}
		}

	    protected virtual void OnOutputFolderSelected(FolderEventArgs args)
		{
			if(OutputFolderSelected != null)
			{
				OutputFolderSelected(this,args);
			}
		}

		private void uiCancelButton_Click(object sender, EventArgs e)
		{
			Close();
		}
	}
}
