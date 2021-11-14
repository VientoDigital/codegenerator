using System;
using System.Windows.Forms;

namespace CodeGenerator.UI
{
    internal class Program
    {
        [STAThread]
        public static void Main()
        {
            Application.ApplicationExit += Application_ApplicationExit;
            Application.Run(new Main());
        }

        private static void Application_ApplicationExit(object sender, EventArgs e)
        {
            ConfigFile.Instance.Save();
        }
    }
}
