using System.Windows.Forms;
using CodeGenerator.DatabaseNavigator;

namespace CodeGenerator.UI
{
    public partial class DatabaseNavigationForm : UserControl
    {
        public DatabaseNavigationForm()
        {
            InitializeComponent();
        }

        public event NavigatorControl.ColumnEventHandler ColumnSelected;

        public event NavigatorControl.DatabaseEventHandler DatabaseSelected;

        public event NavigatorControl.TableEventHandler TableSelected;

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

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE1006:Naming Styles", Justification = "<Pending>")]
        private void navigatorControl_ColumnSelect(object sender, ColumnEventArgs args)
        {
            ColumnSelected?.Invoke(this, args);
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE1006:Naming Styles", Justification = "<Pending>")]
        private void navigatorControl_DatabaseSelect(object sender, DatabaseEventArgs args)
        {
            DatabaseSelected?.Invoke(this, args);
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE1006:Naming Styles", Justification = "<Pending>")]
        private void navigatorControl_TableSelect(object sender, TableEventArgs args)
        {
            TableSelected?.Invoke(this, args);
        }
    }
}