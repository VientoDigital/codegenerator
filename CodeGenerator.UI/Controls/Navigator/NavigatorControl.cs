using System;
using System.ComponentModel;
using System.Windows.Forms;
using CodeGenerator.Data;
using CodeGenerator.Data.Structure;
using Krypton.Toolkit;

namespace CodeGenerator.UI
{
    // TODO: Support generating files for multiple tables at once.. we need to set Checkboxes enabled on the treeView and then:
    //  disable for database nodes (only show checkboxes on table nodes) by using this solution:
    //  https://stackoverflow.com/questions/698369/how-to-disable-a-winforms-treeview-node-checkbox
    // Then we can iterate through the selected tables and generate the files from the selected templates.
    // NOTE: Not currently possible in KryptonTreeView. Have raised an issue here: https://github.com/Krypton-Suite/Standard-Toolkit/issues/468

    public class NavigatorControl : UserControl
    {
        private ImageList imageList;

        private TreeNode rootNode;
        private IContainer components;
        private KryptonTreeView treeView;

        public NavigatorControl()
        {
            InitializeComponent();
            InitializeMenu();
            InitializeTree();
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
        public ProviderType ProviderType
        {
            get => Server.ProviderType;
            set => Server.ProviderType = value;
        }

        public void Connect()
        {
            try
            {
                rootNode.Nodes.Clear();
                var server = new Server();
                rootNode.Text = ConnectionString;
                foreach (Database database in server.Databases)
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
            var editForm = new ConnectionForm();
            if (editForm.ShowDialog(this) == DialogResult.OK)
            {
                Connect();
            }
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            return base.ProcessCmdKey(ref msg, keyData);
        }

        private void InitializeMenu()
        {
            SetDefaultMenu();
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

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE1006:Naming Styles", Justification = "<Pending>")]
        private void mnuConnect_Click(object sender, EventArgs e)
        {
            Connect();
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE1006:Naming Styles", Justification = "<Pending>")]
        private void mnuDisconnect_Click(object sender, EventArgs e)
        {
            Disconnect();
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE1006:Naming Styles", Justification = "<Pending>")]
        private void mnuEditConnection_Click(object sender, EventArgs e)
        {
            ShowConnectionForm();
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE1006:Naming Styles", Justification = "<Pending>")]
        private void mnuOpen_Click(object sender, EventArgs e)
        {
            OpenSelectedTable();
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE1006:Naming Styles", Justification = "<Pending>")]
        private void mnuProperties_Click(object sender, EventArgs e)
        {
            var column = (Column)treeView.SelectedNode.Tag;
            OnColumnShowProperties(new ColumnEventArgs(column));
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE1006:Naming Styles", Justification = "<Pending>")]
        private void mnuRemove_Click(object sender, EventArgs e)
        {
            var node = treeView.SelectedNode;
            var column = node.Tag as Column;
            column.ParentTable.Columns.Remove(column);
            node.Remove();
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE1006:Naming Styles", Justification = "<Pending>")]
        private void openMenuItem_Click(object sender, EventArgs e)
        {
            OpenSelectedDatabase();
        }

        private void OpenSelectedDatabase()
        {
            var databaseNode = treeView.SelectedNode;
            databaseNode.Nodes.Clear();

            /* Changed by Ferhat */
            // Fill tree with tables
            foreach (Table table in ((Database)databaseNode.Tag).Tables)
            {
                databaseNode.Nodes.Add(new TreeNode(table.Name)
                {
                    Tag = table,
                    ImageIndex = (int)NavigatorIcon.Table,
                    SelectedImageIndex = (int)NavigatorIcon.Table
                });
            }

            // Fill tree with views
            databaseNode = treeView.SelectedNode;
            foreach (Table table in ((Database)databaseNode.Tag).Views)
            {
                databaseNode.Nodes.Add(new TreeNode(table.Name)
                {
                    Tag = table,
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
            foreach (Column column in ((Table)tableNode.Tag).Columns)
            {
                tableNode.Nodes.Add(new TreeNode(column.Name)
                {
                    Tag = column,
                    ImageIndex = (int)NavigatorIcon.Column,
                    SelectedImageIndex = (int)NavigatorIcon.Column
                });
            }
        }

        // Column Activate
        private void SetColumnMenu()
        {
            var contextMenu = new ContextMenu();

            var mnuProperties = new MenuItem("Properties");
            mnuProperties.Click += mnuProperties_Click;
            contextMenu.MenuItems.Add(mnuProperties);

            var mnuRemove = new MenuItem("Remove");
            mnuRemove.Click += mnuRemove_Click;
            contextMenu.MenuItems.Add(mnuRemove);

            ContextMenu = contextMenu;
        }

        // Database Activate
        private void SetDatabaseMenu()
        {
            var contextMenu = new ContextMenu();
            var openMenuItem = new MenuItem("Open");
            openMenuItem.Click += openMenuItem_Click;
            contextMenu.MenuItems.Add(openMenuItem);
            ContextMenu = contextMenu;
        }

        // Server Activate
        private void SetDefaultMenu()
        {
            var contextMenu = new ContextMenu();

            var mnuConnect = new MenuItem("Connect");
            mnuConnect.Click += mnuConnect_Click;
            contextMenu.MenuItems.Add(mnuConnect);

            var mnuDisconnect = new MenuItem("Disconnect");
            mnuDisconnect.Click += mnuDisconnect_Click;
            contextMenu.MenuItems.Add(mnuDisconnect);

            var mnuEditConnection = new MenuItem("Edit Connection");
            mnuEditConnection.Click += mnuEditConnection_Click;
            contextMenu.MenuItems.Add(mnuEditConnection);

            ContextMenu = contextMenu;
        }

        // Table Activate
        private void SetTableMenu()
        {
            var contextMenu = new ContextMenu();
            var mnuOpen = new MenuItem("Open");
            mnuOpen.Click += mnuOpen_Click;
            contextMenu.MenuItems.Add(mnuOpen);
            ContextMenu = contextMenu;
        }

        #region Component Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(NavigatorControl));
            this.treeView = new Krypton.Toolkit.KryptonTreeView();
            this.imageList = new System.Windows.Forms.ImageList(this.components);
            this.SuspendLayout();
            // 
            // treeView
            // 
            this.treeView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeView.ImageIndex = 0;
            this.treeView.ImageList = this.imageList;
            this.treeView.Location = new System.Drawing.Point(0, 0);
            this.treeView.Name = "treeView";
            this.treeView.SelectedImageIndex = 0;
            this.treeView.Size = new System.Drawing.Size(150, 150);
            this.treeView.TabIndex = 0;
            this.treeView.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.treeView_AfterSelect);
            this.treeView.Controls[0].DoubleClick += new System.EventHandler(this.treeView_DoubleClick);
            this.treeView.KeyUp += new System.Windows.Forms.KeyEventHandler(this.treeView_KeyUp);
            // 
            // imageList
            // 
            this.imageList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList.ImageStream")));
            this.imageList.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList.Images.SetKeyName(0, "");
            this.imageList.Images.SetKeyName(1, "");
            this.imageList.Images.SetKeyName(2, "");
            this.imageList.Images.SetKeyName(3, "");
            this.imageList.Images.SetKeyName(4, "");
            this.imageList.Images.SetKeyName(5, "");
            // 
            // NavigatorControl
            // 
            this.Controls.Add(this.treeView);
            this.Name = "NavigatorControl";
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.treeView_KeyUp);
            this.ResumeLayout(false);

        }

        #endregion Component Designer generated code

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE1006:Naming Styles", Justification = "<Pending>")]
        private void treeView_AfterSelect(object sender, TreeViewEventArgs e)
        {
            object obj = e.Node.Tag;
            if (obj == null) return;
            if (obj.GetType() == typeof(Server))
            {
                SetDefaultMenu();
            }
            else if (obj.GetType() == typeof(Database))
            {
                SetDatabaseMenu();
                OnDatabaseSelect(new DatabaseEventArgs((Database)treeView.SelectedNode.Tag));
            }
            else if (obj.GetType() == typeof(Table))
            {
                SetTableMenu();
                OnTableSelect(new TableEventArgs((Table)treeView.SelectedNode.Tag));
            }
            else if (obj.GetType() == typeof(Column))
            {
                SetColumnMenu();
                OnColumnSelect(new ColumnEventArgs((Column)treeView.SelectedNode.Tag));
            }
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE1006:Naming Styles", Justification = "<Pending>")]
        private void treeView_DoubleClick(object sender, EventArgs e)
        {
            OpenSelectedItem();
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE1006:Naming Styles", Justification = "<Pending>")]
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

        protected virtual void OnColumnSelect(ColumnEventArgs args)
        {
            ColumnSelect?.Invoke(this, args);
        }

        protected virtual void OnColumnShowProperties(ColumnEventArgs args)
        {
            ColumnShowProperties?.Invoke(this, args);
        }

        protected virtual void OnDatabaseSelect(DatabaseEventArgs args)
        {
            DatabaseSelect?.Invoke(this, args);
        }

        protected virtual void OnTableSelect(TableEventArgs args)
        {
            TableSelect?.Invoke(this, args);
        }

        #endregion Events & Delegates
    }
}