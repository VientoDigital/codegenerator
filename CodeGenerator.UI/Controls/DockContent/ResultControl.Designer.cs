namespace CodeGenerator.UI
{
    partial class ResultControl
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
            components = new System.ComponentModel.Container();
            var resources = new System.ComponentModel.ComponentResourceManager(typeof(ResultControl));
            fctResult = new FastColoredTextBoxNS.FastColoredTextBox();
            ((System.ComponentModel.ISupportInitialize)fctResult).BeginInit();
            SuspendLayout();
            // 
            // fctResult
            // 
            fctResult.AutoCompleteBracketsList = new char[] { '(', ')', '{', '}', '[', ']', '"', '"', '\'', '\'' };
            fctResult.AutoIndentCharsPatterns = "^\\s*[\\w\\.]+(\\s\\w+)?\\s*(?<range>=)\\s*(?<range>[^;=]+);\r\n^\\s*(case|default)\\s*[^:]*(?<range>:)\\s*(?<range>[^;]+);";
            fctResult.AutoScrollMinSize = new System.Drawing.Size(27, 14);
            fctResult.BackBrush = null;
            fctResult.CharHeight = 14;
            fctResult.CharWidth = 8;
            fctResult.DefaultMarkerSize = 8;
            fctResult.DisabledColor = System.Drawing.Color.FromArgb(100, 180, 180, 180);
            fctResult.Dock = DockStyle.Fill;
            fctResult.IsReplaceMode = false;
            fctResult.Language = FastColoredTextBoxNS.Language.CSharp;
            fctResult.Location = new System.Drawing.Point(0, 0);
            fctResult.Name = "fctResult";
            fctResult.Paddings = new Padding(0);
            fctResult.SelectionColor = System.Drawing.Color.FromArgb(60, 0, 0, 255);
            fctResult.ServiceColors = (FastColoredTextBoxNS.ServiceColors)resources.GetObject("fctResult.ServiceColors");
            fctResult.Size = new System.Drawing.Size(284, 262);
            fctResult.TabIndex = 0;
            fctResult.Zoom = 100;
            // 
            // ResultControl
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(fctResult);
            Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            Name = "ResultControl";
            Size = new System.Drawing.Size(284, 262);
            ((System.ComponentModel.ISupportInitialize)fctResult).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private FastColoredTextBoxNS.FastColoredTextBox fctResult;
    }
}