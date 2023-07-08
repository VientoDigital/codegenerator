using System.Diagnostics;

namespace CodeGenerator.UI;

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

    public AboutWindow()
    {
        InitializeComponent();
        lblHeading.Text += $" {AppVersion.Version}";
        lblYear.Text = $"2005 - {DateTime.Now.Year}";
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
        this.lblHeading.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
        this.lblHeading.Location = new System.Drawing.Point(14, 10);
        this.lblHeading.Name = "lblHeading";
        this.lblHeading.Size = new System.Drawing.Size(322, 28);
        this.lblHeading.TabIndex = 0;
        this.lblHeading.Text = "Code Generator";
        this.lblHeading.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
        //
        // lblYear
        //
        this.lblYear.Location = new System.Drawing.Point(18, 38);
        this.lblYear.Name = "lblYear";
        this.lblYear.Size = new System.Drawing.Size(318, 28);
        this.lblYear.TabIndex = 2;
        this.lblYear.Text = "2005 - ";
        this.lblYear.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
        //
        // lnkUrl
        //
        this.lnkUrl.Location = new System.Drawing.Point(18, 66);
        this.lnkUrl.Name = "lnkUrl";
        this.lnkUrl.Size = new System.Drawing.Size(318, 29);
        this.lnkUrl.TabIndex = 3;
        this.lnkUrl.TabStop = true;
        this.lnkUrl.Text = "http://www.icodegenerator.net";
        this.lnkUrl.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
        this.lnkUrl.VisitedLinkColor = System.Drawing.Color.Red;
        this.lnkUrl.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnkUrl_LinkClicked);
        //
        // lblAuthor
        //
        this.lblAuthor.Location = new System.Drawing.Point(18, 111);
        this.lblAuthor.Name = "lblAuthor";
        this.lblAuthor.Size = new System.Drawing.Size(318, 28);
        this.lblAuthor.TabIndex = 1;
        this.lblAuthor.Text = "Original Author: Victor Y. Dominguez";
        this.lblAuthor.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
        //
        // lnkAuthor
        //
        this.lnkAuthor.Location = new System.Drawing.Point(18, 139);
        this.lnkAuthor.Name = "lnkAuthor";
        this.lnkAuthor.Size = new System.Drawing.Size(318, 28);
        this.lnkAuthor.TabIndex = 4;
        this.lnkAuthor.TabStop = true;
        this.lnkAuthor.Text = "http://www.vientodigital.com/";
        this.lnkAuthor.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
        this.lnkAuthor.VisitedLinkColor = System.Drawing.Color.Red;
        //
        // lnkContributor
        //
        this.lnkContributor.Location = new System.Drawing.Point(18, 196);
        this.lnkContributor.Name = "lnkContributor";
        this.lnkContributor.Size = new System.Drawing.Size(318, 28);
        this.lnkContributor.TabIndex = 6;
        this.lnkContributor.TabStop = true;
        this.lnkContributor.Text = "https://github.com/gordon-matt";
        this.lnkContributor.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
        this.lnkContributor.VisitedLinkColor = System.Drawing.Color.Red;
        //
        // lblContributor
        //
        this.lblContributor.Location = new System.Drawing.Point(18, 167);
        this.lblContributor.Name = "lblContributor";
        this.lblContributor.Size = new System.Drawing.Size(318, 29);
        this.lblContributor.TabIndex = 5;
        this.lblContributor.Text = "Contributor: Matt Gordon";
        this.lblContributor.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
        //
        // AboutWindow
        //
        this.AutoScaleBaseSize = new System.Drawing.Size(6, 16);
        this.BackColor = System.Drawing.SystemColors.Window;
        this.ClientSize = new System.Drawing.Size(364, 241);
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

    [System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE1006:Naming Styles", Justification = "Acceptable for WinForms event handlers")]
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
        _ = Process.Start(lnkUrl.Text);
    }

    private void AboutWindow_Click(object sender, EventArgs e) => Hide();
}