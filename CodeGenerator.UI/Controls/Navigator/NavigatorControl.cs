using System.ComponentModel;

namespace CodeGenerator.UI;

// TODO: Support generating files for multiple tables at once.. we need to set Checkboxes enabled on the treeView and then:
//  disable for database nodes (only show checkboxes on table nodes) by using this solution:
//  https://stackoverflow.com/questions/698369/how-to-disable-a-winforms-treeview-node-checkbox
// Then we can iterate through the selected tables and generate the files from the selected templates.
// NOTE: Not currently possible in KryptonTreeView. Have raised an issue here: https://github.com/Krypton-Suite/Standard-Toolkit/issues/468

public partial class NavigatorControl : UserControl
{
    private readonly ContextMenuStrip columnMenu;
    private readonly ContextMenuStrip databaseMenu;
    private readonly ContextMenuStrip defaultMenu;
    private readonly ContextMenuStrip tableMenu;
    private TreeNode rootNode;

    public NavigatorControl()
    {
        InitializeComponent();
        ContextMenuStrip = defaultMenu;
        InitializeTree();

        defaultMenu = CreateDefaultMenu();
        databaseMenu = CreateDatabaseMenu();
        tableMenu = CreateTableMenu();
        columnMenu = CreateColumnMenu();
    }

    private enum NavigatorIcon
    {
        ServerOff,
        ServerOn,
        DatabaseOff,
        DatabaseOn,
        Table,
        Column
    }

    [Browsable(true), Category("Navigator")]
    public string ConnectionString
    {
        get => Server.ConnectionString;
        set => Server.ConnectionString = value;
    }

    [Browsable(true), Category("Navigator")]
    public DataSource ProviderType
    {
        get => Server.ProviderType;
        set => Server.ProviderType = value;
    }

    public void Connect()
    {
        try
        {
            rootNode.Nodes.Clear();
            rootNode.Text = ConnectionString;
            var server = new Server();
            foreach (var database in server.Databases)
            {
                rootNode.Nodes.Add(new TreeNode(database.Name)
                {
                    Tag = database,
                    ImageIndex = (int)NavigatorIcon.DatabaseOff,
                    SelectedImageIndex = (int)NavigatorIcon.DatabaseOff
                });
            }
            rootNode.SelectedImageIndex = (int)NavigatorIcon.ServerOn;
            rootNode.ImageIndex = (int)NavigatorIcon.ServerOn;
        }
        catch
        {
            ShowConnectionForm();
        }
    }

    public void Disconnect()
    {
        rootNode.Nodes.Clear();
        rootNode.Text = "Server";
        rootNode.SelectedImageIndex = (int)NavigatorIcon.ServerOff;
        rootNode.ImageIndex = (int)NavigatorIcon.ServerOff;
    }

    public void ShowConnectionForm()
    {
        using var editForm = new ConnectionForm();
        if (editForm.ShowDialog(this) == DialogResult.OK)
        {
            Connect();
        }
    }

    protected override bool ProcessCmdKey(ref Message msg, Keys keyData) => base.ProcessCmdKey(ref msg, keyData);

    // Column Activate
    private ContextMenuStrip CreateColumnMenu()
    {
        var contextMenu = new ContextMenuStrip();

        var mnuProperties = new ToolStripMenuItem("Properties");
        mnuProperties.Click += mnuProperties_Click;
        contextMenu.Items.Add(mnuProperties);

        return contextMenu;
    }

    // Database Activate
    private ContextMenuStrip CreateDatabaseMenu()
    {
        var contextMenu = new ContextMenuStrip();
        var openMenuItem = new ToolStripMenuItem("Open");
        openMenuItem.Click += openMenuItem_Click;
        contextMenu.Items.Add(openMenuItem);

        return contextMenu;
    }

    // Server Activate
    private ContextMenuStrip CreateDefaultMenu()
    {
        var contextMenu = new ContextMenuStrip();

        var mnuConnect = new ToolStripMenuItem("Connect");
        mnuConnect.Click += mnuConnect_Click;
        contextMenu.Items.Add(mnuConnect);

        var mnuDisconnect = new ToolStripMenuItem("Disconnect");
        mnuDisconnect.Click += mnuDisconnect_Click;
        contextMenu.Items.Add(mnuDisconnect);

        var mnuEditConnection = new ToolStripMenuItem("Edit Connection");
        mnuEditConnection.Click += mnuEditConnection_Click;
        contextMenu.Items.Add(mnuEditConnection);

        return contextMenu;
    }

    // Table Activate
    private ContextMenuStrip CreateTableMenu()
    {
        var contextMenu = new ContextMenuStrip();
        var mnuOpen = new ToolStripMenuItem("Open");
        mnuOpen.Click += mnuOpen_Click;
        contextMenu.Items.Add(mnuOpen);

        return contextMenu;
    }

    private void InitializeTree()
    {
        rootNode = new TreeNode("Server")
        {
            Tag = new Server(),
            ImageIndex = (int)NavigatorIcon.ServerOff,
            SelectedImageIndex = (int)NavigatorIcon.ServerOff
        };
        treeView.Nodes.Add(rootNode);
    }

    [System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE1006:Naming Styles", Justification = "Acceptable for WinForms event handlers")]
    private void mnuConnect_Click(object sender, EventArgs e) => Connect();

    [System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE1006:Naming Styles", Justification = "Acceptable for WinForms event handlers")]
    private void mnuDisconnect_Click(object sender, EventArgs e) => Disconnect();

    [System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE1006:Naming Styles", Justification = "Acceptable for WinForms event handlers")]
    private void mnuEditConnection_Click(object sender, EventArgs e) => ShowConnectionForm();

    [System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE1006:Naming Styles", Justification = "Acceptable for WinForms event handlers")]
    private void mnuOpen_Click(object sender, EventArgs e) => OpenSelectedTable();

    [System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE1006:Naming Styles", Justification = "Acceptable for WinForms event handlers")]
    private void mnuProperties_Click(object sender, EventArgs e)
    {
        var column = (Column)treeView.SelectedNode.Tag;
        OnColumnShowProperties(new ColumnEventArgs(column));
    }

    [System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE1006:Naming Styles", Justification = "Acceptable for WinForms event handlers")]
    private void openMenuItem_Click(object sender, EventArgs e) => OpenSelectedDatabase();

    private void OpenSelectedDatabase()
    {
        var databaseNode = treeView.SelectedNode;
        databaseNode.Nodes.Clear();

        /* Changed by Ferhat */
        // Fill tree with tables
        foreach (var table in ((Database)databaseNode.Tag).Tables)
        {
            databaseNode.Nodes.Add(new TreeNode($"{table.Schema}.{table.Name}")
            {
                Tag = table,
                ImageIndex = (int)NavigatorIcon.Table,
                SelectedImageIndex = (int)NavigatorIcon.Table
            });
        }

        // Fill tree with views
        databaseNode = treeView.SelectedNode;
        foreach (var view in ((Database)databaseNode.Tag).Views)
        {
            databaseNode.Nodes.Add(new TreeNode($"[View] {view.Schema}.{view.Name}")
            {
                Tag = view,
                ImageIndex = (int)NavigatorIcon.Table,
                SelectedImageIndex = (int)NavigatorIcon.Table
            });
        }

        databaseNode.ImageIndex = (int)NavigatorIcon.DatabaseOn;
        databaseNode.SelectedImageIndex = (int)NavigatorIcon.DatabaseOn;
    }

    private void OpenSelectedItem()
    {
        object obj = treeView.SelectedNode.Tag;
        if (obj.GetType() == typeof(Database))
        {
            OpenSelectedDatabase();
        }
        else if (obj.GetType() == typeof(Table))
        {
            OpenSelectedTable();
        }
    }

    private void OpenSelectedTable()
    {
        var tableNode = treeView.SelectedNode;
        tableNode.Nodes.Clear();
        foreach (var column in ((Table)tableNode.Tag).Columns)
        {
            tableNode.Nodes.Add(new TreeNode(column.Name)
            {
                Tag = column,
                ImageIndex = (int)NavigatorIcon.Column,
                SelectedImageIndex = (int)NavigatorIcon.Column
            });
        }
    }

    [System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE1006:Naming Styles", Justification = "Acceptable for WinForms event handlers")]
    private void treeView_AfterSelect(object sender, TreeViewEventArgs e)
    {
        object obj = e.Node.Tag;
        if (obj == null) return;
        if (obj.GetType() == typeof(Server))
        {
            ContextMenuStrip = defaultMenu;
        }
        else if (obj.GetType() == typeof(Database))
        {
            ContextMenuStrip = databaseMenu;
            OnDatabaseSelect(new DatabaseEventArgs((Database)treeView.SelectedNode.Tag));
        }
        else if (obj.GetType() == typeof(Table))
        {
            ContextMenuStrip = tableMenu;
            OnTableSelect(new TableEventArgs((Table)treeView.SelectedNode.Tag));
        }
        else if (obj.GetType() == typeof(Column))
        {
            ContextMenuStrip = columnMenu;
            OnColumnSelect(new ColumnEventArgs((Column)treeView.SelectedNode.Tag));
        }
    }

    [System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE1006:Naming Styles", Justification = "Acceptable for WinForms event handlers")]
    private void treeView_DoubleClick(object sender, EventArgs e) => OpenSelectedItem();

    [System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE1006:Naming Styles", Justification = "Acceptable for WinForms event handlers")]
    private void treeView_KeyUp(object sender, KeyEventArgs e)
    {
        if (e.KeyCode == Keys.Enter)
        {
            OpenSelectedItem();
        }
    }

    #region Events & Delegates

    public delegate void ColumnEventHandler(object sender, ColumnEventArgs args);

    public delegate void DatabaseEventHandler(object sender, DatabaseEventArgs args);

    public delegate void TableEventHandler(object sender, TableEventArgs args);

    [Browsable(true), Category("Navigator")]
    public event ColumnEventHandler ColumnSelect;

    [Browsable(true), Category("Navigator")]
    public event ColumnEventHandler ColumnShowProperties;

    [Browsable(true), Category("Navigator")]
    public event DatabaseEventHandler DatabaseSelect;

    [Browsable(true), Category("Navigator")]
    public event TableEventHandler TableSelect;

    protected virtual void OnColumnSelect(ColumnEventArgs args) => ColumnSelect?.Invoke(this, args);

    protected virtual void OnColumnShowProperties(ColumnEventArgs args) => ColumnShowProperties?.Invoke(this, args);

    protected virtual void OnDatabaseSelect(DatabaseEventArgs args) => DatabaseSelect?.Invoke(this, args);

    protected virtual void OnTableSelect(TableEventArgs args) => TableSelect?.Invoke(this, args);

    #endregion Events & Delegates
}