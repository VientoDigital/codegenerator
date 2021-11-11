using System;
using System.ComponentModel;
using System.Data;
using System.Windows.Forms;
using ComponentFactory.Krypton.Toolkit;
using CodeGenerator.DatabaseStructure;
using CodeGenerator.GenericDataAccess;

namespace CodeGenerator.DatabaseNavigator
{
    public class ServerSettingsForm : KryptonForm
    {
        #region Attributes

        private KryptonLabel uiConnectionStringLabel;
        private KryptonLabel uiProviderTypeLabel;
        private KryptonButton uiTestConnectionButton;
        private KryptonComboBox uiProviderTypeSelection;
        private KryptonButton uiSaveConnectionButton;
        private ToolTip uiMessageToolTip;
        private KryptonComboBox uiConnectionStringComboList;
        private KryptonButton uiCancel;
        private KryptonLabel uiConnectionStringHelp;
        private IContainer components;

        #endregion Attributes

        public ServerSettingsForm()
        {
            InitializeComponent();
            InitializingProviderTypes();
            LoadConnectionStrings();

            if (Server.ConnectionString.Length > 0)
            {
                uiConnectionStringComboList.SelectedIndex = uiConnectionStringComboList.FindString(Server.ConnectionString.Trim());
                uiProviderTypeSelection.SelectedIndex = (int)Server.ProviderType;
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
            this.uiConnectionStringLabel = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.uiProviderTypeLabel = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            this.uiTestConnectionButton = new ComponentFactory.Krypton.Toolkit.KryptonButton();
            this.uiProviderTypeSelection = new ComponentFactory.Krypton.Toolkit.KryptonComboBox();
            this.uiSaveConnectionButton = new ComponentFactory.Krypton.Toolkit.KryptonButton();
            this.uiMessageToolTip = new System.Windows.Forms.ToolTip(this.components);
            this.uiConnectionStringComboList = new ComponentFactory.Krypton.Toolkit.KryptonComboBox();
            this.uiCancel = new ComponentFactory.Krypton.Toolkit.KryptonButton();
            this.uiConnectionStringHelp = new ComponentFactory.Krypton.Toolkit.KryptonLabel();
            ((System.ComponentModel.ISupportInitialize)(this.uiProviderTypeSelection)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.uiConnectionStringComboList)).BeginInit();
            this.SuspendLayout();
            // 
            // uiConnectionStringLabel
            // 
            this.uiConnectionStringLabel.Location = new System.Drawing.Point(8, 35);
            this.uiConnectionStringLabel.Name = "uiConnectionStringLabel";
            this.uiConnectionStringLabel.Size = new System.Drawing.Size(109, 20);
            this.uiConnectionStringLabel.TabIndex = 0;
            this.uiConnectionStringLabel.Values.Text = "Connection String";
            // 
            // uiProviderTypeLabel
            // 
            this.uiProviderTypeLabel.Location = new System.Drawing.Point(8, 8);
            this.uiProviderTypeLabel.Name = "uiProviderTypeLabel";
            this.uiProviderTypeLabel.Size = new System.Drawing.Size(85, 20);
            this.uiProviderTypeLabel.TabIndex = 1;
            this.uiProviderTypeLabel.Values.Text = "Provider Type";
            // 
            // uiTestConnectionButton
            // 
            this.uiTestConnectionButton.Location = new System.Drawing.Point(8, 102);
            this.uiTestConnectionButton.Name = "uiTestConnectionButton";
            this.uiTestConnectionButton.Size = new System.Drawing.Size(128, 35);
            this.uiTestConnectionButton.TabIndex = 10;
            this.uiTestConnectionButton.Values.Text = "Test Connection";
            this.uiTestConnectionButton.Click += new System.EventHandler(this.uiTestConnectionButton_Click);
            // 
            // uiProviderTypeSelection
            // 
            this.uiProviderTypeSelection.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.uiProviderTypeSelection.DropDownWidth = 256;
            this.uiProviderTypeSelection.Location = new System.Drawing.Point(150, 8);
            this.uiProviderTypeSelection.Name = "uiProviderTypeSelection";
            this.uiProviderTypeSelection.Size = new System.Drawing.Size(256, 21);
            this.uiProviderTypeSelection.TabIndex = 1;
            this.uiProviderTypeSelection.SelectedIndexChanged += new System.EventHandler(this.uiProviderTypeSelection_SelectedIndexChanged);
            // 
            // uiSaveConnectionButton
            // 
            this.uiSaveConnectionButton.Location = new System.Drawing.Point(142, 102);
            this.uiSaveConnectionButton.Name = "uiSaveConnectionButton";
            this.uiSaveConnectionButton.Size = new System.Drawing.Size(128, 35);
            this.uiSaveConnectionButton.TabIndex = 20;
            this.uiSaveConnectionButton.Values.Text = "Save Connection";
            this.uiSaveConnectionButton.Click += new System.EventHandler(this.uiSaveConnectionButton_Click);
            // 
            // uiConnectionStringComboList
            // 
            this.uiConnectionStringComboList.DropDownWidth = 522;
            this.uiConnectionStringComboList.Location = new System.Drawing.Point(150, 35);
            this.uiConnectionStringComboList.Name = "uiConnectionStringComboList";
            this.uiConnectionStringComboList.Size = new System.Drawing.Size(522, 21);
            this.uiConnectionStringComboList.TabIndex = 5;
            // 
            // uiCancel
            // 
            this.uiCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.uiCancel.Location = new System.Drawing.Point(278, 102);
            this.uiCancel.Name = "uiCancel";
            this.uiCancel.Size = new System.Drawing.Size(128, 35);
            this.uiCancel.TabIndex = 25;
            this.uiCancel.Values.Text = "Cancel";
            this.uiCancel.Click += new System.EventHandler(this.uiCancel_Click);
            // 
            // uiConnectionStringHelp
            // 
            this.uiConnectionStringHelp.Font = new System.Drawing.Font("Courier New", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.uiConnectionStringHelp.ForeColor = System.Drawing.SystemColors.WindowText;
            this.uiConnectionStringHelp.Location = new System.Drawing.Point(8, 76);
            this.uiConnectionStringHelp.Name = "uiConnectionStringHelp";
            this.uiConnectionStringHelp.Size = new System.Drawing.Size(39, 20);
            this.uiConnectionStringHelp.TabIndex = 26;
            // 
            // ServerSettingsForm
            // 
            this.AcceptButton = this.uiSaveConnectionButton;
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.CancelButton = this.uiCancel;
            this.ClientSize = new System.Drawing.Size(684, 157);
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
            ((System.ComponentModel.ISupportInitialize)(this.uiProviderTypeSelection)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.uiConnectionStringComboList)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion Windows Form Designer generated code

        private void uiTestConnectionButton_Click(object sender, EventArgs e)
        {
            if (uiProviderTypeSelection.SelectedIndex >= 0)
            {
                var dataAccessProviderFactory = new DataAccessProviderFactory(((DataAccessProviderInfo)uiProviderTypeSelection.SelectedItem).ProviderType);
                IDbConnection connection = dataAccessProviderFactory.CreateConnection(uiConnectionStringComboList.Text.Trim());
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
            if (uiProviderTypeSelection.SelectedIndex >= 0)
            {
                Server.ProviderType = ((DataAccessProviderInfo)uiProviderTypeSelection.SelectedItem).ProviderType;
                bool IsNewConnectionString = uiConnectionStringComboList.SelectedIndex == -1;

                if (IsNewConnectionString)
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
            if (uiProviderTypeSelection.SelectedIndex >= 0)
            {
                string connectionStringFormat = new DataAccessProviderInfo(((DataAccessProviderInfo)uiProviderTypeSelection.SelectedItem).ProviderType).ConnectionStringFormat;
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