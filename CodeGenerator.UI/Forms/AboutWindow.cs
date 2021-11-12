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
    public class AboutWindow : KryptonForm
    {
        private Label lblYear;
        private LinkLabel lnkUrl;
        private Label lblHeading;
        private Label lblAuthor;
        private LinkLabel lnkAuthor;
        private LinkLabel lnkContributor;
        private Label lblContributor;

        /// <summary>
        /// Required designer variable.
        /// </summary>
        private readonly Container components = null;

        public AboutWindow()
        {
            InitializeComponent();
            lblHeading.Text += $" {AppVersion.Version}";
            lblYear.Text = $"2005 - {DateTime.Now.Year}";
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AboutWindow));
            this.lblHeading = new System.Windows.Forms.Label();
            this.lblYear = new System.Windows.Forms.Label();
            this.lnkUrl = new System.Windows.Forms.LinkLabel();
            this.lblAuthor = new System.Windows.Forms.Label();
            this.lnkAuthor = new System.Windows.Forms.LinkLabel();
            this.lnkContributor = new System.Windows.Forms.LinkLabel();
            this.lblContributor = new System.Windows.Forms.Label();
            this.SuspendLayout();
            //
            // lblHeading
            //
            this.lblHeading.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblHeading.Location = new System.Drawing.Point(12, 8);
            this.lblHeading.Name = "lblHeading";
            this.lblHeading.Size = new System.Drawing.Size(268, 23);
            this.lblHeading.TabIndex = 0;
            this.lblHeading.Text = "Code Generator";
            this.lblHeading.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            //
            // lblYear
            //
            this.lblYear.Location = new System.Drawing.Point(15, 31);
            this.lblYear.Name = "lblYear";
            this.lblYear.Size = new System.Drawing.Size(265, 23);
            this.lblYear.TabIndex = 2;
            this.lblYear.Text = "2005 - ";
            this.lblYear.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            //
            // lnkUrl
            //
            this.lnkUrl.Location = new System.Drawing.Point(15, 54);
            this.lnkUrl.Name = "lnkUrl";
            this.lnkUrl.Size = new System.Drawing.Size(265, 23);
            this.lnkUrl.TabIndex = 3;
            this.lnkUrl.TabStop = true;
            this.lnkUrl.Text = "http://www.icodegenerator.net";
            this.lnkUrl.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lnkUrl.VisitedLinkColor = System.Drawing.Color.Red;
            this.lnkUrl.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnkUrl_LinkClicked);
            //
            // lblAuthor
            //
            this.lblAuthor.Location = new System.Drawing.Point(15, 90);
            this.lblAuthor.Name = "lblAuthor";
            this.lblAuthor.Size = new System.Drawing.Size(265, 23);
            this.lblAuthor.TabIndex = 1;
            this.lblAuthor.Text = "Original Author: Victor Y. Dominguez";
            this.lblAuthor.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            //
            // lnkAuthor
            //
            this.lnkAuthor.Location = new System.Drawing.Point(15, 113);
            this.lnkAuthor.Name = "lnkAuthor";
            this.lnkAuthor.Size = new System.Drawing.Size(265, 23);
            this.lnkAuthor.TabIndex = 4;
            this.lnkAuthor.TabStop = true;
            this.lnkAuthor.Text = "http://www.vientodigital.com/";
            this.lnkAuthor.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lnkAuthor.VisitedLinkColor = System.Drawing.Color.Red;
            //
            // lnkContributor
            //
            this.lnkContributor.Location = new System.Drawing.Point(15, 159);
            this.lnkContributor.Name = "lnkContributor";
            this.lnkContributor.Size = new System.Drawing.Size(265, 23);
            this.lnkContributor.TabIndex = 6;
            this.lnkContributor.TabStop = true;
            this.lnkContributor.Text = "https://github.com/gordon-matt";
            this.lnkContributor.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lnkContributor.VisitedLinkColor = System.Drawing.Color.Red;
            //
            // lblContributor
            //
            this.lblContributor.Location = new System.Drawing.Point(15, 136);
            this.lblContributor.Name = "lblContributor";
            this.lblContributor.Size = new System.Drawing.Size(265, 23);
            this.lblContributor.TabIndex = 5;
            this.lblContributor.Text = "Contributor: Matt Gordon";
            this.lblContributor.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            //
            // AboutWindow
            //
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.BackColor = System.Drawing.SystemColors.Window;
            this.ClientSize = new System.Drawing.Size(292, 191);
            this.Controls.Add(this.lnkContributor);
            this.Controls.Add(this.lblContributor);
            this.Controls.Add(this.lnkAuthor);
            this.Controls.Add(this.lnkUrl);
            this.Controls.Add(this.lblYear);
            this.Controls.Add(this.lblAuthor);
            this.Controls.Add(this.lblHeading);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "AboutWindow";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "CodeGenerator";
            this.Click += new System.EventHandler(this.AboutWindow_Click);
            this.ResumeLayout(false);
        }

        #endregion Windows Form Designer generated code

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE1006:Naming Styles", Justification = "<Pending>")]
        private void lnkUrl_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
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
            lnkUrl.LinkVisited = true;
            //Call the Process.Start method to open the default browser
            //with a URL:
            Process.Start(lnkUrl.Text);
        }

        private void AboutWindow_Click(object sender, System.EventArgs e)
        {
            this.Hide();
        }
    }
}