using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;

namespace iCodeGenerator.iCodeGeneratorGui
{
    public partial class ResultForm : DockContent
    {
        public ResultForm()
        {
            InitializeComponent();
        }

        public string ContentText
        {
            get { return rtbResult.Text; }
            set { rtbResult.Text = value; }
        }
    }
}
