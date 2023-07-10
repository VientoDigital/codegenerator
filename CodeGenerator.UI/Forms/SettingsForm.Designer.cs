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
            tabs = new TabControl();
            tabDataTypeMapping = new TabPage();
            btnRemove = new KryptonButton();
            btnAdd = new KryptonButton();
            lblLanguage = new KryptonLabel();
            cmbLanguage = new KryptonComboBox();
            dgvMappings = new KryptonDataGridView();
            panel1 = new Panel();
            btnCancel = new KryptonButton();
            btnSave = new KryptonButton();
            tabs.SuspendLayout();
            tabDataTypeMapping.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)cmbLanguage).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dgvMappings).BeginInit();
            panel1.SuspendLayout();
            SuspendLayout();
            // 
            // tabs
            // 
            tabs.Controls.Add(tabDataTypeMapping);
            tabs.Dock = DockStyle.Fill;
            tabs.Location = new System.Drawing.Point(0, 0);
            tabs.Name = "tabs";
            tabs.SelectedIndex = 0;
            tabs.Size = new System.Drawing.Size(937, 547);
            tabs.TabIndex = 3;
            // 
            // tabDataTypeMapping
            // 
            tabDataTypeMapping.Controls.Add(btnRemove);
            tabDataTypeMapping.Controls.Add(btnAdd);
            tabDataTypeMapping.Controls.Add(lblLanguage);
            tabDataTypeMapping.Controls.Add(cmbLanguage);
            tabDataTypeMapping.Controls.Add(dgvMappings);
            tabDataTypeMapping.Location = new System.Drawing.Point(4, 24);
            tabDataTypeMapping.Name = "tabDataTypeMapping";
            tabDataTypeMapping.Padding = new Padding(3);
            tabDataTypeMapping.Size = new System.Drawing.Size(929, 519);
            tabDataTypeMapping.TabIndex = 0;
            tabDataTypeMapping.Text = "Data Type Mapping";
            tabDataTypeMapping.UseVisualStyleBackColor = true;
            // 
            // btnRemove
            // 
            btnRemove.CornerRoundingRadius = -1F;
            btnRemove.Location = new System.Drawing.Point(532, 8);
            btnRemove.Name = "btnRemove";
            btnRemove.Size = new System.Drawing.Size(88, 40);
            btnRemove.TabIndex = 4;
            btnRemove.Values.Image = Properties.Resources.Remove_32x32;
            btnRemove.Values.Text = "Remove";
            btnRemove.Click += btnRemove_Click;
            // 
            // btnAdd
            // 
            btnAdd.CornerRoundingRadius = -1F;
            btnAdd.Location = new System.Drawing.Point(438, 8);
            btnAdd.Name = "btnAdd";
            btnAdd.Size = new System.Drawing.Size(88, 40);
            btnAdd.TabIndex = 3;
            btnAdd.Values.Image = Properties.Resources.Add_32x32;
            btnAdd.Values.Text = "Add";
            btnAdd.Click += btnAdd_Click;
            // 
            // lblLanguage
            // 
            lblLanguage.Location = new System.Drawing.Point(8, 16);
            lblLanguage.Name = "lblLanguage";
            lblLanguage.Size = new System.Drawing.Size(64, 20);
            lblLanguage.TabIndex = 1;
            lblLanguage.Values.Text = "Language";
            // 
            // cmbLanguage
            // 
            cmbLanguage.CornerRoundingRadius = -1F;
            cmbLanguage.DropDownWidth = 354;
            cmbLanguage.IntegralHeight = false;
            cmbLanguage.Location = new System.Drawing.Point(78, 16);
            cmbLanguage.Name = "cmbLanguage";
            cmbLanguage.Size = new System.Drawing.Size(354, 21);
            cmbLanguage.StateCommon.ComboBox.Content.TextH = PaletteRelativeAlign.Near;
            cmbLanguage.TabIndex = 2;
            cmbLanguage.SelectedIndexChanged += cmbLanguage_SelectedIndexChanged;
            // 
            // dgvMappings
            // 
            dgvMappings.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            dgvMappings.Location = new System.Drawing.Point(8, 54);
            dgvMappings.Name = "dgvMappings";
            dgvMappings.RowTemplate.Height = 25;
            dgvMappings.Size = new System.Drawing.Size(913, 398);
            dgvMappings.TabIndex = 0;
            // 
            // panel1
            // 
            panel1.Controls.Add(btnCancel);
            panel1.Controls.Add(btnSave);
            panel1.Dock = DockStyle.Bottom;
            panel1.Location = new System.Drawing.Point(0, 482);
            panel1.Name = "panel1";
            panel1.Size = new System.Drawing.Size(937, 65);
            panel1.TabIndex = 4;
            // 
            // btnCancel
            // 
            btnCancel.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            btnCancel.CornerRoundingRadius = -1F;
            btnCancel.Location = new System.Drawing.Point(679, 11);
            btnCancel.Name = "btnCancel";
            btnCancel.Size = new System.Drawing.Size(120, 42);
            btnCancel.TabIndex = 1;
            btnCancel.Values.Image = Properties.Resources.Cancel_32x32;
            btnCancel.Values.Text = "Cancel";
            btnCancel.Click += btnCancel_Click;
            // 
            // btnSave
            // 
            btnSave.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            btnSave.CornerRoundingRadius = -1F;
            btnSave.Location = new System.Drawing.Point(805, 11);
            btnSave.Name = "btnSave";
            btnSave.Size = new System.Drawing.Size(120, 42);
            btnSave.TabIndex = 0;
            btnSave.Values.Image = Properties.Resources.Save_32x32;
            btnSave.Values.Text = "Save";
            btnSave.Click += btnSave_Click;
            // 
            // SettingsForm
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(937, 547);
            Controls.Add(panel1);
            Controls.Add(tabs);
            FormBorderStyle = FormBorderStyle.FixedToolWindow;
            Name = "SettingsForm";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Settings";
            Load += SettingsForm_Load;
            tabs.ResumeLayout(false);
            tabDataTypeMapping.ResumeLayout(false);
            tabDataTypeMapping.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)cmbLanguage).EndInit();
            ((System.ComponentModel.ISupportInitialize)dgvMappings).EndInit();
            panel1.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion
        private TabControl tabs;
        private TabPage tabDataTypeMapping;
        private KryptonButton btnAdd;
        private KryptonLabel lblLanguage;
        private KryptonComboBox cmbLanguage;
        private KryptonDataGridView dgvMappings;
        private Panel panel1;
        private KryptonButton btnCancel;
        private KryptonButton btnSave;
        private KryptonButton btnRemove;
    }
}