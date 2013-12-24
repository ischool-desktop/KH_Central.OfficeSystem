using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using FISCA.Presentation.Controls;
using DevComponents.Editors;

namespace KH_Central.OfficeSystem
{
    public partial class SchoolDistrictEdit : BaseForm
    {
        public string name1 = "";
        public string name2 = "";
        public string name3 = "";


        public SchoolDistrictEdit()
        {
            InitializeComponent();
        }

        public SchoolDistrictEdit(string name)
        {
            InitializeComponent();

            this.Text = name;
        }

        private void SchoolDistrictEdit_Load(object sender, EventArgs e)
        {
            for (int x = 1; x < 100; x++)
            {
                ListViewItem item = new ListViewItem();
                item.Name = "item" + x.ToString().PadLeft(3, '0');
                item.Text = x.ToString().PadLeft(3, '0');
                listViewEx1.Items.Add(item);
            }

        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (comboBoxEx1.SelectedIndex > -1)
                name1 = (comboBoxEx1.Items[comboBoxEx1.SelectedIndex] as ComboItem).Text;
            if (comboBoxEx2.SelectedIndex > -1)
                name2 = (comboBoxEx2.Items[comboBoxEx2.SelectedIndex] as ComboItem).Text;

            List<string> list = new List<string>();
            foreach (ListViewItem each in listViewEx1.Items)
            {
                if (each.Checked)
                {
                    list.Add(each.Text);
                }
            }

            name3 = string.Join(",", list);

            this.DialogResult = System.Windows.Forms.DialogResult.Yes;
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.No;
        }

        private void cbSelectAll_CheckedChanged(object sender, EventArgs e)
        {
            foreach (ListViewItem item in listViewEx1.Items)
            {
                item.Checked = cbSelectAll.Checked;
            }
        }
    }
}
