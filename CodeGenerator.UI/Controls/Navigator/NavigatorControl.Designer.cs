namespace CodeGenerator.UI;

public partial class NavigatorControl
{
    /// <summary>
    /// Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    /// <summary>
    /// Clean up any resources being used.
    /// </summary>
    /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
    protected override void Dispose(bool disposing)
    {
        if (disposing)
        {
            defaultMenu?.Dispose();
            databaseMenu?.Dispose();
            tableMenu?.Dispose();
            columnMenu?.Dispose();
            components?.Dispose();
        }
        base.Dispose(disposing);
    }

    #region Component Designer generated code

    /// <summary>
    /// Required method for Designer support - do not modify
    /// the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
        this.components = new System.ComponentModel.Container();
        System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(NavigatorControl));
        this.treeView = new Krypton.Toolkit.KryptonTreeView();
        this.imageList = new System.Windows.Forms.ImageList(this.components);
        this.SuspendLayout();
        //
        // treeView
        //
        this.treeView.Dock = System.Windows.Forms.DockStyle.Fill;
        this.treeView.ImageIndex = 0;
        this.treeView.ImageList = this.imageList;
        this.treeView.Location = new System.Drawing.Point(0, 0);
        this.treeView.Name = "treeView";
        this.treeView.SelectedImageIndex = 0;
        this.treeView.Size = new System.Drawing.Size(150, 150);
        this.treeView.TabIndex = 0;
        this.treeView.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.treeView_AfterSelect);
        this.treeView.Controls[0].DoubleClick += new System.EventHandler(this.treeView_DoubleClick);
        this.treeView.KeyUp += new System.Windows.Forms.KeyEventHandler(this.treeView_KeyUp);
        //
        // imageList
        //
        this.imageList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList.ImageStream")));
        this.imageList.TransparentColor = System.Drawing.Color.Transparent;
        this.imageList.Images.SetKeyName(0, "");
        this.imageList.Images.SetKeyName(1, "");
        this.imageList.Images.SetKeyName(2, "");
        this.imageList.Images.SetKeyName(3, "");
        this.imageList.Images.SetKeyName(4, "");
        this.imageList.Images.SetKeyName(5, "");
        //
        // NavigatorControl
        //
        this.Controls.Add(this.treeView);
        this.Name = "NavigatorControl";
        this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.treeView_KeyUp);
        this.ResumeLayout(false);
    }

    #endregion Component Designer generated code

    private ImageList imageList;
    private KryptonTreeView treeView;
}