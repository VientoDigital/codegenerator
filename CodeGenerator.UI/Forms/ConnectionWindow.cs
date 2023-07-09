using System.ComponentModel;

namespace CodeGenerator.UI;

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

        cmbProviderType.DataSource = Enum.GetValues(typeof(DataSource));
        cmbProviderType.SelectedIndex = 0;

        cmbConnectionString.Items.Clear();

        if (!ConfigFile.Instance.ConnectionStrings.IsNullOrEmpty())
        {
            cmbConnectionString.Items.AddRange(ConfigFile.Instance.ConnectionStrings.ToArray());
            cmbConnectionString.SelectedIndex = 0;
        }

        if (!string.IsNullOrEmpty(Server.ConnectionString))
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

    [System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE1006:Naming Styles", Justification = "Acceptable for WinForms event handlers")]
    private void btnCancel_Click(object sender, EventArgs e)
    {
        Close();
    }

    [System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE1006:Naming Styles", Justification = "Acceptable for WinForms event handlers")]
    private void btnConnect_Click(object sender, EventArgs e)
    {
        if (TestConnection())
        {
            Server.ProviderType = (DataSource)cmbProviderType.SelectedItem;
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
            var providerFactory = new ProviderFactory(((DataSource)cmbProviderType.SelectedItem));

            try
            {
                using var connection = providerFactory.CreateConnection(cmbConnectionString.Text.Trim());
                connection.Open();
                return true;
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

    [System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE1006:Naming Styles", Justification = "Acceptable for WinForms event handlers")]
    private void btnTestConnection_Click(object sender, EventArgs e)
    {
        if (TestConnection())
        {
            MessageBox.Show("Connected successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }

    [System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE1006:Naming Styles", Justification = "Acceptable for WinForms event handlers")]
    private void cmbProviderType_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (cmbProviderType.SelectedIndex >= 0)
        {
            var providerType = (DataSource)cmbProviderType.SelectedItem;
            string connectionStringFormat = GetConnectionStringFormat(providerType);
            lblConnectionStringHelp.Text = connectionStringFormat;
            toolTip.SetToolTip(cmbConnectionString, connectionStringFormat);
            cmbConnectionString.Focus();
        }
    }

    private static string GetConnectionStringFormat(DataSource providerType) => providerType switch
    {
        DataSource.SqlServer => "Server=<SERVER>;User Id=<USERNAME>;Password=<PASSWORD>;",
        DataSource.MySql => "Server=<SERVER>;Uid=<USERNAME>;Pwd=<PASSWORD>;",
        DataSource.PostgresSql => "Host=<SERVER>;Port=<PORT>;User Id=<USERNAME>;Password=<PASSWORD>;",
        DataSource.Oracle => "Data Source=<SERVER>;User Id=<USERNAME>;Password=<PASSWORD>;Integrated Security=no;",
        _ => null,
    };

    #region Windows Form Designer generated code

    /// <summary>
    /// Required method for Designer support - do not modify
    /// the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
        components = new Container();
        lblConnectionString = new KryptonLabel();
        lblProviderType = new KryptonLabel();
        btnTestConnection = new KryptonButton();
        cmbProviderType = new KryptonComboBox();
        btnConnect = new KryptonButton();
        toolTip = new ToolTip(components);
        cmbConnectionString = new KryptonComboBox();
        btnCancel = new KryptonButton();
        lblConnectionStringHelp = new KryptonLabel();
        ((ISupportInitialize)cmbProviderType).BeginInit();
        ((ISupportInitialize)cmbConnectionString).BeginInit();
        SuspendLayout();
        // 
        // lblConnectionString
        // 
        lblConnectionString.Location = new System.Drawing.Point(10, 43);
        lblConnectionString.Name = "lblConnectionString";
        lblConnectionString.Size = new System.Drawing.Size(109, 20);
        lblConnectionString.TabIndex = 0;
        lblConnectionString.Values.Text = "Connection String";
        // 
        // lblProviderType
        // 
        lblProviderType.Location = new System.Drawing.Point(10, 10);
        lblProviderType.Name = "lblProviderType";
        lblProviderType.Size = new System.Drawing.Size(85, 20);
        lblProviderType.TabIndex = 1;
        lblProviderType.Values.Text = "Provider Type";
        // 
        // btnTestConnection
        // 
        btnTestConnection.CornerRoundingRadius = -1F;
        btnTestConnection.Location = new System.Drawing.Point(10, 126);
        btnTestConnection.Name = "btnTestConnection";
        btnTestConnection.Size = new System.Drawing.Size(153, 43);
        btnTestConnection.TabIndex = 10;
        btnTestConnection.Values.Image = Properties.Resources.TestTube_32x32;
        btnTestConnection.Values.Text = "Test Connection";
        btnTestConnection.Click += btnTestConnection_Click;
        // 
        // cmbProviderType
        // 
        cmbProviderType.CornerRoundingRadius = -1F;
        cmbProviderType.DropDownStyle = ComboBoxStyle.DropDownList;
        cmbProviderType.DropDownWidth = 256;
        cmbProviderType.IntegralHeight = false;
        cmbProviderType.Location = new System.Drawing.Point(180, 10);
        cmbProviderType.Name = "cmbProviderType";
        cmbProviderType.Size = new System.Drawing.Size(307, 21);
        cmbProviderType.TabIndex = 1;
        cmbProviderType.SelectedIndexChanged += cmbProviderType_SelectedIndexChanged;
        // 
        // btnConnect
        // 
        btnConnect.CornerRoundingRadius = -1F;
        btnConnect.Location = new System.Drawing.Point(170, 126);
        btnConnect.Name = "btnConnect";
        btnConnect.Size = new System.Drawing.Size(154, 43);
        btnConnect.TabIndex = 20;
        btnConnect.Values.Image = Properties.Resources.Connect_32x32;
        btnConnect.Values.Text = "Connect";
        btnConnect.Click += btnConnect_Click;
        // 
        // cmbConnectionString
        // 
        cmbConnectionString.CornerRoundingRadius = -1F;
        cmbConnectionString.DropDownWidth = 522;
        cmbConnectionString.IntegralHeight = false;
        cmbConnectionString.Location = new System.Drawing.Point(180, 43);
        cmbConnectionString.Name = "cmbConnectionString";
        cmbConnectionString.Size = new System.Drawing.Size(626, 21);
        cmbConnectionString.TabIndex = 5;
        // 
        // btnCancel
        // 
        btnCancel.CornerRoundingRadius = -1F;
        btnCancel.DialogResult = DialogResult.Cancel;
        btnCancel.Location = new System.Drawing.Point(334, 126);
        btnCancel.Name = "btnCancel";
        btnCancel.Size = new System.Drawing.Size(153, 43);
        btnCancel.TabIndex = 25;
        btnCancel.Values.Image = Properties.Resources.Cancel_32x32;
        btnCancel.Values.Text = "Cancel";
        btnCancel.Click += btnCancel_Click;
        // 
        // lblConnectionStringHelp
        // 
        lblConnectionStringHelp.Font = new System.Drawing.Font("Courier New", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
        lblConnectionStringHelp.ForeColor = System.Drawing.SystemColors.WindowText;
        lblConnectionStringHelp.Location = new System.Drawing.Point(10, 94);
        lblConnectionStringHelp.Name = "lblConnectionStringHelp";
        lblConnectionStringHelp.Size = new System.Drawing.Size(39, 20);
        lblConnectionStringHelp.TabIndex = 26;
        // 
        // ConnectionForm
        // 
        AcceptButton = btnConnect;
        AutoScaleBaseSize = new System.Drawing.Size(6, 16);
        CancelButton = btnCancel;
        ClientSize = new System.Drawing.Size(814, 181);
        Controls.Add(lblConnectionStringHelp);
        Controls.Add(btnCancel);
        Controls.Add(cmbConnectionString);
        Controls.Add(lblConnectionString);
        Controls.Add(lblProviderType);
        Controls.Add(btnTestConnection);
        Controls.Add(cmbProviderType);
        Controls.Add(btnConnect);
        FormBorderStyle = FormBorderStyle.FixedToolWindow;
        MaximizeBox = false;
        MinimizeBox = false;
        Name = "ConnectionForm";
        ShowInTaskbar = false;
        StartPosition = FormStartPosition.CenterParent;
        Text = "Connection String";
        ((ISupportInitialize)cmbProviderType).EndInit();
        ((ISupportInitialize)cmbConnectionString).EndInit();
        ResumeLayout(false);
        PerformLayout();
    }

    #endregion Windows Form Designer generated code
}