using System;
using System.ComponentModel;
using System.Linq;
using System.Windows.Forms;
using CodeGenerator.Data;
using CodeGenerator.Data.Structure;
using CodeGenerator.Shared.Extensions;
using Krypton.Toolkit;

namespace CodeGenerator.UI
{
    public class ConnectionForm : KryptonForm
    {
        private KryptonButton btnCancel;
        private KryptonButton btnConnect;
        private KryptonButton btnTestConnection;
        private KryptonComboBox cmbConnectionString;
        private KryptonComboBox cmbProviderType;
        private IContainer components;
        private KryptonLabel lblConnectionString;
        private KryptonLabel lblConnectionStringHelp;
        private KryptonLabel lblProviderType;
        private ToolTip toolTip;

        public ConnectionForm()
        {
            InitializeComponent();

            cmbProviderType.DataSource = Enum.GetValues(typeof(ProviderType));
            cmbProviderType.SelectedIndex = 0;

            cmbConnectionString.Items.Clear();

            if (!ConfigFile.Instance.ConnectionStrings.IsNullOrEmpty())
            {
                cmbConnectionString.Items.AddRange(ConfigFile.Instance.ConnectionStrings.ToArray());
                cmbConnectionString.SelectedIndex = 0;
            }

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
        private void btnConnect_Click(object sender, EventArgs e)
        {
            if (TestConnection())
            {
                Server.ProviderType = ((ProviderType)cmbProviderType.SelectedItem);
                bool isNewConnectionString = cmbConnectionString.SelectedIndex == -1;
                string selectedConnectionString = cmbConnectionString.Text.Trim();

                if (isNewConnectionString)
                {
                    ConfigFile.Instance.ConnectionStrings.Add(selectedConnectionString);
                }

                Server.ConnectionString = selectedConnectionString;

                DialogResult = DialogResult.OK;
                Hide();
            }
        }

        private bool TestConnection()
        {
            if (cmbProviderType.SelectedIndex >= 0)
            {
                var providerFactory = new ProviderFactory(((ProviderType)cmbProviderType.SelectedItem));

                try
                {
                    using (var connection = providerFactory.CreateConnection(cmbConnectionString.Text.Trim()))
                    {
                        connection.Open();
                        return true;
                    }
                }
                catch (Exception x)
                {
                    MessageBox.Show(x.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
            }
            else
            {
                MessageBox.Show("Please select a provider!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }

            return false;
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE1006:Naming Styles", Justification = "<Pending>")]
        private void btnTestConnection_Click(object sender, EventArgs e)
        {
            if (TestConnection())
            {
                MessageBox.Show("Connected successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE1006:Naming Styles", Justification = "<Pending>")]
        private void cmbProviderType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbProviderType.SelectedIndex >= 0)
            {
                string connectionStringFormat = new ProviderInfo(((ProviderType)cmbProviderType.SelectedItem)).ConnectionStringFormat;
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
            this.btnConnect = new Krypton.Toolkit.KryptonButton();
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
            this.lblConnectionString.Location = new System.Drawing.Point(10, 43);
            this.lblConnectionString.Name = "lblConnectionString";
            this.lblConnectionString.Size = new System.Drawing.Size(109, 20);
            this.lblConnectionString.TabIndex = 0;
            this.lblConnectionString.Values.Text = "Connection String";
            // 
            // lblProviderType
            // 
            this.lblProviderType.Location = new System.Drawing.Point(10, 10);
            this.lblProviderType.Name = "lblProviderType";
            this.lblProviderType.Size = new System.Drawing.Size(85, 20);
            this.lblProviderType.TabIndex = 1;
            this.lblProviderType.Values.Text = "Provider Type";
            // 
            // btnTestConnection
            // 
            this.btnTestConnection.Location = new System.Drawing.Point(10, 126);
            this.btnTestConnection.Name = "btnTestConnection";
            this.btnTestConnection.Size = new System.Drawing.Size(153, 43);
            this.btnTestConnection.TabIndex = 10;
            this.btnTestConnection.Values.Text = "Test Connection";
            this.btnTestConnection.Click += new System.EventHandler(this.btnTestConnection_Click);
            // 
            // cmbProviderType
            // 
            this.cmbProviderType.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawVariable;
            this.cmbProviderType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbProviderType.DropDownWidth = 256;
            this.cmbProviderType.IntegralHeight = false;
            this.cmbProviderType.Location = new System.Drawing.Point(180, 10);
            this.cmbProviderType.Name = "cmbProviderType";
            this.cmbProviderType.Size = new System.Drawing.Size(307, 21);
            this.cmbProviderType.TabIndex = 1;
            this.cmbProviderType.SelectedIndexChanged += new System.EventHandler(this.cmbProviderType_SelectedIndexChanged);
            // 
            // btnConnect
            // 
            this.btnConnect.Location = new System.Drawing.Point(170, 126);
            this.btnConnect.Name = "btnConnect";
            this.btnConnect.Size = new System.Drawing.Size(154, 43);
            this.btnConnect.TabIndex = 20;
            this.btnConnect.Values.Text = "Connect";
            this.btnConnect.Click += new System.EventHandler(this.btnConnect_Click);
            // 
            // cmbConnectionString
            // 
            this.cmbConnectionString.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawVariable;
            this.cmbConnectionString.DropDownWidth = 522;
            this.cmbConnectionString.IntegralHeight = false;
            this.cmbConnectionString.Location = new System.Drawing.Point(180, 43);
            this.cmbConnectionString.Name = "cmbConnectionString";
            this.cmbConnectionString.Size = new System.Drawing.Size(626, 21);
            this.cmbConnectionString.TabIndex = 5;
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(334, 126);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(153, 43);
            this.btnCancel.TabIndex = 25;
            this.btnCancel.Values.Text = "Cancel";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // lblConnectionStringHelp
            // 
            this.lblConnectionStringHelp.Font = new System.Drawing.Font("Courier New", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.lblConnectionStringHelp.ForeColor = System.Drawing.SystemColors.WindowText;
            this.lblConnectionStringHelp.Location = new System.Drawing.Point(10, 94);
            this.lblConnectionStringHelp.Name = "lblConnectionStringHelp";
            this.lblConnectionStringHelp.Size = new System.Drawing.Size(39, 20);
            this.lblConnectionStringHelp.TabIndex = 26;
            // 
            // ConnectionForm
            // 
            this.AcceptButton = this.btnConnect;
            this.AutoScaleBaseSize = new System.Drawing.Size(6, 16);
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(814, 181);
            this.Controls.Add(this.lblConnectionStringHelp);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.cmbConnectionString);
            this.Controls.Add(this.lblConnectionString);
            this.Controls.Add(this.lblProviderType);
            this.Controls.Add(this.btnTestConnection);
            this.Controls.Add(this.cmbProviderType);
            this.Controls.Add(this.btnConnect);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ConnectionForm";
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