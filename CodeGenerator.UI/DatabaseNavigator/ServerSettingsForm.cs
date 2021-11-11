using System;
using System.ComponentModel;
using System.Data;
using System.Windows.Forms;
using CodeGenerator.Data;
using CodeGenerator.Data.Structure;
using Krypton.Toolkit;

namespace CodeGenerator.DatabaseNavigator
{
    public class ServerSettingsForm : KryptonForm
    {
        private KryptonButton btnCancel;
        private KryptonButton btnSaveConnection;
        private KryptonButton btnTestConnection;
        private KryptonComboBox cmbConnectionString;
        private KryptonComboBox cmbProviderType;
        private IContainer components;
        private KryptonLabel lblConnectionString;
        private KryptonLabel lblConnectionStringHelp;
        private KryptonLabel lblProviderType;
        private ToolTip toolTip;

        public ServerSettingsForm()
        {
            InitializeComponent();

            cmbProviderType.Items.Add(new DataAccessProviderInfo(DataProviderType.SqlClient));
            cmbProviderType.Items.Add(new DataAccessProviderInfo(DataProviderType.MySql));
            cmbProviderType.Items.Add(new DataAccessProviderInfo(DataProviderType.PostgresSql));
            cmbProviderType.Items.Add(new DataAccessProviderInfo(DataProviderType.Oracle));
            cmbProviderType.DisplayMember = "Name";

            cmbConnectionString.Items.Clear();
            cmbConnectionString.Items.AddRange(ConnectionStringManager.Instance.GetConnectionStrings());

            if (Server.ConnectionString.Length > 0)
            {
                cmbConnectionString.SelectedIndex = cmbConnectionString.FindString(Server.ConnectionString.Trim());
                cmbProviderType.SelectedIndex = (int)Server.ProviderType;
            }
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

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE1006:Naming Styles", Justification = "<Pending>")]
        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE1006:Naming Styles", Justification = "<Pending>")]
        private void btnSaveConnection_Click(object sender, EventArgs e)
        {
            if (cmbProviderType.SelectedIndex >= 0)
            {
                Server.ProviderType = ((DataAccessProviderInfo)cmbProviderType.SelectedItem).ProviderType;
                bool IsNewConnectionString = cmbConnectionString.SelectedIndex == -1;

                if (IsNewConnectionString)
                {
                    ConnectionStringManager.Instance.Add(cmbConnectionString.Text.Trim());
                }
                Server.ConnectionString = cmbConnectionString.Text.Trim();
            }
            DialogResult = DialogResult.OK;
            Hide();
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE1006:Naming Styles", Justification = "<Pending>")]
        private void btnTestConnection_Click(object sender, EventArgs e)
        {
            if (cmbProviderType.SelectedIndex >= 0)
            {
                var dataAccessProviderFactory = new DataAccessProviderFactory(((DataAccessProviderInfo)cmbProviderType.SelectedItem).ProviderType);
                IDbConnection connection = dataAccessProviderFactory.CreateConnection(cmbConnectionString.Text.Trim());
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

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE1006:Naming Styles", Justification = "<Pending>")]
        private void cmbProviderType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbProviderType.SelectedIndex >= 0)
            {
                string connectionStringFormat = new DataAccessProviderInfo(((DataAccessProviderInfo)cmbProviderType.SelectedItem).ProviderType).ConnectionStringFormat;
                lblConnectionStringHelp.Text = connectionStringFormat;
                toolTip.SetToolTip(cmbConnectionString, connectionStringFormat);
                cmbConnectionString.Focus();
            }
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.lblConnectionString = new Krypton.Toolkit.KryptonLabel();
            this.lblProviderType = new Krypton.Toolkit.KryptonLabel();
            this.btnTestConnection = new Krypton.Toolkit.KryptonButton();
            this.cmbProviderType = new Krypton.Toolkit.KryptonComboBox();
            this.btnSaveConnection = new Krypton.Toolkit.KryptonButton();
            this.toolTip = new System.Windows.Forms.ToolTip(this.components);
            this.cmbConnectionString = new Krypton.Toolkit.KryptonComboBox();
            this.btnCancel = new Krypton.Toolkit.KryptonButton();
            this.lblConnectionStringHelp = new Krypton.Toolkit.KryptonLabel();
            ((System.ComponentModel.ISupportInitialize)(this.cmbProviderType)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbConnectionString)).BeginInit();
            this.SuspendLayout();
            //
            // lblConnectionString
            //
            this.lblConnectionString.Location = new System.Drawing.Point(8, 35);
            this.lblConnectionString.Name = "lblConnectionString";
            this.lblConnectionString.Size = new System.Drawing.Size(109, 20);
            this.lblConnectionString.TabIndex = 0;
            this.lblConnectionString.Values.Text = "Connection String";
            //
            // lblProviderType
            //
            this.lblProviderType.Location = new System.Drawing.Point(8, 8);
            this.lblProviderType.Name = "lblProviderType";
            this.lblProviderType.Size = new System.Drawing.Size(85, 20);
            this.lblProviderType.TabIndex = 1;
            this.lblProviderType.Values.Text = "Provider Type";
            //
            // btnTestConnection
            //
            this.btnTestConnection.Location = new System.Drawing.Point(8, 102);
            this.btnTestConnection.Name = "btnTestConnection";
            this.btnTestConnection.Size = new System.Drawing.Size(128, 35);
            this.btnTestConnection.TabIndex = 10;
            this.btnTestConnection.Values.Text = "Test Connection";
            this.btnTestConnection.Click += new System.EventHandler(this.btnTestConnection_Click);
            //
            // cmbProviderType
            //
            this.cmbProviderType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbProviderType.DropDownWidth = 256;
            this.cmbProviderType.Location = new System.Drawing.Point(150, 8);
            this.cmbProviderType.Name = "cmbProviderType";
            this.cmbProviderType.Size = new System.Drawing.Size(256, 21);
            this.cmbProviderType.TabIndex = 1;
            this.cmbProviderType.SelectedIndexChanged += new System.EventHandler(this.cmbProviderType_SelectedIndexChanged);
            //
            // btnSaveConnection
            //
            this.btnSaveConnection.Location = new System.Drawing.Point(142, 102);
            this.btnSaveConnection.Name = "btnSaveConnection";
            this.btnSaveConnection.Size = new System.Drawing.Size(128, 35);
            this.btnSaveConnection.TabIndex = 20;
            this.btnSaveConnection.Values.Text = "Save Connection";
            this.btnSaveConnection.Click += new System.EventHandler(this.btnSaveConnection_Click);
            //
            // cmbConnectionString
            //
            this.cmbConnectionString.DropDownWidth = 522;
            this.cmbConnectionString.Location = new System.Drawing.Point(150, 35);
            this.cmbConnectionString.Name = "cmbConnectionString";
            this.cmbConnectionString.Size = new System.Drawing.Size(522, 21);
            this.cmbConnectionString.TabIndex = 5;
            //
            // btnCancel
            //
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(278, 102);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(128, 35);
            this.btnCancel.TabIndex = 25;
            this.btnCancel.Values.Text = "Cancel";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            //
            // lblConnectionStringHelp
            //
            this.lblConnectionStringHelp.Font = new System.Drawing.Font("Courier New", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblConnectionStringHelp.ForeColor = System.Drawing.SystemColors.WindowText;
            this.lblConnectionStringHelp.Location = new System.Drawing.Point(8, 76);
            this.lblConnectionStringHelp.Name = "lblConnectionStringHelp";
            this.lblConnectionStringHelp.Size = new System.Drawing.Size(39, 20);
            this.lblConnectionStringHelp.TabIndex = 26;
            //
            // ServerSettingsForm
            //
            this.AcceptButton = this.btnSaveConnection;
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(684, 157);
            this.Controls.Add(this.lblConnectionStringHelp);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.cmbConnectionString);
            this.Controls.Add(this.lblConnectionString);
            this.Controls.Add(this.lblProviderType);
            this.Controls.Add(this.btnTestConnection);
            this.Controls.Add(this.cmbProviderType);
            this.Controls.Add(this.btnSaveConnection);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ServerSettingsForm";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Connection String";
            ((System.ComponentModel.ISupportInitialize)(this.cmbProviderType)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbConnectionString)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion Windows Form Designer generated code
    }
}