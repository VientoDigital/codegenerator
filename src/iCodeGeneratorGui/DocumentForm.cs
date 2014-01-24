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
    public partial class DocumentForm : DockContent
    {
        public DocumentForm()
        {
            InitializeComponent();
        }

        public int SelectionStart
        {
            get { return rtbDocument.SelectionStart; }
        }

        public string ContentText
        {
            get { return rtbDocument.Text; }
            set { rtbDocument.Text = value; }
        }
    }
}
