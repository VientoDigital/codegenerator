using System.Diagnostics;
using System.Runtime.InteropServices;

namespace CodeGenerator.UI;

/// <summary>
/// Summary description for AboutWindow.
/// </summary>
public class AboutWindow : KryptonForm
{
    private Label lblAuthor;
    private Label lblContributor;
    private Label lblHeading;
    private Label lblYear;
    private LinkLabel lnkAuthor;
    private LinkLabel lnkContributor;
    private LinkLabel lnkUrl;

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
        var resources = new System.ComponentModel.ComponentResourceManager(typeof(AboutWindow));
        lblHeading = new Label();
        lblYear = new Label();
        lnkUrl = new LinkLabel();
        lblAuthor = new Label();
        lnkAuthor = new LinkLabel();
        lnkContributor = new LinkLabel();
        lblContributor = new Label();
        SuspendLayout();
        //
        // lblHeading
        //
        lblHeading.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
        lblHeading.Location = new System.Drawing.Point(14, 10);
        lblHeading.Name = "lblHeading";
        lblHeading.Size = new System.Drawing.Size(322, 28);
        lblHeading.TabIndex = 0;
        lblHeading.Text = "Code Generator";
        lblHeading.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
        //
        // lblYear
        //
        lblYear.Location = new System.Drawing.Point(18, 38);
        lblYear.Name = "lblYear";
        lblYear.Size = new System.Drawing.Size(318, 28);
        lblYear.TabIndex = 2;
        lblYear.Text = "2005 - ";
        lblYear.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
        //
        // lnkUrl
        //
        lnkUrl.Location = new System.Drawing.Point(18, 66);
        lnkUrl.Name = "lnkUrl";
        lnkUrl.Size = new System.Drawing.Size(318, 29);
        lnkUrl.TabIndex = 3;
        lnkUrl.TabStop = true;
        lnkUrl.Text = "http://www.icodegenerator.net";
        lnkUrl.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
        lnkUrl.VisitedLinkColor = System.Drawing.Color.Red;
        lnkUrl.LinkClicked += lnkUrl_LinkClicked;
        //
        // lblAuthor
        //
        lblAuthor.Location = new System.Drawing.Point(18, 111);
        lblAuthor.Name = "lblAuthor";
        lblAuthor.Size = new System.Drawing.Size(318, 28);
        lblAuthor.TabIndex = 1;
        lblAuthor.Text = "Original Author: Victor Y. Dominguez";
        lblAuthor.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
        //
        // lnkAuthor
        //
        lnkAuthor.Location = new System.Drawing.Point(18, 139);
        lnkAuthor.Name = "lnkAuthor";
        lnkAuthor.Size = new System.Drawing.Size(318, 28);
        lnkAuthor.TabIndex = 4;
        lnkAuthor.TabStop = true;
        lnkAuthor.Text = "http://www.vientodigital.com/";
        lnkAuthor.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
        lnkAuthor.VisitedLinkColor = System.Drawing.Color.Red;
        lnkAuthor.LinkClicked += lnkAuthor_LinkClicked;
        //
        // lnkContributor
        //
        lnkContributor.Location = new System.Drawing.Point(18, 196);
        lnkContributor.Name = "lnkContributor";
        lnkContributor.Size = new System.Drawing.Size(318, 28);
        lnkContributor.TabIndex = 6;
        lnkContributor.TabStop = true;
        lnkContributor.Text = "https://github.com/gordon-matt";
        lnkContributor.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
        lnkContributor.VisitedLinkColor = System.Drawing.Color.Red;
        lnkContributor.LinkClicked += lnkContributor_LinkClicked;
        //
        // lblContributor
        //
        lblContributor.Location = new System.Drawing.Point(18, 167);
        lblContributor.Name = "lblContributor";
        lblContributor.Size = new System.Drawing.Size(318, 29);
        lblContributor.TabIndex = 5;
        lblContributor.Text = "Contributor: Matt Gordon";
        lblContributor.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
        //
        // AboutWindow
        //
        AutoScaleBaseSize = new System.Drawing.Size(6, 16);
        BackColor = System.Drawing.SystemColors.Window;
        ClientSize = new System.Drawing.Size(364, 241);
        Controls.Add(lnkContributor);
        Controls.Add(lblContributor);
        Controls.Add(lnkAuthor);
        Controls.Add(lnkUrl);
        Controls.Add(lblYear);
        Controls.Add(lblAuthor);
        Controls.Add(lblHeading);
        FormBorderStyle = FormBorderStyle.FixedToolWindow;
        Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
        Name = "AboutWindow";
        StartPosition = FormStartPosition.CenterScreen;
        Text = "CodeGenerator";
        Click += AboutWindow_Click;
        ResumeLayout(false);
    }

    #endregion Windows Form Designer generated code

    private static void VisitLink(LinkLabel linkLabel)
    {
        linkLabel.LinkVisited = true;
        string url = linkLabel.Text;

        try
        {
            using var process = Process.Start(url);
        }
        catch
        {
            // hack because of this: https://github.com/dotnet/corefx/issues/10361
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                url = url.Replace("&", "^&");
                using var process = Process.Start(new ProcessStartInfo("cmd", $"/c start {url}") { CreateNoWindow = true });
            }
            else if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
            {
                using var process = Process.Start("xdg-open", url);
            }
            else if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
            {
                using var process = Process.Start("open", url);
            }
            else
            {
                throw;
            }
        }
    }

    private void AboutWindow_Click(object sender, EventArgs e) => Hide();

    [System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE1006:Naming Styles", Justification = "Acceptable for WinForms event handlers")]
    private void lnkAuthor_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e) => VisitLink(lnkAuthor);

    [System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE1006:Naming Styles", Justification = "Acceptable for WinForms event handlers")]
    private void lnkContributor_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e) => VisitLink(lnkContributor);

    [System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE1006:Naming Styles", Justification = "Acceptable for WinForms event handlers")]
    private void lnkUrl_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e) => VisitLink(lnkUrl);
}