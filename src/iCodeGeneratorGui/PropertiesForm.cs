using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using iCodeGenerator.DatabaseStructure;
using WeifenLuo.WinFormsUI.Docking;

namespace iCodeGenerator.iCodeGeneratorGui
{
    public partial class PropertiesForm : DockContent
    {
        public PropertiesForm()
        {
            InitializeComponent();
        }

        public object SelectedObject { 
            get { return (Table) propertyGrid.SelectedObject;  }
            set { propertyGrid.SelectedObject = value; }
        }
    }
}
