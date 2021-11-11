namespace CodeGenerator.UI
{
    partial class MainApp
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
            if (disposing && (components != null))
            {
                components.Dispose();
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainApp));
            this.menuStrip = new System.Windows.Forms.MenuStrip();
            this.mnuFile = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuFileDatabaseConnect = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuFileDatabaseDisconnect = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuFileEditConfiguration = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.mnuFileNewTemplate = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuFileOpenTemplate = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuFileSaveTemplate = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuFileSaveAsTemplate = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuFileSaveAsResult = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuFileTemplateExit = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuView = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuViewTemplate = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuViewResults = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuGenerate = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuGenerateFiles = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuHelp = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuHelpDocumentation = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuHelpAbout = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuHelpAboutVientoDigital = new System.Windows.Forms.ToolStripMenuItem();
            this.kryptonManager = new Krypton.Toolkit.KryptonManager(this.components);
            this.kryptonDockingManager = new Krypton.Docking.KryptonDockingManager();
            this.kryptonDockableWorkspace = new Krypton.Docking.KryptonDockableWorkspace();
            this.kryptonPanel = new Krypton.Toolkit.KryptonPanel();
            this.dlgSaveFile = new System.Windows.Forms.SaveFileDialog();
            this.dlgOpenFile = new System.Windows.Forms.OpenFileDialog();
            this.menuStrip.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonDockableWorkspace)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPanel)).BeginInit();
            this.kryptonPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip
            // 
            this.menuStrip.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.menuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuFile,
            this.mnuView,
            this.mnuGenerate,
            this.mnuGenerateFiles,
            this.mnuHelp});
            this.menuStrip.Location = new System.Drawing.Point(0, 0);
            this.menuStrip.Name = "menuStrip";
            this.menuStrip.Size = new System.Drawing.Size(1046, 24);
            this.menuStrip.TabIndex = 3;
            this.menuStrip.Text = "menuStrip";
            // 
            // mnuFile
            // 
            this.mnuFile.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuFileDatabaseConnect,
            this.mnuFileDatabaseDisconnect,
            this.mnuFileEditConfiguration,
            this.toolStripSeparator1,
            this.mnuFileNewTemplate,
            this.mnuFileOpenTemplate,
            this.mnuFileSaveTemplate,
            this.mnuFileSaveAsTemplate,
            this.mnuFileSaveAsResult,
            this.mnuFileTemplateExit});
            this.mnuFile.Name = "mnuFile";
            this.mnuFile.Size = new System.Drawing.Size(37, 20);
            this.mnuFile.Text = "&File";
            // 
            // mnuFileDatabaseConnect
            // 
            this.mnuFileDatabaseConnect.Name = "mnuFileDatabaseConnect";
            this.mnuFileDatabaseConnect.Size = new System.Drawing.Size(184, 22);
            this.mnuFileDatabaseConnect.Text = "Database &Connect";
            this.mnuFileDatabaseConnect.Click += new System.EventHandler(this.mnuFileDatabaseConnect_Click);
            // 
            // mnuFileDatabaseDisconnect
            // 
            this.mnuFileDatabaseDisconnect.Name = "mnuFileDatabaseDisconnect";
            this.mnuFileDatabaseDisconnect.Size = new System.Drawing.Size(184, 22);
            this.mnuFileDatabaseDisconnect.Text = "Database &Disconnect";
            this.mnuFileDatabaseDisconnect.Click += new System.EventHandler(this.mnuFileDatabaseDisconnect_Click);
            // 
            // mnuFileEditConfiguration
            // 
            this.mnuFileEditConfiguration.Name = "mnuFileEditConfiguration";
            this.mnuFileEditConfiguration.Size = new System.Drawing.Size(184, 22);
            this.mnuFileEditConfiguration.Text = "Edit Configuration";
            this.mnuFileEditConfiguration.Click += new System.EventHandler(this.mnuFileEditConfiguration_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(181, 6);
            // 
            // mnuFileNewTemplate
            // 
            this.mnuFileNewTemplate.Name = "mnuFileNewTemplate";
            this.mnuFileNewTemplate.Size = new System.Drawing.Size(184, 22);
            this.mnuFileNewTemplate.Text = "New Template";
            this.mnuFileNewTemplate.Click += new System.EventHandler(this.mnuFileNewTemplate_Click);
            // 
            // mnuFileOpenTemplate
            // 
            this.mnuFileOpenTemplate.Name = "mnuFileOpenTemplate";
            this.mnuFileOpenTemplate.Size = new System.Drawing.Size(184, 22);
            this.mnuFileOpenTemplate.Text = "&Open Template";
            this.mnuFileOpenTemplate.Click += new System.EventHandler(this.mnuFileOpenTemplate_Click);
            // 
            // mnuFileSaveTemplate
            // 
            this.mnuFileSaveTemplate.Name = "mnuFileSaveTemplate";
            this.mnuFileSaveTemplate.Size = new System.Drawing.Size(184, 22);
            this.mnuFileSaveTemplate.Text = "&Save Template";
            this.mnuFileSaveTemplate.Click += new System.EventHandler(this.mnuFileSaveTemplate_Click);
            // 
            // mnuFileSaveAsTemplate
            // 
            this.mnuFileSaveAsTemplate.AccessibleDescription = "";
            this.mnuFileSaveAsTemplate.Name = "mnuFileSaveAsTemplate";
            this.mnuFileSaveAsTemplate.Size = new System.Drawing.Size(184, 22);
            this.mnuFileSaveAsTemplate.Text = "Save &As Template";
            this.mnuFileSaveAsTemplate.Click += new System.EventHandler(this.mnuFileSaveAsTemplate_Click);
            // 
            // mnuFileSaveAsResult
            // 
            this.mnuFileSaveAsResult.AccessibleDescription = "";
            this.mnuFileSaveAsResult.Name = "mnuFileSaveAsResult";
            this.mnuFileSaveAsResult.Size = new System.Drawing.Size(184, 22);
            this.mnuFileSaveAsResult.Text = "Save As Result";
            this.mnuFileSaveAsResult.Click += new System.EventHandler(this.mnuFileSaveAsResult_Click);
            // 
            // mnuFileTemplateExit
            // 
            this.mnuFileTemplateExit.Name = "mnuFileTemplateExit";
            this.mnuFileTemplateExit.Size = new System.Drawing.Size(184, 22);
            this.mnuFileTemplateExit.Text = "E&xit";
            this.mnuFileTemplateExit.Click += new System.EventHandler(this.mnuFileTemplateExit_Click);
            // 
            // mnuView
            // 
            this.mnuView.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuViewTemplate,
            this.mnuViewResults});
            this.mnuView.Name = "mnuView";
            this.mnuView.Size = new System.Drawing.Size(44, 20);
            this.mnuView.Text = "&View";
            // 
            // mnuViewTemplate
            // 
            this.mnuViewTemplate.Name = "mnuViewTemplate";
            this.mnuViewTemplate.Size = new System.Drawing.Size(122, 22);
            this.mnuViewTemplate.Text = "Template";
            this.mnuViewTemplate.Click += new System.EventHandler(this.mnuViewTemplate_Click);
            // 
            // mnuViewResults
            // 
            this.mnuViewResults.Name = "mnuViewResults";
            this.mnuViewResults.Size = new System.Drawing.Size(122, 22);
            this.mnuViewResults.Text = "Results";
            this.mnuViewResults.Click += new System.EventHandler(this.mnuViewResults_Click);
            // 
            // mnuGenerate
            // 
            this.mnuGenerate.Name = "mnuGenerate";
            this.mnuGenerate.Size = new System.Drawing.Size(66, 20);
            this.mnuGenerate.Text = "&Generate";
            this.mnuGenerate.Click += new System.EventHandler(this.mnuGenerate_Click);
            // 
            // mnuGenerateFiles
            // 
            this.mnuGenerateFiles.Name = "mnuGenerateFiles";
            this.mnuGenerateFiles.Size = new System.Drawing.Size(92, 20);
            this.mnuGenerateFiles.Text = "Generate Files";
            this.mnuGenerateFiles.Click += new System.EventHandler(this.mnuGenerateFiles_Click);
            // 
            // mnuHelp
            // 
            this.mnuHelp.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuHelpDocumentation,
            this.mnuHelpAbout,
            this.mnuHelpAboutVientoDigital});
            this.mnuHelp.Name = "mnuHelp";
            this.mnuHelp.Size = new System.Drawing.Size(44, 20);
            this.mnuHelp.Text = "&Help";
            // 
            // mnuHelpDocumentation
            // 
            this.mnuHelpDocumentation.Name = "mnuHelpDocumentation";
            this.mnuHelpDocumentation.Size = new System.Drawing.Size(193, 22);
            this.mnuHelpDocumentation.Text = "Documentation";
            this.mnuHelpDocumentation.Click += new System.EventHandler(this.mnuHelpDocumentation_Click);
            // 
            // mnuHelpAbout
            // 
            this.mnuHelpAbout.Image = ((System.Drawing.Image)(resources.GetObject("mnuHelpAbout.Image")));
            this.mnuHelpAbout.Name = "mnuHelpAbout";
            this.mnuHelpAbout.Size = new System.Drawing.Size(193, 22);
            this.mnuHelpAbout.Text = "About Code Generator";
            this.mnuHelpAbout.Click += new System.EventHandler(this.mnuHelpAbout_Click);
            // 
            // mnuHelpAboutVientoDigital
            // 
            this.mnuHelpAboutVientoDigital.BackColor = System.Drawing.SystemColors.Control;
            this.mnuHelpAboutVientoDigital.Image = ((System.Drawing.Image)(resources.GetObject("mnuHelpAboutVientoDigital.Image")));
            this.mnuHelpAboutVientoDigital.Name = "mnuHelpAboutVientoDigital";
            this.mnuHelpAboutVientoDigital.Size = new System.Drawing.Size(193, 22);
            this.mnuHelpAboutVientoDigital.Text = "About Viento Digital";
            this.mnuHelpAboutVientoDigital.Click += new System.EventHandler(this.mnuHelpAboutVientoDigital_Click);
            // 
            // kryptonManager
            // 
            this.kryptonManager.GlobalPaletteMode = Krypton.Toolkit.PaletteModeManager.Office2010Black;
            // 
            // kryptonDockableWorkspace
            // 
            this.kryptonDockableWorkspace.AutoHiddenHost = false;
            this.kryptonDockableWorkspace.CompactFlags = ((Krypton.Workspace.CompactFlags)(((Krypton.Workspace.CompactFlags.RemoveEmptyCells | Krypton.Workspace.CompactFlags.RemoveEmptySequences) 
            | Krypton.Workspace.CompactFlags.PromoteLeafs)));
            this.kryptonDockableWorkspace.Dock = System.Windows.Forms.DockStyle.Fill;
            this.kryptonDockableWorkspace.Location = new System.Drawing.Point(0, 0);
            this.kryptonDockableWorkspace.Name = "kryptonDockableWorkspace";
            // 
            // 
            // 
            this.kryptonDockableWorkspace.Root.UniqueName = "F867F895E47B4CA5C6AC47791205A4AB";
            this.kryptonDockableWorkspace.Root.WorkspaceControl = this.kryptonDockableWorkspace;
            this.kryptonDockableWorkspace.ShowMaximizeButton = false;
            this.kryptonDockableWorkspace.Size = new System.Drawing.Size(1046, 647);
            this.kryptonDockableWorkspace.TabIndex = 5;
            this.kryptonDockableWorkspace.TabStop = true;
            // 
            // kryptonPanel
            // 
            this.kryptonPanel.Controls.Add(this.kryptonDockableWorkspace);
            this.kryptonPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.kryptonPanel.Location = new System.Drawing.Point(0, 24);
            this.kryptonPanel.Name = "kryptonPanel";
            this.kryptonPanel.Size = new System.Drawing.Size(1046, 647);
            this.kryptonPanel.TabIndex = 6;
            // 
            // dlgSaveFile
            // 
            this.dlgSaveFile.RestoreDirectory = true;
            // 
            // dlgOpenFile
            // 
            this.dlgOpenFile.Filter = "All files (*.*)|*.*";
            this.dlgOpenFile.RestoreDirectory = true;
            // 
            // MainApp
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1046, 671);
            this.Controls.Add(this.kryptonPanel);
            this.Controls.Add(this.menuStrip);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.IsMdiContainer = true;
            this.Name = "MainApp";
            this.Text = "iCodegenerator - Viento Digital";
            this.menuStrip.ResumeLayout(false);
            this.menuStrip.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonDockableWorkspace)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPanel)).EndInit();
            this.kryptonPanel.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.MenuStrip menuStrip;
        private System.Windows.Forms.ToolStripMenuItem mnuFile;
        private System.Windows.Forms.ToolStripMenuItem mnuView;
        private System.Windows.Forms.ToolStripMenuItem mnuGenerate;
        private System.Windows.Forms.ToolStripMenuItem mnuHelp;
        private System.Windows.Forms.ToolStripMenuItem mnuFileDatabaseConnect;
        private System.Windows.Forms.ToolStripMenuItem mnuFileDatabaseDisconnect;
        private System.Windows.Forms.ToolStripMenuItem mnuFileEditConfiguration;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem mnuFileOpenTemplate;
        private System.Windows.Forms.ToolStripMenuItem mnuFileSaveTemplate;
        private System.Windows.Forms.ToolStripMenuItem mnuFileSaveAsTemplate;
        private System.Windows.Forms.ToolStripMenuItem mnuFileSaveAsResult;
        private System.Windows.Forms.ToolStripMenuItem mnuFileTemplateExit;
        private System.Windows.Forms.ToolStripMenuItem mnuGenerateFiles;
        private System.Windows.Forms.ToolStripMenuItem mnuHelpDocumentation;
        private System.Windows.Forms.ToolStripMenuItem mnuHelpAbout;
        private System.Windows.Forms.ToolStripMenuItem mnuHelpAboutVientoDigital;
        private System.Windows.Forms.ToolStripMenuItem mnuViewTemplate;
        private System.Windows.Forms.ToolStripMenuItem mnuViewResults;
        private System.Windows.Forms.ToolStripMenuItem mnuFileNewTemplate;
        private Krypton.Toolkit.KryptonManager kryptonManager;
        private Krypton.Docking.KryptonDockingManager kryptonDockingManager;
        private Krypton.Docking.KryptonDockableWorkspace kryptonDockableWorkspace;
        private Krypton.Toolkit.KryptonPanel kryptonPanel;
        private System.Windows.Forms.SaveFileDialog dlgSaveFile;
        private System.Windows.Forms.OpenFileDialog dlgOpenFile;
    }
}