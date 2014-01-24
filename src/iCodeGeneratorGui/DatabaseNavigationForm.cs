using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using iCodeGenerator.DatabaseNavigator;
using iCodeGenerator.DatabaseStructure;
using WeifenLuo.WinFormsUI.Docking;

namespace iCodeGenerator.iCodeGeneratorGui
{
    public partial class DatabaseNavigationForm : DockContent
    {
        public DatabaseNavigationForm()
        {
            InitializeComponent();
        }

        public void Connect()
        {
			navigatorControl.Connect();
        }

        public void Disconnect()
        {
			navigatorControl.Disconnect();
        }

        public void ShowEditConnectionString()
        {
			navigatorControl.ShowEditConnectionStringDialog();
        }

		public event NavigatorControl.DatabaseEventHandler DatabaseSelected;
		protected virtual void OnDatabaseSelected(DatabaseEventArgs args)
		{
			if(DatabaseSelected != null)
			{
				DatabaseSelected(this,args);
			}
		}

		public event NavigatorControl.ColumnEventHandler ColumnSelected;
		protected virtual void OnColumnSelected(ColumnEventArgs args)
		{
			if(ColumnSelected != null)
			{
				ColumnSelected(this,args);
			}
		}
	
		public event NavigatorControl.TableEventHandler TableSelected;
		protected virtual void OnTableSelected(TableEventArgs args)
		{
			if(TableSelected != null)
			{
				TableSelected(this,args);
			}
		}

        private void navigatorControl_ColumnSelect(object sender, ColumnEventArgs args)
        {
            OnColumnSelected(args);    
        }

        private void navigatorControl_DatabaseSelect(object sender, DatabaseEventArgs args)
        {
		    OnDatabaseSelected(args);
        }

        private void navigatorControl_TableSelect(object sender, TableEventArgs args)
        {
            OnTableSelected(args);
        }

    }
}
