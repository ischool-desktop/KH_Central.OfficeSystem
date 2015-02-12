using FISCA.Permission;
using FISCA.Presentation.Controls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace KH_Central.OfficeSystem
{
    public partial class IsViewForm_Open : BaseForm
    {
        string Url { get; set; }
        public IsViewForm_Open(name m)
        {
            InitializeComponent();

            labelX1.Text = m._messageTitle1;
            textBoxX1.Text = m._value1;
            if (m.type)
            {
                linkLabel1.Visible = UserAcl.Current["KH_Central.OfficeSystem_Catalog004"].Executable;
            }
            else
            {
                linkLabel1.Visible = false;
            }
        }

        private void buttonX1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (!string.IsNullOrEmpty(Url))
            {
                this.Close();
                System.Diagnostics.Process.Start(Url);
            }
        }

        private void linkLabel1_LinkClicked_1(object sender, LinkLabelLinkClickedEventArgs e)
        {
            KH_Central.OfficeSystem.RibbonBar.CentralDataUpdateDocForm cdudf = new RibbonBar.CentralDataUpdateDocForm();
            cdudf.ShowDialog();   
        }
    }
}
