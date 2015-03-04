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
    public partial class BatchInputMessage : BaseForm
    {

        public string _message = "";
        public BatchInputMessage()
        {
            InitializeComponent();
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            _message = textBoxX1.Text;
            this.DialogResult = System.Windows.Forms.DialogResult.Yes;
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.No;
        }
    }
}
