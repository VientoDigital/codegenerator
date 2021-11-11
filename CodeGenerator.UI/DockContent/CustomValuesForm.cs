using System;
using System.Collections;
using System.Data;
using System.IO;
using System.Windows.Forms;

namespace CodeGenerator.UI
{
    public partial class CustomValuesForm : UserControl
    {
        private static readonly string customValuesFilename = $"{AppDomain.CurrentDomain.BaseDirectory}CustomValues.xml";

        private DataSet set;

        public CustomValuesForm()
        {
            InitializeComponent();
            InitializeCustomValuesDataGrid();
        }
        public IDictionary CustomValues
        {
            get
            {
                IDictionary customValues = new Hashtable();
                foreach (DataRow row in set.Tables[0].Rows)
                {
                    customValues[row["Name"].ToString().ToUpper()] = row["Value"].ToString();
                }
                return customValues;
            }
        }

        private void FilterEmptyCustomValues()
        {
            DataRow[] rows = set.Tables[0].Select();
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

        private void GridCustomValuesLeave(object sender, EventArgs e)
        {
            FilterEmptyCustomValues();
            SaveCustomValues();
        }

        private void InitializeCustomValuesDataGrid()
        {
            set = new DataSet();
            var table = new DataTable();
            table.Columns.Add("Name");
            table.Columns.Add("Value");
            set.Tables.Add(table);

            if (File.Exists(customValuesFilename))
            {
                set.ReadXml(customValuesFilename);
            }

            gridCustomValues.DataSource = set.Tables[0];
        }

        private void SaveCustomValues()
        {
            set.WriteXml(customValuesFilename);
        }
    }
}