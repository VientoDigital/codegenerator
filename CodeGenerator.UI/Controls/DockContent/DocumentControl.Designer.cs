namespace CodeGenerator.UI
{
    partial class DocumentControl
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
            var resources = new System.ComponentModel.ComponentResourceManager(typeof(DocumentControl));
            fctDocument = new FastColoredTextBoxNS.FastColoredTextBox();
            ((System.ComponentModel.ISupportInitialize)fctDocument).BeginInit();
            SuspendLayout();
            // 
            // fctDocument
            // 
            fctDocument.AutoCompleteBracketsList = new char[] { '(', ')', '{', '}', '[', ']', '"', '"', '\'', '\'' };
            fctDocument.AutoIndentCharsPatterns = "^\\s*[\\w\\.]+(\\s\\w+)?\\s*(?<range>=)\\s*(?<range>[^;=]+);\r\n^\\s*(case|default)\\s*[^:]*(?<range>:)\\s*(?<range>[^;]+);";
            fctDocument.AutoScrollMinSize = new System.Drawing.Size(27, 14);
            fctDocument.BackBrush = null;
            fctDocument.CharHeight = 14;
            fctDocument.CharWidth = 8;
            fctDocument.DefaultMarkerSize = 8;
            fctDocument.DisabledColor = System.Drawing.Color.FromArgb(100, 180, 180, 180);
            fctDocument.Dock = System.Windows.Forms.DockStyle.Fill;
            fctDocument.IsReplaceMode = false;
            fctDocument.Language = FastColoredTextBoxNS.Language.CSharp;
            fctDocument.Location = new System.Drawing.Point(0, 0);
            fctDocument.Name = "fctDocument";
            fctDocument.Paddings = new System.Windows.Forms.Padding(0);
            fctDocument.SelectionColor = System.Drawing.Color.FromArgb(60, 0, 0, 255);
            fctDocument.ServiceColors = (FastColoredTextBoxNS.ServiceColors)resources.GetObject("fctDocument.ServiceColors");
            fctDocument.Size = new System.Drawing.Size(284, 262);
            fctDocument.TabIndex = 0;
            fctDocument.Zoom = 100;
            // 
            // DocumentControl
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            Controls.Add(fctDocument);
            Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            Name = "DocumentControl";
            Size = new System.Drawing.Size(284, 262);
            ((System.ComponentModel.ISupportInitialize)fctDocument).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private FastColoredTextBoxNS.FastColoredTextBox fctDocument;
    }
}