using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;
using iCodeGenerator.DatabaseNavigator;
using iCodeGenerator.DatabaseStructure;
using iCodeGenerator.DataTypeConverter;
using iCodeGenerator.Generator;
using iCodeGenerator.Updater;
using WeifenLuo.WinFormsUI.Docking;

namespace iCodeGenerator.iCodeGeneratorGui
{
    public partial class MainApp :Form
    {
        public MainApp()
        {
            InitializeComponent();
            InitializeControls();
            CheckForUpdates();
        }

        private void CheckForUpdates()
        {
            if (UpdateChecker.IsNewUpdate)
            {
                aboutICodegeneratorToolStripMenuItem.BackColor = Color.LightCoral;
                aboutICodegeneratorToolStripMenuItem.ForeColor = Color.White;
            }
                
        }

        #region Forms
        DatabaseNavigationForm _dnf;
        PropertiesForm _pf;
        CustomValuesForm _cvf;
        SnippetsForm _sf;
        DocumentForm _df;
        ResultForm _rf;
        #endregion

        private void InitializeControls()
        {
            _dnf = new DatabaseNavigationForm();
            _dnf.Text = "Database Navigation";
            _dnf.Show(dockPanel, DockState.DockLeft);
            _dnf.Icon = Icon.ExtractAssociatedIcon(@"idb.ico");
            _dnf.TableSelected += DnfTableSelected;
            _dnf.DatabaseSelected += DnfDatabaseSelected;
            _dnf.ColumnSelected += DnfColumnSelected;
            _dnf.HideOnClose = true;

            _sf = new SnippetsForm();
            _sf.Text = "Snippets";
            _sf.SnippetSelected += SfSnippetSelected;
            _sf.Show(dockPanel, DockState.DockLeftAutoHide);
            _sf.Icon = Icon.ExtractAssociatedIcon(@"isnippet.ico");
            _sf.HideOnClose = true;

            _df = new DocumentForm();
            _df.Text = "Template";
            _df.Show(dockPanel, DockState.Document);
            _df.Icon = Icon.ExtractAssociatedIcon(@"itemplate.ico");
            _df.HideOnClose = true;

            _rf = new ResultForm();
            _rf.Text = "Results";
            _rf.Show(dockPanel, DockState.Document);
            _rf.Icon = Icon.ExtractAssociatedIcon(@"iresult.ico");
            _rf.HideOnClose = true;

            _pf = new PropertiesForm();
            _pf.Text = "Properties";
            _pf.Show(dockPanel, DockState.DockRight);
            _pf.Icon = Icon.ExtractAssociatedIcon(@"igen.ico");
            _pf.HideOnClose = true;

            _cvf = new CustomValuesForm();
            _cvf.Text = "Custom Values";
            _cvf.Show(dockPanel, DockState.DockRight);
            _cvf.Icon = Icon.ExtractAssociatedIcon(@"icustom.ico");
            _cvf.HideOnClose = true;
        }

        void SfSnippetSelected(object sender, SnippetEventArgs args)
        {
            _df.ContentText = _df.ContentText.Insert(_df.SelectionStart,new SnippetsHelper().Snippets[args.Snippet].ToString());
        }

		private static Table _selectedTable;
        private void DnfColumnSelected(object sender, ColumnEventArgs args)
        {
			_selectedTable = args.Column.ParentTable;
			_pf.SelectedObject = args.Column;
        }

        private void DnfDatabaseSelected(object sender, DatabaseEventArgs args)
        {
            _pf.SelectedObject = args.Database;
        }

        private void DnfTableSelected(object sender, TableEventArgs args)
        {
			_selectedTable = args.Table;
			_pf.SelectedObject = args.Table;
        }

        private void GenerateCode()
		{
			try
			{
				if (_selectedTable == null) return;
				var cgenerator = new Client {CustomValues = _cvf.CustomValues};
			    _rf.ContentText = cgenerator.Parse(_selectedTable, _df.ContentText);
			}
			catch (DataTypeManagerException ex)
			{
				MessageBox.Show(this, ex.Message, ex.Message, MessageBoxButtons.OK, MessageBoxIcon.Warning);
			}
		}

        [STAThread]
		public static void Main()
		{
			Application.Run(new MainApp());
		}

        private void DatabaseConnectClick(object sender, EventArgs e)
        {
		    _dnf.Connect();	
        }

        private void DatabaseDisconnectClick(object sender, EventArgs e)
        {
            _dnf.Disconnect();
        }

        private void EditConfigDatabaseClick(object sender, EventArgs e)
        {
			_dnf.ShowEditConnectionString();
        }

        private void GenerateCodeClick(object sender, EventArgs e)
        {
            GenerateCode();
        }

        private void GenerateFilesClick(object sender, EventArgs e)
        {
            GenerateFiles();
        }

		DirectorySelectionWindow _selectionWindow;
		private void SelectTemplatesDirectory()
		{
		    if (_selectionWindow == null)
		    {
    		    _selectionWindow = new DirectorySelectionWindow();
                _selectionWindow.InputFolderSelected += SelectionWindowInputFolderSelected;
                _selectionWindow.OutputFolderSelected += SelectionWindowOutputFolderSelected;
		    }
			_selectionWindow.ShowDialog(this);
		}

        static void SelectionWindowOutputFolderSelected(object sender, FolderEventArgs args)
        {
            _OutputTemplateFolder = args.FolderName;
        }

        static void SelectionWindowInputFolderSelected(object sender, FolderEventArgs args)
        {
            _InputTemplateFolder = args.FolderName;
        }

		private static string _InputTemplateFolder = String.Empty;
		private static string _OutputTemplateFolder = String.Empty;

		private void GenerateFiles()
		{
			if (_selectedTable == null) return;
			if (IsValidFolder(_InputTemplateFolder) && IsValidFolder(_OutputTemplateFolder))
			{
				try
				{
					var generator = new FileGenerator();
					generator.OnComplete += FileGeneratorCompleted;
				    generator.CustomValue = _cvf.CustomValues;
					generator.Generate(_selectedTable, _InputTemplateFolder, _OutputTemplateFolder);
				}
				catch (Exception e)
				{
					MessageBox.Show(e.Message);
           
				}
			}
			else
			{
				SelectTemplatesDirectory();
			}
		}

        private void FileGeneratorCompleted(object sender, EventArgs e)
		{
			//MessageBox.Show("File Generation Completed");
            if (IsValidFolder(_OutputTemplateFolder))
			{
				Process.Start(_OutputTemplateFolder);
			}
		}

		private bool IsValidFolder(string folder)
		{
			return folder.Length > 0 && Directory.Exists(folder);
		}

        private void documentationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ProcessStartInfo sInfo = new ProcessStartInfo("http://icodegenerator.net/#.documentation");
            Process.Start(sInfo);
        }

        private void aboutICodegeneratorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var uw = new UpdatesWindow();           
            uw.ShowDialog();
        }

        private void aboutVientoDigitalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ProcessStartInfo sInfo = new ProcessStartInfo("http://www.vientodigital.com/");
            Process.Start(sInfo);
        }

        private void databaseNavigationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if(_dnf.IsHidden)
                _dnf.Show();
            else 
                _dnf.Hide();
        }

        private void snippetsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if(_sf.IsHidden)
                _sf.Show();
            else
                _sf.Hide();
        }

        private void templateToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if(_df.IsHidden)
                _df.Show();
            else
                _df.Hide();
            
        }

        private void resultsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (_rf.IsHidden)
                _rf.Show();
            else
                _rf.Hide();
        }

        private void propertiesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if(_pf.IsHidden)
                _pf.Show();
            else
                _pf.Hide();
        }

        private void customValuesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if(_cvf.IsHidden)
                _cvf.Show();
            else
                _cvf.Hide();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        private static string _TemplateFile;
        private void openTemplateToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var ofd = new OpenFileDialog();
            ofd.Filter = @"All files (*.*)|*.*";
            ofd.RestoreDirectory = true;
            if (ofd.ShowDialog(this) == DialogResult.OK)
            {
                try
                {
                    _TemplateFile = ofd.FileName;
                    Stream s;
                    if ((s = ofd.OpenFile()) != null)
                    {
                        StreamReader sr;
                        using (sr = new StreamReader(s))
                        {
                            var line = sr.ReadToEnd();
                            _df.ContentText = line;
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(@"Error: Could not read file from disk. Original error: " + ex.Message);
                }
            }
        }

        private void saveTemplateToolStripMenuItem_Click(object sender, EventArgs e)
        {

            if (_TemplateFile != null)
                SaveFile(_TemplateFile, _df.ContentText);
            else
                _TemplateFile = SaveAsFile(_TemplateFile, _df.ContentText);
        }

        private void saveAsTemplateToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _TemplateFile = SaveAsFile(null, _df.ContentText);
        }

        private string SaveAsFile(string filename, string contentText)
        {
            Stream stream;
            var sfd = new SaveFileDialog();
            sfd.RestoreDirectory = true;
            if (sfd.ShowDialog() == DialogResult.OK)
            {
                filename = sfd.FileName;
                SaveFile(filename, contentText);
            }
            return filename;
        }

        private string SaveFile(string filename, string contentText)
        {
            using (var writer = new StreamWriter(filename))
            {
                writer.Write(contentText);
            }
            return filename;
        }

        private void saveResultToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveAsFile(null, _rf.ContentText);
        }

        private void newTemplateToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _TemplateFile = null;
            _df.ContentText = string.Empty;
        }
    }
}
