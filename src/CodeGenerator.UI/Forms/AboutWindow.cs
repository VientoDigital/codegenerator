using System.ComponentModel;
using System.Diagnostics;
using System.Windows.Forms;
using ComponentFactory.Krypton.Toolkit;

namespace CodeGenerator.CodeGenerator.UI
{
    /// <summary>
    /// Summary description for AboutWindow.
    /// </summary>
    public class AboutWindow : KryptonForm
    {
        private Label label2;
        private Label label3;
        private LinkLabel uiCodeGeneratorLink;
        private System.Windows.Forms.Label uiCodeGeneratorLabel;

        /// <summary>
        /// Required designer variable.
        /// </summary>
        private Container components = null;

        public AboutWindow()
        {
            InitializeComponent();
            uiCodeGeneratorLabel.Text = uiCodeGeneratorLabel.Text + "2.0";
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
            System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(AboutWindow));
            this.uiCodeGeneratorLabel = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.uiCodeGeneratorLink = new System.Windows.Forms.LinkLabel();
            this.SuspendLayout();
            //
            // uiCodeGeneratorLabel
            //
            this.uiCodeGeneratorLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
            this.uiCodeGeneratorLabel.Location = new System.Drawing.Point(16, 8);
            this.uiCodeGeneratorLabel.Name = "uiCodeGeneratorLabel";
            this.uiCodeGeneratorLabel.Size = new System.Drawing.Size(248, 23);
            this.uiCodeGeneratorLabel.TabIndex = 0;
            this.uiCodeGeneratorLabel.Text = "CodeGenerator";
            this.uiCodeGeneratorLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            //
            // label2
            //
            this.label2.Location = new System.Drawing.Point(64, 42);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(144, 23);
            this.label2.TabIndex = 1;
            this.label2.Text = "Author: Victor Y Dominguez";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            //
            // label3
            //
            this.label3.Location = new System.Drawing.Point(80, 76);
            this.label3.Name = "label3";
            this.label3.TabIndex = 2;
            this.label3.Text = "2005 - 2006";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            //
            // uiCodeGeneratorLink
            //
            this.uiCodeGeneratorLink.Location = new System.Drawing.Point(32, 110);
            this.uiCodeGeneratorLink.Name = "uiCodeGeneratorLink";
            this.uiCodeGeneratorLink.Size = new System.Drawing.Size(232, 23);
            this.uiCodeGeneratorLink.TabIndex = 3;
            this.uiCodeGeneratorLink.TabStop = true;
            this.uiCodeGeneratorLink.Text = "http://www.icodegenerator.net";
            this.uiCodeGeneratorLink.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.uiCodeGeneratorLink.VisitedLinkColor = System.Drawing.Color.Red;
            this.uiCodeGeneratorLink.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.uiCodeGeneratorLink_LinkClicked);
            //
            // AboutWindow
            //
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.BackColor = System.Drawing.SystemColors.Window;
            this.ClientSize = new System.Drawing.Size(292, 136);
            this.Controls.Add(this.uiCodeGeneratorLink);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.uiCodeGeneratorLabel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "AboutWindow";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "CodeGenerator";
            this.Click += new System.EventHandler(this.AboutWindow_Click);
            this.ResumeLayout(false);
        }

        #endregion Windows Form Designer generated code

        private void uiCodeGeneratorLink_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                VisitLink();
            }
            catch
            {
                MessageBox.Show("Unable to open link that was clicked.");
            }
        }

        private void VisitLink()
        {
            // Change the color of the link text by setting LinkVisited
            // to true.
            uiCodeGeneratorLink.LinkVisited = true;
            //Call the Process.Start method to open the default browser
            //with a URL:
            Process.Start(uiCodeGeneratorLink.Text);
        }

        private void AboutWindow_Click(object sender, System.EventArgs e)
        {
            this.Hide();
        }
    }
}