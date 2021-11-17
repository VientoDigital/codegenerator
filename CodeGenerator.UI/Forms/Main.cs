﻿using System;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using CodeGenerator.Data.Structure;
using CodeGenerator.Generator;
using CodeGenerator.UI.Properties;
using Krypton.Docking;
using Krypton.Navigator;
using Krypton.Toolkit;

namespace CodeGenerator.UI
{
    public partial class Main : KryptonForm
    {
        private static string inputTemplateFolder = string.Empty;

        private static string outputTemplateFolder = string.Empty;

        private static Table selectedTable;

        private static string templateFile;

        private DirectorySelectionWindow directorySelectionWindow;

        private KryptonDockingWorkspace workspace;

        #region Forms

        private CustomValuesControl customValuesForm;

        private DatabaseNavigationControl databaseNavigationForm;

        private DocumentControl templateForm;

        private PropertiesControl propertiesForm;

        private ResultControl resultForm;

        private KryptonPage resultPage;

        private SnippetsControl snippetsForm;

        private KryptonPage templatePage;

        #endregion Forms

        public Main()
        {
            InitializeComponent();
            InitializeControls();
            //CheckForUpdates(); // Commented out, as no longer working
        }

        #region Event Handlers

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE1006:Naming Styles", Justification = "<Pending>")]
        private static void directorySelectionWindow_InputFolderSelected(object sender, FolderEventArgs args)
        {
            inputTemplateFolder = args.FolderName;
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE1006:Naming Styles", Justification = "<Pending>")]
        private static void directorySelectionWindow_OutputFolderSelected(object sender, FolderEventArgs args)
        {
            outputTemplateFolder = args.FolderName;
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE1006:Naming Styles", Justification = "<Pending>")]
        private void databaseNavigationForm_ColumnSelected(object sender, ColumnEventArgs args)
        {
            selectedTable = args.Column.ParentTable;
            propertiesForm.SelectedObject = args.Column;
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE1006:Naming Styles", Justification = "<Pending>")]
        private void databaseNavigationForm_DatabaseSelected(object sender, DatabaseEventArgs args)
        {
            propertiesForm.SelectedObject = args.Database;
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE1006:Naming Styles", Justification = "<Pending>")]
        private void databaseNavigationForm_TableSelected(object sender, TableEventArgs args)
        {
            selectedTable = args.Table;
            propertiesForm.SelectedObject = args.Table;
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE1006:Naming Styles", Justification = "<Pending>")]
        private void fileGenerator_OnComplete(object sender, EventArgs e)
        {
            //MessageBox.Show("File Generation Completed");
            if (IsValidFolder(outputTemplateFolder))
            {
                Process.Start(outputTemplateFolder);
            }
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE1006:Naming Styles", Justification = "<Pending>")]
        private void mnuFileDatabaseConnect_Click(object sender, EventArgs e)
        {
            databaseNavigationForm.Connect();
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE1006:Naming Styles", Justification = "<Pending>")]
        private void mnuFileDatabaseDisconnect_Click(object sender, EventArgs e)
        {
            databaseNavigationForm.Disconnect();
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE1006:Naming Styles", Justification = "<Pending>")]
        private void mnuFileEditConfiguration_Click(object sender, EventArgs e)
        {
            databaseNavigationForm.ShowEditConnectionString();
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE1006:Naming Styles", Justification = "<Pending>")]
        private void mnuFileNewTemplate_Click(object sender, EventArgs e)
        {
            templateFile = null;
            templateForm.ContentText = string.Empty;
            workspace.SelectPage(templatePage.UniqueName);
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE1006:Naming Styles", Justification = "<Pending>")]
        private void mnuFileOpenTemplate_Click(object sender, EventArgs e)
        {
            if (dlgOpenFile.ShowDialog(this) == DialogResult.OK)
            {
                try
                {
                    templateFile = dlgOpenFile.FileName;
                    using (var stream = dlgOpenFile.OpenFile())
                    using (var streamReader = new StreamReader(stream))
                    {
                        templateForm.ContentText = streamReader.ReadToEnd();
                    }
                    workspace.SelectPage(templatePage.UniqueName);
                }
                catch (Exception x)
                {
                    MessageBox.Show(@"Error: Could not read file from disk. Original error: " + x.Message);
                }
            }
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE1006:Naming Styles", Justification = "<Pending>")]
        private void mnuFileSaveAsResult_Click(object sender, EventArgs e)
        {
            SaveAsFile(null, resultForm.ContentText);
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE1006:Naming Styles", Justification = "<Pending>")]
        private void mnuFileSaveAsTemplate_Click(object sender, EventArgs e)
        {
            templateFile = SaveAsFile(null, templateForm.ContentText);
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE1006:Naming Styles", Justification = "<Pending>")]
        private void mnuFileSaveTemplate_Click(object sender, EventArgs e)
        {
            if (templateFile != null)
            {
                SaveFile(templateFile, templateForm.ContentText);
            }
            else
            {
                templateFile = SaveAsFile(templateFile, templateForm.ContentText);
            }
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE1006:Naming Styles", Justification = "<Pending>")]
        private void mnuFileSettings_Click(object sender, EventArgs e)
        {
            var settingsForm = new SettingsForm();
            settingsForm.ShowDialog();
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE1006:Naming Styles", Justification = "<Pending>")]
        private void mnuFileTemplateExit_Click(object sender, EventArgs e)
        {
            Close();
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE1006:Naming Styles", Justification = "<Pending>")]
        private void mnuGenerate_Click(object sender, EventArgs e)
        {
            GenerateCode();
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE1006:Naming Styles", Justification = "<Pending>")]
        private void mnuGenerateFiles_Click(object sender, EventArgs e)
        {
            GenerateFiles();
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE1006:Naming Styles", Justification = "<Pending>")]
        private void mnuHelpAbout_Click(object sender, EventArgs e)
        {
            new AboutWindow().ShowDialog();
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE1006:Naming Styles", Justification = "<Pending>")]
        private void mnuHelpAboutVientoDigital_Click(object sender, EventArgs e)
        {
            var processStartInfo = new ProcessStartInfo("http://www.vientodigital.com/") { UseShellExecute = true };
            Process.Start(processStartInfo);
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE1006:Naming Styles", Justification = "<Pending>")]
        private void mnuHelpDocumentation_Click(object sender, EventArgs e)
        {
            new DocumentationWindow().ShowDialog();
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE1006:Naming Styles", Justification = "<Pending>")]
        private void mnuViewResults_Click(object sender, EventArgs e)
        {
            EnsureResultsFormExists();

            if (!kryptonDockingManager.IsPageShowing(resultPage))
            {
                kryptonDockingManager.ShowPage(resultPage);
            }
            else
            {
                kryptonDockingManager.HidePage(resultPage);
            }
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE1006:Naming Styles", Justification = "<Pending>")]
        private void mnuViewTemplate_Click(object sender, EventArgs e)
        {
            if (templatePage.IsDisposed)
            {
                if (templateForm.IsDisposed)
                {
                    templateForm = new DocumentControl { Text = "Template" };
                }
                templatePage = NewDocument("Template", templateForm, icon: IconToBitMap(Resources.itemplate));
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

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE1006:Naming Styles", Justification = "<Pending>")]
        private void snippetsForm_SnippetSelected(object sender, SnippetEventArgs args)
        {
            templateForm.ContentText = templateForm.ContentText.Insert(templateForm.SelectionStart, SnippetsHelper.Snippets[args.Snippet].ToString());
        }

        #endregion Event Handlers

        //private void CheckForUpdates()
        //{
        //    mnuHelpAbout.Text = $"{mnuHelpAbout.Text} {AppVersion.Version}";
        //    if (AppVersion.HasNewUpdate)
        //    {
        //        mnuHelpAbout.BackColor = Color.LightCoral;
        //        mnuHelpAbout.ForeColor = Color.White;
        //        mnuHelpAbout.Text = $@"Download Code Generator (Version: {AppVersion.LatestVersion.Version})";
        //    }
        //}

        private void EnsureResultsFormExists()
        {
            if (resultPage.IsDisposed)
            {
                if (resultForm.IsDisposed)
                {
                    resultForm = new ResultControl { Text = "Results" };
                }
                resultPage = NewDocument("Results", resultForm, icon: IconToBitMap(Resources.iresult));
                kryptonDockingManager.AddToWorkspace("Workspace", new KryptonPage[] { resultPage });
                kryptonDockingManager.HidePage(resultPage);
                kryptonDockingManager.ShowPage(resultPage);
            }
        }

        private static bool EnsureTableSelected()
        {
            if (selectedTable == null)
            {
                MessageBox.Show("Please selected a table first.", "Nothing Selected", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return false;
            }

            return true;
        }

        private void GenerateCode()
        {
            try
            {
                if (!EnsureTableSelected())
                {
                    return;
                }

                EnsureResultsFormExists();
                var client = new Client { CustomValues = CustomValuesControl.CustomValues };
                resultForm.ContentText = client.Parse(selectedTable, templateForm.ContentText);
                workspace.SelectPage(resultPage.UniqueName);
            }
            catch (Exception x)
            {
                MessageBox.Show(this, x.Message, x.Message, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void GenerateFiles()
        {
            if (!EnsureTableSelected())
            {
                return;
            }

            if (IsValidFolder(inputTemplateFolder) && IsValidFolder(outputTemplateFolder))
            {
                try
                {
                    var fileGenerator = new FileGenerator();
                    fileGenerator.OnComplete += fileGenerator_OnComplete;
                    fileGenerator.CustomValues = CustomValuesControl.CustomValues;
                    fileGenerator.Generate(selectedTable, inputTemplateFolder, outputTemplateFolder);
                }
                catch (Exception x)
                {
                    MessageBox.Show(x.Message);
                }
            }
            else
            {
                SelectTemplatesDirectory();
            }
        }

        //private Bitmap IconToBitMap(string iconName)
        //{
        //    return IconToBitMap(Icon.ExtractAssociatedIcon($@"Resources\{iconName}"));
        //}

        private static Bitmap IconToBitMap(Icon icon)
        {
            return new Bitmap(icon.ToBitmap(), new Size(16, 16));
        }

        private void InitializeControls()
        {
            // Setup docking functionality
            workspace = kryptonDockingManager.ManageWorkspace(kryptonDockableWorkspace);
            kryptonDockingManager.ManageControl(kryptonPanel, workspace);
            kryptonDockingManager.ManageFloating(this);

            // Add initial docking pages
            databaseNavigationForm = new DatabaseNavigationControl { Text = "Database Navigation" };
            databaseNavigationForm.TableSelected += databaseNavigationForm_TableSelected;
            databaseNavigationForm.DatabaseSelected += databaseNavigationForm_DatabaseSelected;
            databaseNavigationForm.ColumnSelected += databaseNavigationForm_ColumnSelected;

            snippetsForm = new SnippetsControl { Text = "Snippets" };
            snippetsForm.SnippetSelected += snippetsForm_SnippetSelected;

            templateForm = new DocumentControl { Text = "Template" };
            resultForm = new ResultControl { Text = "Results" };
            propertiesForm = new PropertiesControl { Text = "Properties" };
            customValuesForm = new CustomValuesControl { Text = "Custom Values" };

            templatePage = NewDocument("Template", templateForm, icon: IconToBitMap(Resources.itemplate));
            resultPage = NewDocument("Results", resultForm, icon: IconToBitMap(Resources.iresult));

            kryptonDockingManager.AddToWorkspace("Workspace", new KryptonPage[] { templatePage, resultPage });
            kryptonDockingManager.AddAutoHiddenGroup("Control", DockingEdge.Left, new KryptonPage[]
            {
                NewPage("Snippets", snippetsForm, icon: IconToBitMap(Resources.isnippet))
            });
            kryptonDockingManager.AddDockspace("Control", DockingEdge.Left, new KryptonPage[]
            {
                NewPage("Database Navigation", databaseNavigationForm, icon: IconToBitMap(Resources.idb))
            });
            kryptonDockingManager.AddDockspace("Control", DockingEdge.Right, new KryptonPage[]
            {
                NewPage("Properties", propertiesForm, icon: IconToBitMap(Resources.igen)),
                NewPage("Custom Values", customValuesForm, icon: IconToBitMap(Resources.icustom))
            });
        }

        private static bool IsValidFolder(string folderPath)
        {
            return !string.IsNullOrEmpty(folderPath) && Directory.Exists(folderPath);
        }

        private static KryptonPage NewDocument(string name, Control content, Bitmap icon = null)
        {
            var page = NewPage(name, content, icon);
            page.ClearFlags(KryptonPageFlags.DockingAllowClose);
            return page;
        }

        private static KryptonPage NewPage(string name, Control content, Bitmap icon = null)
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

        private string SaveAsFile(string fileName, string contentText)
        {
            if (dlgSaveFile.ShowDialog() == DialogResult.OK)
            {
                fileName = dlgSaveFile.FileName;
                SaveFile(fileName, contentText);
            }
            return fileName;
        }

        private static string SaveFile(string fileName, string contentText)
        {
            using (var streamWriter = new StreamWriter(fileName))
            {
                streamWriter.Write(contentText);
            }
            return fileName;
        }

        private void SelectTemplatesDirectory()
        {
            if (directorySelectionWindow == null)
            {
                directorySelectionWindow = new DirectorySelectionWindow();
                directorySelectionWindow.InputFolderSelected += directorySelectionWindow_InputFolderSelected;
                directorySelectionWindow.OutputFolderSelected += directorySelectionWindow_OutputFolderSelected;
            }
            directorySelectionWindow.ShowDialog(this);
        }
    }
}