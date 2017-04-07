using System.Windows.Forms;
using iCodeGenerator.DatabaseNavigator;

namespace iCodeGenerator.iCodeGeneratorGui
{
    public partial class DatabaseNavigationForm : UserControl
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
            DatabaseSelected?.Invoke(this, args);
        }

        public event NavigatorControl.ColumnEventHandler ColumnSelected;

        protected virtual void OnColumnSelected(ColumnEventArgs args)
        {
            ColumnSelected?.Invoke(this, args);
        }

        public event NavigatorControl.TableEventHandler TableSelected;

        protected virtual void OnTableSelected(TableEventArgs args)
        {
            TableSelected?.Invoke(this, args);
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