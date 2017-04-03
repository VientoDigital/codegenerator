using System;
using System.ComponentModel;
using System.Windows.Forms;
using ComponentFactory.Krypton.Toolkit;
using iCodeGenerator.DatabaseStructure;
using iCodeGenerator.GenericDataAccess;

namespace iCodeGenerator.DatabaseNavigator
{
    public class NavigatorControl : UserControl
    {
        private enum NavigatorIcon : int
        {
            ServerOff,
            ServerOn,
            DatabaseOff,
            DatabaseOn,
            Table,
            Column
        }

        #region Attributes

        private KryptonTreeView uiNavigatorTreeView;
        private IContainer components;

        /*
		private ShortcutListener _shortcuts = null;
		private MenuBar _menuBar = null;
		private ContextMenuBarItem _contextMenu = null;
        */
        private ImageList uiNavigatorImageList;

        private TreeNode _rootNode;

        #endregion Attributes

        [Browsable(true), Category("Navigator")]
        public string ConnectionString
        {
            set { Server.ConnectionString = value; }
            get { return Server.ConnectionString; }
        }

        [Browsable(true), Category("Navigator")]
        public DataProviderType ProviderType
        {
            set { Server.ProviderType = value; }
            get { return Server.ProviderType; }
        }

        public NavigatorControl()
        {
            InitializeComponent();
            InitializeMenu();
            InitializeTree();
        }

        private void InitializeTree()
        {
            _rootNode = new TreeNode("Server");
            _rootNode.Tag = new Server();
            _rootNode.ImageIndex = (int)NavigatorIcon.ServerOff;
            _rootNode.SelectedImageIndex = (int)NavigatorIcon.ServerOff;
            uiNavigatorTreeView.Nodes.Add(_rootNode);
        }

        private void InitializeMenu()
        {
            SetDefaultMenu();
        }

        // Server Activate
        private void SetDefaultMenu()
        {
            var contextMenu = new ContextMenu();
            var miConnect = new MenuItem("Connect");
            miConnect.Click += connect_Activate;
            var miDisconnect = new MenuItem("Disconnect");
            miDisconnect.Click += disconnect_Activate;
            var miEditConnection = new MenuItem("Edit Connection");
            miEditConnection.Click += serverEdit_Activate;
            contextMenu.MenuItems.Add(miConnect);
            contextMenu.MenuItems.Add(miDisconnect);
            contextMenu.MenuItems.Add(miEditConnection);
            ContextMenu = contextMenu;
            /*
			if(_contextMenu == null)
			{
				_contextMenu = new ContextMenuBarItem();
			}
			_contextMenu.MenuItems.Clear();
			MenuButtonItem connect = new MenuButtonItem("Connect");
			connect.Activate += new EventHandler(connect_Activate);
			MenuButtonItem disconnect = new MenuButtonItem("Disconnect");
			disconnect.Activate += new EventHandler(disconnect_Activate);
			MenuButtonItem edit = new MenuButtonItem("Edit");
			edit.Activate += new EventHandler(serverEdit_Activate);
			_contextMenu.MenuItems.AddRange(new MenuButtonItem[] { connect, edit, disconnect });
            */
            //contextMenu.MenuItems[1].Shortcut = Shortcut.CtrlJ;
        }

        private void connect_Activate(object sender, EventArgs e)
        {
            Connect();
        }

        public void Connect()
        {
            try
            {
                _rootNode.Nodes.Clear();
                Server server = new Server();
                _rootNode.Text = ConnectionString;
                foreach (Database database in server.Databases)
                {
                    TreeNode databaseNode = new TreeNode(database.Name);
                    databaseNode.Tag = database;
                    databaseNode.ImageIndex = (int)NavigatorIcon.DatabaseOff;
                    databaseNode.SelectedImageIndex = (int)NavigatorIcon.DatabaseOff;
                    _rootNode.Nodes.Add(databaseNode);
                }
                _rootNode.SelectedImageIndex = (int)NavigatorIcon.ServerOn;
                _rootNode.ImageIndex = (int)NavigatorIcon.ServerOn;
            }
            catch (Exception)
            {
                ShowEditConnectionStringDialog();
            }
        }

        private void disconnect_Activate(object sender, EventArgs e)
        {
            Disconnect();
        }

        public void Disconnect()
        {
            _rootNode.Nodes.Clear();
            _rootNode.Text = "Server";
            _rootNode.SelectedImageIndex = (int)NavigatorIcon.ServerOff;
            _rootNode.ImageIndex = (int)NavigatorIcon.ServerOff;
        }

        private void serverEdit_Activate(object sender, EventArgs e)
        {
            ShowEditConnectionStringDialog();
        }

        public void ShowEditConnectionStringDialog()
        {
            ServerSettingsForm editForm = new ServerSettingsForm();
            if (editForm.ShowDialog(this) == DialogResult.OK)
            {
                Connect();
            }
        }

        // Database Activate
        private void SetDatabaseMenu()
        {
            var contextMenu = new ContextMenu();
            var miOpen = new MenuItem("Open");
            miOpen.Click += databaseOpen_Activate;
            contextMenu.MenuItems.Add(miOpen);
            ContextMenu = contextMenu;
        }

        private void databaseOpen_Activate(object sender, EventArgs e)
        {
            OpenSelectedDatabase();
        }

        private void OpenSelectedDatabase()
        {
            TreeNode databaseNode = uiNavigatorTreeView.SelectedNode;
            databaseNode.Nodes.Clear();

            /* Changed by Ferhat */
            // Fill tree with tables
            foreach (Table table in ((Database)databaseNode.Tag).Tables)
            {
                TreeNode tableNode = new TreeNode(table.Name);
                tableNode.Tag = table;
                tableNode.ImageIndex = (int)NavigatorIcon.Table;
                tableNode.SelectedImageIndex = (int)NavigatorIcon.Table;
                databaseNode.Nodes.Add(tableNode);
            }

            // Fill tree with views
            databaseNode = uiNavigatorTreeView.SelectedNode;
            foreach (Table table in ((Database)databaseNode.Tag).Views)
            {
                TreeNode tableNode = new TreeNode(table.Name);
                tableNode.Tag = table;
                tableNode.ImageIndex = (int)NavigatorIcon.Table;
                tableNode.SelectedImageIndex = (int)NavigatorIcon.Table;
                databaseNode.Nodes.Add(tableNode);
            }

            databaseNode.ImageIndex = (int)NavigatorIcon.DatabaseOn;
            databaseNode.SelectedImageIndex = (int)NavigatorIcon.DatabaseOn;
        }

        // Table Activate
        private void SetTableMenu()
        {
            var contextMenu = new ContextMenu();
            var miOpen = new MenuItem("Open");
            miOpen.Click += tableOpen_Activate;
            contextMenu.MenuItems.Add(miOpen);
            ContextMenu = contextMenu;
        }

        private void tableOpen_Activate(object sender, EventArgs e)
        {
            OpenSelectedTable();
        }

        private void OpenSelectedTable()
        {
            TreeNode tableNode = uiNavigatorTreeView.SelectedNode;
            tableNode.Nodes.Clear();
            foreach (Column column in ((Table)tableNode.Tag).Columns)
            {
                TreeNode columnNode = new TreeNode(column.Name);
                columnNode.Tag = column;
                columnNode.ImageIndex = (int)NavigatorIcon.Column;
                columnNode.SelectedImageIndex = (int)NavigatorIcon.Column;
                tableNode.Nodes.Add(columnNode);
            }
        }

        // Column Activate
        private void SetColumnMenu()
        {
            var contextMenu = new ContextMenu();
            var miProperties = new MenuItem("Properties");
            miProperties.Click += columnShowProperties;
            contextMenu.MenuItems.Add(miProperties);
            var miRemove = new MenuItem("Remove");
            miRemove.Click += columnRemove_Activate;
            contextMenu.MenuItems.Add(miRemove);
            ContextMenu = contextMenu;
        }

        private void columnShowProperties(object sender, EventArgs e)
        {
            Column column = (Column)uiNavigatorTreeView.SelectedNode.Tag;
            OnColumnShowProperties(new ColumnEventArgs(column));
        }

        private void columnRemove_Activate(object sender, EventArgs e)
        {
            TreeNode node = uiNavigatorTreeView.SelectedNode;
            Column column = node.Tag as Column;
            column.ParentTable.Columns.Remove(column);
            node.Remove();
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            /*
			if(_shortcuts.ShortcutActivated(keyData))
				return true;
             */

            return base.ProcessCmdKey(ref msg, keyData);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (components != null)
                {
                    components.Dispose();
                }
                /*
				_shortcuts.Dispose();
				_menuBar.SetSandBarMenu(this,null);
				_menuBar.Dispose();
                */
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(NavigatorControl));
            this.uiNavigatorTreeView = new KryptonTreeView();
            this.uiNavigatorImageList = new System.Windows.Forms.ImageList(this.components);
            this.SuspendLayout();
            //
            // uiNavigatorTreeView
            //
            this.uiNavigatorTreeView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.uiNavigatorTreeView.ImageList = this.uiNavigatorImageList;
            this.uiNavigatorTreeView.Location = new System.Drawing.Point(0, 0);
            this.uiNavigatorTreeView.Name = "uiNavigatorTreeView";
            this.uiNavigatorTreeView.Size = new System.Drawing.Size(150, 150);
            this.uiNavigatorTreeView.TabIndex = 0;
            this.uiNavigatorTreeView.KeyUp += new System.Windows.Forms.KeyEventHandler(this.uiNavigatorTreeView_KeyUp);
            this.uiNavigatorTreeView.DoubleClick += new System.EventHandler(this.uiNavigatorTreeView_DoubleClick);
            this.uiNavigatorTreeView.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.uiNavigatorTreeView_AfterSelect);
            //
            // uiNavigatorImageList
            //
            this.uiNavigatorImageList.ImageSize = new System.Drawing.Size(16, 16);
            this.uiNavigatorImageList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("uiNavigatorImageList.ImageStream")));
            this.uiNavigatorImageList.TransparentColor = System.Drawing.Color.Transparent;
            //
            // NavigatorControl
            //
            this.Controls.Add(this.uiNavigatorTreeView);
            this.Name = "NavigatorControl";
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.uiNavigatorTreeView_KeyUp);
            this.ResumeLayout(false);
        }

        #endregion Component Designer generated code

        private void uiNavigatorTreeView_AfterSelect(object sender, TreeViewEventArgs e)
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
                OnDatabaseSelect(new DatabaseEventArgs((Database)uiNavigatorTreeView.SelectedNode.Tag));
            }
            else if (obj.GetType() == typeof(Table))
            {
                SetTableMenu();
                OnTableSelect(new TableEventArgs((Table)uiNavigatorTreeView.SelectedNode.Tag));
            }
            else if (obj.GetType() == typeof(Column))
            {
                SetColumnMenu();
                OnColumnSelect(new ColumnEventArgs((Column)uiNavigatorTreeView.SelectedNode.Tag));
            }
        }

        private void uiNavigatorTreeView_DoubleClick(object sender, EventArgs e)
        {
            OpenSelectedItem();
        }

        private void uiNavigatorTreeView_KeyUp(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                OpenSelectedItem();
            }
        }

        private void OpenSelectedItem()
        {
            object obj = uiNavigatorTreeView.SelectedNode.Tag;
            if (obj.GetType() == typeof(Database))
            {
                OpenSelectedDatabase();
            }
            else if (obj.GetType() == typeof(Table))
            {
                OpenSelectedTable();
            }
        }

        #region Events & Delegates

        // Column Events & Delegates

        public delegate void ColumnEventHandler(object sender, ColumnEventArgs args);

        [Browsable(true), Category("Navigator")]
        public event ColumnEventHandler ColumnSelect;

        protected virtual void OnColumnSelect(ColumnEventArgs args)
        {
            if (ColumnSelect != null)
            {
                ColumnSelect(this, args);
            }
        }

        [Browsable(true), Category("Navigator")]
        public event ColumnEventHandler ColumnShowProperties;

        protected virtual void OnColumnShowProperties(ColumnEventArgs args)
        {
            if (ColumnShowProperties != null)
            {
                ColumnShowProperties(this, args);
            }
        }

        // Table Events & Delegates

        public delegate void TableEventHandler(object sender, TableEventArgs args);

        [Browsable(true), Category("Navigator")]
        public event TableEventHandler TableSelect;

        protected virtual void OnTableSelect(TableEventArgs args)
        {
            if (TableSelect != null)
            {
                TableSelect(this, args);
            }
        }

        // Database Events & Delegates

        public delegate void DatabaseEventHandler(object sender, DatabaseEventArgs args);

        [Browsable(true), Category("Navigator")]
        public event DatabaseEventHandler DatabaseSelect;

        protected virtual void OnDatabaseSelect(DatabaseEventArgs args)
        {
            if (DatabaseSelect != null)
            {
                DatabaseSelect(this, args);
            }
        }

        #endregion Events & Delegates
    }
}