using System;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using ComponentFactory.Krypton.Docking;
using ComponentFactory.Krypton.Navigator;
using ComponentFactory.Krypton.Toolkit;
using iCodeGenerator.DatabaseNavigator;
using iCodeGenerator.DatabaseStructure;
using iCodeGenerator.DataTypeConverter;
using iCodeGenerator.Generator;
using iCodeGenerator.Updater;

namespace iCodeGenerator.iCodeGeneratorGui
{
    public partial class MainApp : KryptonForm
    {
        public MainApp()
        {
            InitializeComponent();
            InitializeControls();
            CheckForUpdates();
        }

        private void CheckForUpdates()
        {
            aboutICodegeneratorToolStripMenuItem.Text = aboutICodegeneratorToolStripMenuItem.Text + @" " + UpdateChecker.Version;
            if (UpdateChecker.IsNewUpdate)
            {
                aboutICodegeneratorToolStripMenuItem.BackColor = Color.LightCoral;
                aboutICodegeneratorToolStripMenuItem.ForeColor = Color.White;
                aboutICodegeneratorToolStripMenuItem.Text = @" Download iCodegenerator " + @" (New Version " + UpdateChecker.Software.Version + @")";
            }
        }

        #region Forms

        private DatabaseNavigationForm databaseNavigationForm;
        private PropertiesForm propertiesForm;
        private CustomValuesForm customValuesForm;
        private SnippetsForm snippetsForm;
        private DocumentForm documentForm;
        private ResultForm resultForm;

        private KryptonPage templatePage;
        private KryptonPage resultPage;

        #endregion Forms

        private KryptonPage NewPage(string name, Control content, Image icon = null)
        {
            // Create new page with title and image
            var page = new KryptonPage
            {
                Text = name,
                TextTitle = name,
                TextDescription = name
            };

            if (icon != null)
            {
                page.ImageSmall = icon;
            }

            page.ClearFlags(KryptonPageFlags.DockingAllowClose);

            // Add the control for display inside the page
            content.Dock = DockStyle.Fill;
            page.Controls.Add(content);

            return page;
        }

        private KryptonPage NewDocument(string name, Control content, Image icon = null)
        {
            var page = NewPage(name, content, icon);

            page.ClearFlags(KryptonPageFlags.DockingAllowClose);

            return page;
        }

        private void InitializeControls()
        {
            // Setup docking functionality
            KryptonDockingWorkspace w = kryptonDockingManager.ManageWorkspace(kryptonDockableWorkspace);
            kryptonDockingManager.ManageControl(kryptonPanel, w);
            kryptonDockingManager.ManageFloating(this);

            // Add initial docking pages
            databaseNavigationForm = new DatabaseNavigationForm { Text = "Database Navigation" };
            databaseNavigationForm.TableSelected += DnfTableSelected;
            databaseNavigationForm.DatabaseSelected += DnfDatabaseSelected;
            databaseNavigationForm.ColumnSelected += DnfColumnSelected;

            snippetsForm = new SnippetsForm { Text = "Snippets" };
            snippetsForm.SnippetSelected += SfSnippetSelected;

            documentForm = new DocumentForm { Text = "Template" };
            resultForm = new ResultForm { Text = "Results" };
            propertiesForm = new PropertiesForm { Text = "Properties" };
            customValuesForm = new CustomValuesForm { Text = "Custom Values" };

            templatePage = NewDocument("Template", documentForm, icon: IconToBitMap("itemplate.ico"));
            resultPage = NewDocument("Results", resultForm, icon: IconToBitMap("iresult.ico"));

            kryptonDockingManager.AddToWorkspace("Workspace", new KryptonPage[] { templatePage, resultPage });
            kryptonDockingManager.AddAutoHiddenGroup("Control", DockingEdge.Left, new KryptonPage[]
            {
                NewPage("Snippets", snippetsForm, icon: IconToBitMap("isnippet.ico"))
            });
            kryptonDockingManager.AddDockspace("Control", DockingEdge.Left, new KryptonPage[]
            {
                NewPage("Database Navigation", databaseNavigationForm, icon: IconToBitMap("idb.ico"))
            });
            kryptonDockingManager.AddDockspace("Control", DockingEdge.Right, new KryptonPage[]
            {
                NewPage("Properties", propertiesForm, icon: IconToBitMap("igen.ico")),
                NewPage("Custom Values", customValuesForm, icon: IconToBitMap("icustom.ico"))
            });
        }

        private Bitmap IconToBitMap(string iconName)
        {
            return new Bitmap(Icon.ExtractAssociatedIcon(@"Resources\" + iconName).ToBitmap(), new Size(16, 16));
        }

        private void SfSnippetSelected(object sender, SnippetEventArgs args)
        {
            documentForm.ContentText = documentForm.ContentText.Insert(documentForm.SelectionStart, new SnippetsHelper().Snippets[args.Snippet].ToString());
        }

        private static Table _selectedTable;

        private void DnfColumnSelected(object sender, ColumnEventArgs args)
        {
            _selectedTable = args.Column.ParentTable;
            propertiesForm.SelectedObject = args.Column;
        }

        private void DnfDatabaseSelected(object sender, DatabaseEventArgs args)
        {
            propertiesForm.SelectedObject = args.Database;
        }

        private void DnfTableSelected(object sender, TableEventArgs args)
        {
            _selectedTable = args.Table;
            propertiesForm.SelectedObject = args.Table;
        }

        private void GenerateCode()
        {
            try
            {
                if (_selectedTable == null) return;
                var cgenerator = new Client { CustomValues = customValuesForm.CustomValues };
                resultForm.ContentText = cgenerator.Parse(_selectedTable, documentForm.ContentText);
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
            databaseNavigationForm.Connect();
        }

        private void DatabaseDisconnectClick(object sender, EventArgs e)
        {
            databaseNavigationForm.Disconnect();
        }

        private void EditConfigDatabaseClick(object sender, EventArgs e)
        {
            databaseNavigationForm.ShowEditConnectionString();
        }

        private void GenerateCodeClick(object sender, EventArgs e)
        {
            GenerateCode();
        }

        private void GenerateFilesClick(object sender, EventArgs e)
        {
            GenerateFiles();
        }

        private DirectorySelectionWindow _selectionWindow;

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

        private static void SelectionWindowOutputFolderSelected(object sender, FolderEventArgs args)
        {
            _OutputTemplateFolder = args.FolderName;
        }

        private static void SelectionWindowInputFolderSelected(object sender, FolderEventArgs args)
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
                    generator.CustomValue = customValuesForm.CustomValues;
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
            /*
            var uw = new UpdatesWindow();
            uw.ShowDialog();
            */
            var sInfo = new ProcessStartInfo("http://www.icodegenerator.net/");
            Process.Start(sInfo);
        }

        private void aboutVientoDigitalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var sInfo = new ProcessStartInfo("http://www.vientodigital.com/");
            Process.Start(sInfo);
        }

        private void templateToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (templatePage.IsDisposed)
            {
                if (documentForm.IsDisposed)
                {
                    documentForm = new DocumentForm { Text = "Template" };
                }
                templatePage = NewDocument("Template", documentForm, icon: IconToBitMap("itemplate.ico"));
                kryptonDockingManager.AddToWorkspace("Workspace", new KryptonPage[] { templatePage });
                kryptonDockingManager.HidePage(templatePage);
                kryptonDockingManager.ShowPage(templatePage);
            }
            else
            {
                if (!kryptonDockingManager.IsPageShowing(templatePage))
                {
                    kryptonDockingManager.ShowPage(templatePage);
                }
                else
                {
                    kryptonDockingManager.HidePage(templatePage);
                }
            }
        }

        private void resultsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (resultPage.IsDisposed)
            {
                if (resultForm.IsDisposed)
                {
                    resultForm = new ResultForm { Text = "Results" };
                }
                resultPage = NewDocument("Results", resultForm, icon: IconToBitMap("iresult.ico"));
                kryptonDockingManager.AddToWorkspace("Workspace", new KryptonPage[] { resultPage });
                kryptonDockingManager.HidePage(resultPage);
                kryptonDockingManager.ShowPage(resultPage);
            }
            else
            {
                if (!kryptonDockingManager.IsPageShowing(resultPage))
                {
                    kryptonDockingManager.ShowPage(resultPage);
                }
                else
                {
                    kryptonDockingManager.HidePage(resultPage);
                }
            }
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
                            documentForm.ContentText = line;
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
                SaveFile(_TemplateFile, documentForm.ContentText);
            else
                _TemplateFile = SaveAsFile(_TemplateFile, documentForm.ContentText);
        }

        private void saveAsTemplateToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _TemplateFile = SaveAsFile(null, documentForm.ContentText);
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
            SaveAsFile(null, resultForm.ContentText);
        }

        private void newTemplateToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _TemplateFile = null;
            documentForm.ContentText = string.Empty;
        }
    }
}