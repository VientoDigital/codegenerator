using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using CodeGenerator.Shared.Extensions;

namespace CodeGenerator.UI
{
    public partial class CustomValuesForm : UserControl
    {
        public CustomValuesForm()
        {
            InitializeComponent();
            gridCustomValues.DataSource = ConfigFile.Instance.CustomValues.ToDataTable();
        }

        public IDictionary CustomValues => (Dictionary<string, string>)ConfigFile.Instance.CustomValues;

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE1006:Naming Styles", Justification = "<Pending>")]
        private void gridCustomValues_Leave(object sender, EventArgs e)
        {
            for (int i = gridCustomValues.Rows.Count - 1; i > -1; i--)
            {
                DataGridViewRow row = gridCustomValues.Rows[i];
                if (!row.IsNewRow && string.IsNullOrEmpty(row.Cells[0].Value as string))
                {
                    gridCustomValues.Rows.RemoveAt(i);
                }
            }

            ConfigFile.Instance.CustomValues = (gridCustomValues.DataSource as DataTable).Rows
                .OfType<DataRow>()
                .ToDictionary(
                    k => k.ItemArray[0].ToString(),
                    v => v.ItemArray[1].ToString());
        }
    }
}