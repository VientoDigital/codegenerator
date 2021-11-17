namespace CodeGenerator.UI
{
    partial class SettingsForm
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
            this.tabs = new System.Windows.Forms.TabControl();
            this.tabDataTypeMapping = new System.Windows.Forms.TabPage();
            this.btnRemove = new Krypton.Toolkit.KryptonButton();
            this.btnAdd = new Krypton.Toolkit.KryptonButton();
            this.lblLanguage = new Krypton.Toolkit.KryptonLabel();
            this.cmbLanguage = new Krypton.Toolkit.KryptonComboBox();
            this.dgvMappings = new Krypton.Toolkit.KryptonDataGridView();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnCancel = new Krypton.Toolkit.KryptonButton();
            this.btnSave = new Krypton.Toolkit.KryptonButton();
            this.tabs.SuspendLayout();
            this.tabDataTypeMapping.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cmbLanguage)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvMappings)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabs
            // 
            this.tabs.Controls.Add(this.tabDataTypeMapping);
            this.tabs.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabs.Location = new System.Drawing.Point(0, 0);
            this.tabs.Name = "tabs";
            this.tabs.SelectedIndex = 0;
            this.tabs.Size = new System.Drawing.Size(937, 490);
            this.tabs.TabIndex = 3;
            // 
            // tabDataTypeMapping
            // 
            this.tabDataTypeMapping.Controls.Add(this.btnRemove);
            this.tabDataTypeMapping.Controls.Add(this.btnAdd);
            this.tabDataTypeMapping.Controls.Add(this.lblLanguage);
            this.tabDataTypeMapping.Controls.Add(this.cmbLanguage);
            this.tabDataTypeMapping.Controls.Add(this.dgvMappings);
            this.tabDataTypeMapping.Location = new System.Drawing.Point(4, 24);
            this.tabDataTypeMapping.Name = "tabDataTypeMapping";
            this.tabDataTypeMapping.Padding = new System.Windows.Forms.Padding(3);
            this.tabDataTypeMapping.Size = new System.Drawing.Size(929, 462);
            this.tabDataTypeMapping.TabIndex = 0;
            this.tabDataTypeMapping.Text = "Data Type Mapping";
            this.tabDataTypeMapping.UseVisualStyleBackColor = true;
            // 
            // btnRemove
            // 
            this.btnRemove.Location = new System.Drawing.Point(501, 6);
            this.btnRemove.Name = "btnRemove";
            this.btnRemove.Size = new System.Drawing.Size(57, 25);
            this.btnRemove.TabIndex = 4;
            this.btnRemove.Values.Text = "Remove";
            this.btnRemove.Click += new System.EventHandler(this.btnRemove_Click);
            // 
            // btnAdd
            // 
            this.btnAdd.Location = new System.Drawing.Point(438, 6);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(57, 25);
            this.btnAdd.TabIndex = 3;
            this.btnAdd.Values.Text = "Add";
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // lblLanguage
            // 
            this.lblLanguage.Location = new System.Drawing.Point(8, 6);
            this.lblLanguage.Name = "lblLanguage";
            this.lblLanguage.Size = new System.Drawing.Size(64, 20);
            this.lblLanguage.TabIndex = 1;
            this.lblLanguage.Values.Text = "Language";
            // 
            // cmbLanguage
            // 
            this.cmbLanguage.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawVariable;
            this.cmbLanguage.DropDownWidth = 354;
            this.cmbLanguage.IntegralHeight = false;
            this.cmbLanguage.Location = new System.Drawing.Point(78, 6);
            this.cmbLanguage.Name = "cmbLanguage";
            this.cmbLanguage.Size = new System.Drawing.Size(354, 21);
            this.cmbLanguage.StateCommon.ComboBox.Content.TextH = Krypton.Toolkit.PaletteRelativeAlign.Near;
            this.cmbLanguage.TabIndex = 2;
            this.cmbLanguage.SelectedIndexChanged += new System.EventHandler(this.cmbLanguage_SelectedIndexChanged);
            // 
            // dgvMappings
            // 
            this.dgvMappings.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvMappings.Location = new System.Drawing.Point(8, 37);
            this.dgvMappings.Name = "dgvMappings";
            this.dgvMappings.RowTemplate.Height = 25;
            this.dgvMappings.Size = new System.Drawing.Size(913, 376);
            this.dgvMappings.TabIndex = 0;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btnCancel);
            this.panel1.Controls.Add(this.btnSave);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 443);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(937, 47);
            this.panel1.TabIndex = 4;
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.Location = new System.Drawing.Point(739, 10);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(90, 25);
            this.btnCancel.TabIndex = 1;
            this.btnCancel.Values.Text = "Cancel";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnSave
            // 
            this.btnSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSave.Location = new System.Drawing.Point(835, 10);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(90, 25);
            this.btnSave.TabIndex = 0;
            this.btnSave.Values.Text = "Save";
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // SettingsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(937, 490);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.tabs);
            this.Name = "SettingsForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Settings";
            this.Load += new System.EventHandler(this.SettingsForm_Load);
            this.tabs.ResumeLayout(false);
            this.tabDataTypeMapping.ResumeLayout(false);
            this.tabDataTypeMapping.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cmbLanguage)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvMappings)).EndInit();
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.TabControl tabs;
        private System.Windows.Forms.TabPage tabDataTypeMapping;
        private Krypton.Toolkit.KryptonButton btnAdd;
        private Krypton.Toolkit.KryptonLabel lblLanguage;
        private Krypton.Toolkit.KryptonComboBox cmbLanguage;
        private Krypton.Toolkit.KryptonDataGridView dgvMappings;
        private System.Windows.Forms.Panel panel1;
        private Krypton.Toolkit.KryptonButton btnCancel;
        private Krypton.Toolkit.KryptonButton btnSave;
        private Krypton.Toolkit.KryptonButton btnRemove;
    }
}