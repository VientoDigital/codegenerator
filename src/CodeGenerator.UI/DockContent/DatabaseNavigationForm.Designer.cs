using CodeGenerator.DatabaseNavigator;

namespace CodeGenerator.CodeGenerator.UI
{
    partial class DatabaseNavigationForm
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
            this.navigatorControl = new NavigatorControl();
            this.SuspendLayout();
            // 
            // navigatorControl
            // 
            this.navigatorControl.ConnectionString = "";
            this.navigatorControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.navigatorControl.Location = new System.Drawing.Point(0, 0);
            this.navigatorControl.Name = "navigatorControl";
            this.navigatorControl.ProviderType = Data.DataProviderType.SqlClient;
            this.navigatorControl.Size = new System.Drawing.Size(284, 262);
            this.navigatorControl.TabIndex = 0;
            this.navigatorControl.TableSelect += new NavigatorControl.TableEventHandler(this.navigatorControl_TableSelect);
            this.navigatorControl.DatabaseSelect += new NavigatorControl.DatabaseEventHandler(this.navigatorControl_DatabaseSelect);
            this.navigatorControl.ColumnSelect += new NavigatorControl.ColumnEventHandler(this.navigatorControl_ColumnSelect);
            // 
            // DatabaseNavigationForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 262);
            this.Controls.Add(this.navigatorControl);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "DatabaseNavigationForm";
            this.Text = "DatabaseNavigationForm";
            this.ResumeLayout(false);

        }

        #endregion

        private NavigatorControl navigatorControl;
    }
}