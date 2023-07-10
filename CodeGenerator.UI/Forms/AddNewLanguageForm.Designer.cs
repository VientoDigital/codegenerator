namespace CodeGenerator.UI
{
    partial class AddNewLanguageForm
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
            btnOK = new KryptonButton();
            btnCancel = new KryptonButton();
            txtName = new KryptonTextBox();
            lblName = new KryptonLabel();
            SuspendLayout();
            // 
            // btnOK
            // 
            btnOK.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnOK.CornerRoundingRadius = -1F;
            btnOK.Location = new System.Drawing.Point(492, 41);
            btnOK.Name = "btnOK";
            btnOK.Size = new System.Drawing.Size(108, 36);
            btnOK.TabIndex = 0;
            btnOK.Values.Image = Properties.Resources.OK_32x32;
            btnOK.Values.Text = "OK";
            btnOK.Click += btnOK_Click;
            // 
            // btnCancel
            // 
            btnCancel.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnCancel.CornerRoundingRadius = -1F;
            btnCancel.Location = new System.Drawing.Point(378, 41);
            btnCancel.Name = "btnCancel";
            btnCancel.Size = new System.Drawing.Size(108, 36);
            btnCancel.TabIndex = 1;
            btnCancel.Values.Image = Properties.Resources.Cancel_32x32;
            btnCancel.Values.Text = "Cancel";
            btnCancel.Click += btnCancel_Click;
            // 
            // txtName
            // 
            txtName.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            txtName.Location = new System.Drawing.Point(64, 12);
            txtName.Name = "txtName";
            txtName.Size = new System.Drawing.Size(536, 23);
            txtName.TabIndex = 2;
            // 
            // lblName
            // 
            lblName.Location = new System.Drawing.Point(12, 12);
            lblName.Name = "lblName";
            lblName.Size = new System.Drawing.Size(46, 20);
            lblName.TabIndex = 3;
            lblName.Values.Text = "Name:";
            // 
            // AddNewLanguageForm
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(609, 86);
            Controls.Add(lblName);
            Controls.Add(txtName);
            Controls.Add(btnCancel);
            Controls.Add(btnOK);
            FormBorderStyle = FormBorderStyle.FixedToolWindow;
            Name = "AddNewLanguageForm";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Add New Language";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private KryptonButton btnOK;
        private KryptonButton btnCancel;
        private KryptonTextBox txtName;
        private KryptonLabel lblName;
    }
}