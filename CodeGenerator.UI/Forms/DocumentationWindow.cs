using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows.Forms;
using Krypton.Toolkit;

namespace CodeGenerator.UI
{
    /// <summary>
    /// Summary description for AboutWindow.
    /// </summary>
    public class DocumentationWindow : KryptonForm
    {
        private KryptonRichTextBox rtbDocumentation;

        /// <summary>
        /// Required designer variable.
        /// </summary>
        private readonly Container components = null;

        public DocumentationWindow()
        {
            InitializeComponent();
            rtbDocumentation.LoadFile($"{AppDomain.CurrentDomain.BaseDirectory}Documentation.rtf");
        }

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (components != null)
                {
                    components.Dispose();
                }
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DocumentationWindow));
            this.rtbDocumentation = new Krypton.Toolkit.KryptonRichTextBox();
            this.SuspendLayout();
            // 
            // rtbDocumentation
            // 
            this.rtbDocumentation.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rtbDocumentation.Location = new System.Drawing.Point(0, 0);
            this.rtbDocumentation.Name = "rtbDocumentation";
            this.rtbDocumentation.Size = new System.Drawing.Size(1008, 681);
            this.rtbDocumentation.TabIndex = 0;
            this.rtbDocumentation.Text = "";
            // 
            // DocumentationWindow
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.BackColor = System.Drawing.SystemColors.Window;
            this.ClientSize = new System.Drawing.Size(1008, 681);
            this.Controls.Add(this.rtbDocumentation);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "DocumentationWindow";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Code Generator";
            this.ResumeLayout(false);

        }

        #endregion Windows Form Designer generated code
    }
}