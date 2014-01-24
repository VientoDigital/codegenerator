using System;
using System.ComponentModel;
using System.Data;
using System.Windows.Forms;
using iCodeGenerator.GenericDataAccess;
using iCodeGenerator.DatabaseStructure;

namespace iCodeGenerator.DatabaseNavigator
{
	public class ServerSettingsForm : Form
	{
		#region Attributes

		private Label uiConnectionStringLabel;
		private Label uiProviderTypeLabel;
		private Button uiTestConnectionButton;
		private ComboBox uiProviderTypeSelection;
		private Button uiSaveConnectionButton;
		private ToolTip uiMessageToolTip;
		private System.Windows.Forms.ComboBox uiConnectionStringComboList;
		private System.Windows.Forms.Button uiCancel;
		private System.Windows.Forms.Label uiConnectionStringHelp;
		private IContainer components;
		#endregion

		public ServerSettingsForm()
		{
			InitializeComponent();
			InitializingProviderTypes();
			LoadConnectionStrings();

			if (Server.ConnectionString.Length > 0)
			{
				uiConnectionStringComboList.SelectedIndex = uiConnectionStringComboList.FindString(Server.ConnectionString.Trim());
				uiProviderTypeSelection.SelectedIndex = (int) Server.ProviderType;
			}
		}

		private void LoadConnectionStrings()
		{
			uiConnectionStringComboList.Items.Clear();
			uiConnectionStringComboList.Items.AddRange(ConnectionStringManager.Instance.GetConnectionStrings());
		}

		private void InitializingProviderTypes()
		{
			uiProviderTypeSelection.Items.Add(new DataAccessProviderInfo(DataProviderType.SqlClient));
			uiProviderTypeSelection.Items.Add(new DataAccessProviderInfo(DataProviderType.MySql));
			uiProviderTypeSelection.Items.Add(new DataAccessProviderInfo(DataProviderType.PostgresSql));
			uiProviderTypeSelection.Items.Add(new DataAccessProviderInfo(DataProviderType.Oracle));

			uiProviderTypeSelection.DisplayMember = "Name";
		}

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
			this.components = new System.ComponentModel.Container();
			this.uiConnectionStringLabel = new System.Windows.Forms.Label();
			this.uiProviderTypeLabel = new System.Windows.Forms.Label();
			this.uiTestConnectionButton = new System.Windows.Forms.Button();
			this.uiProviderTypeSelection = new System.Windows.Forms.ComboBox();
			this.uiSaveConnectionButton = new System.Windows.Forms.Button();
			this.uiMessageToolTip = new System.Windows.Forms.ToolTip(this.components);
			this.uiConnectionStringComboList = new System.Windows.Forms.ComboBox();
			this.uiCancel = new System.Windows.Forms.Button();
			this.uiConnectionStringHelp = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// uiConnectionStringLabel
			// 
			this.uiConnectionStringLabel.Location = new System.Drawing.Point(7, 40);
			this.uiConnectionStringLabel.Name = "uiConnectionStringLabel";
			this.uiConnectionStringLabel.TabIndex = 0;
			this.uiConnectionStringLabel.Text = "Connection String";
			// 
			// uiProviderTypeLabel
			// 
			this.uiProviderTypeLabel.Location = new System.Drawing.Point(8, 8);
			this.uiProviderTypeLabel.Name = "uiProviderTypeLabel";
			this.uiProviderTypeLabel.TabIndex = 1;
			this.uiProviderTypeLabel.Text = "Provider Type";
			// 
			// uiTestConnectionButton
			// 
			this.uiTestConnectionButton.Location = new System.Drawing.Point(23, 104);
			this.uiTestConnectionButton.Name = "uiTestConnectionButton";
			this.uiTestConnectionButton.Size = new System.Drawing.Size(128, 23);
			this.uiTestConnectionButton.TabIndex = 10;
			this.uiTestConnectionButton.Text = "Test Connection";
			this.uiTestConnectionButton.Click += new System.EventHandler(this.uiTestConnectionButton_Click);
			// 
			// uiProviderTypeSelection
			// 
			this.uiProviderTypeSelection.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.uiProviderTypeSelection.Location = new System.Drawing.Point(112, 8);
			this.uiProviderTypeSelection.Name = "uiProviderTypeSelection";
			this.uiProviderTypeSelection.Size = new System.Drawing.Size(256, 21);
			this.uiProviderTypeSelection.TabIndex = 1;
			this.uiProviderTypeSelection.SelectedIndexChanged += new System.EventHandler(this.uiProviderTypeSelection_SelectedIndexChanged);
			// 
			// uiSaveConnectionButton
			// 
			this.uiSaveConnectionButton.Location = new System.Drawing.Point(183, 104);
			this.uiSaveConnectionButton.Name = "uiSaveConnectionButton";
			this.uiSaveConnectionButton.Size = new System.Drawing.Size(128, 23);
			this.uiSaveConnectionButton.TabIndex = 20;
			this.uiSaveConnectionButton.Text = "Save Connection";
			this.uiSaveConnectionButton.Click += new System.EventHandler(this.uiSaveConnectionButton_Click);
			// 
			// uiConnectionStringComboList
			// 
			this.uiConnectionStringComboList.Location = new System.Drawing.Point(112, 40);
			this.uiConnectionStringComboList.Name = "uiConnectionStringComboList";
			this.uiConnectionStringComboList.Size = new System.Drawing.Size(376, 21);
			this.uiConnectionStringComboList.TabIndex = 5;
			// 
			// uiCancel
			// 
			this.uiCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.uiCancel.Location = new System.Drawing.Point(343, 104);
			this.uiCancel.Name = "uiCancel";
			this.uiCancel.Size = new System.Drawing.Size(128, 23);
			this.uiCancel.TabIndex = 25;
			this.uiCancel.Text = "Cancel";
			this.uiCancel.Click += new System.EventHandler(this.uiCancel_Click);
			// 
			// uiConnectionStringHelp
			// 
			this.uiConnectionStringHelp.Font = new System.Drawing.Font("Courier New", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.uiConnectionStringHelp.ForeColor = System.Drawing.SystemColors.WindowText;
			this.uiConnectionStringHelp.Location = new System.Drawing.Point(15, 72);
			this.uiConnectionStringHelp.Name = "uiConnectionStringHelp";
			this.uiConnectionStringHelp.Size = new System.Drawing.Size(464, 23);
			this.uiConnectionStringHelp.TabIndex = 26;
			this.uiConnectionStringHelp.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// ServerSettingsForm
			// 
			this.AcceptButton = this.uiSaveConnectionButton;
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.CancelButton = this.uiCancel;
			this.ClientSize = new System.Drawing.Size(494, 136);
			this.Controls.Add(this.uiConnectionStringHelp);
			this.Controls.Add(this.uiCancel);
			this.Controls.Add(this.uiConnectionStringComboList);
			this.Controls.Add(this.uiConnectionStringLabel);
			this.Controls.Add(this.uiProviderTypeLabel);
			this.Controls.Add(this.uiTestConnectionButton);
			this.Controls.Add(this.uiProviderTypeSelection);
			this.Controls.Add(this.uiSaveConnectionButton);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "ServerSettingsForm";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Connection String";
			this.ResumeLayout(false);

		}

		#endregion

		private void uiTestConnectionButton_Click(object sender, EventArgs e)
		{
			if (uiProviderTypeSelection.SelectedIndex >= 0)
			{
				DataAccessProviderFactory dataAccessProvider = new DataAccessProviderFactory(((DataAccessProviderInfo) uiProviderTypeSelection.SelectedItem).ProviderType);
				IDbConnection connection = dataAccessProvider.CreateConnection(uiConnectionStringComboList.Text.Trim());
				try
				{
					connection.Open();
					MessageBox.Show("Connection Succesfull");
				}
				catch (Exception e1)
				{
					MessageBox.Show(e1.Message);
				}
				finally
				{
					if (connection.State != ConnectionState.Closed)
					{
						connection.Close();
					}
				}
			}
		}

		private void uiSaveConnectionButton_Click(object sender, EventArgs e)
		{
			if(uiProviderTypeSelection.SelectedIndex >= 0)
			{
				Server.ProviderType = ((DataAccessProviderInfo)uiProviderTypeSelection.SelectedItem).ProviderType;
				bool IsNewConnectionString = uiConnectionStringComboList.SelectedIndex == -1;

				if(IsNewConnectionString)
				{
					ConnectionStringManager.Instance.Add(uiConnectionStringComboList.Text.Trim());
				}
				Server.ConnectionString = uiConnectionStringComboList.Text.Trim();
			}
			DialogResult = DialogResult.OK;
			Hide();
		}

		private void uiProviderTypeSelection_SelectedIndexChanged(object sender, EventArgs e)
		{
			if ( uiProviderTypeSelection.SelectedIndex >= 0 )
			{
				string connectionStringFormat = new DataAccessProviderInfo(((DataAccessProviderInfo) uiProviderTypeSelection.SelectedItem).ProviderType).ConnectionStringFormat;
				uiConnectionStringHelp.Text = connectionStringFormat;
				uiMessageToolTip.SetToolTip(uiConnectionStringComboList, connectionStringFormat);
				uiConnectionStringComboList.Focus();
			}
		}

		private void uiCancel_Click(object sender, System.EventArgs e)
		{
			Close();
		}
	}
}