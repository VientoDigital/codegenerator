using System;
using System.Collections;
using System.Data;
using System.IO;
using System.Windows.Forms;

namespace iCodeGenerator.iCodeGeneratorGui
{
    public partial class CustomValuesForm : UserControl
    {
        public CustomValuesForm()
        {
            InitializeComponent();
            InitializeCustomValuesDataGrid();
        }

        private static string _CustomValuesFilename = AppDomain.CurrentDomain.BaseDirectory + "CustomValues.xml";
        private DataSet _CustomValues;

        private void InitializeCustomValuesDataGrid()
        {
            _CustomValues = new DataSet();
            DataTable dt = new DataTable();
            _CustomValues.Tables.Add(dt);
            _CustomValues.Tables[0].Columns.Add("Name");
            _CustomValues.Tables[0].Columns.Add("Value");
            if (File.Exists(_CustomValuesFilename))
            {
                _CustomValues.ReadXml(_CustomValuesFilename);
            }
            gridCustomValues.DataSource = _CustomValues.Tables[0];
        }

        private void SaveCustomValues()
        {
            _CustomValues.WriteXml(_CustomValuesFilename);
        }

        private void FilterEmptyCustomValues()
        {
            DataRow[] rows = _CustomValues.Tables[0].Select();
            for (int i = 0; i < rows.Length; i++)
            {
                if (rows[i][0].ToString().Trim().Length == 0)
                {
                    rows[i].Delete();
                }
                else
                {
                    rows[i][0] = rows[i][0].ToString().ToUpper().Trim();
                }
            }
            gridCustomValues.Refresh();
        }

        public IDictionary CustomValues
        {
            get
            {
                IDictionary customValues = new Hashtable();
                foreach (DataRow row in _CustomValues.Tables[0].Rows)
                {
                    customValues[row["Name"].ToString().ToUpper()] = row["Value"].ToString();
                }
                return customValues;
            }
        }

        private void GridCustomValuesLeave(object sender, EventArgs e)
        {
            FilterEmptyCustomValues();
            SaveCustomValues();
        }
    }
}