using Krypton.Toolkit;

namespace CodeGenerator.UI
{
    partial class CustomValuesControl
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
            this.gridCustomValues = new KryptonDataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.gridCustomValues)).BeginInit();
            this.SuspendLayout();
            // 
            // gridCustomValues
            // 
            this.gridCustomValues.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gridCustomValues.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridCustomValues.Location = new System.Drawing.Point(0, 0);
            this.gridCustomValues.Name = "gridCustomValues";
            this.gridCustomValues.Size = new System.Drawing.Size(284, 262);
            this.gridCustomValues.TabIndex = 0;
            this.gridCustomValues.Leave += new System.EventHandler(this.gridCustomValues_Leave);
            // 
            // CustomValuesForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 262);
            this.Controls.Add(this.gridCustomValues);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "CustomValuesForm";
            this.Text = "CustomValuesForm";
            ((System.ComponentModel.ISupportInitialize)(this.gridCustomValues)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private KryptonDataGridView gridCustomValues;
    }
}